using System;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class ImgOrLabViewModel : LoginPageViewModel
    {
        //TODO: Look into removing the Critical Objects file.
        public static User ThisUser = new User();
        public static bool Edit = false;
        public static int EditAppointemntId;

        private ObservableCollection<Proceedure> _proceedures = new ObservableCollection<Proceedure>();
        public ObservableCollection<Proceedure> Proceedures
        {
            get { return _proceedures; }
            set { SetProperty(ref _proceedures, value); }
        }

        public Appointment CurrentAppointment = new();
        public Address CurrentAddress = new();

        private static AppointmentCalls AppointmentCalls = new();
        private static ProceedureCalls ProceedureCalls = new();
        private static AddressCalls AddressCalls = new();

        [ObservableProperty]
        public int selectedProceedure;

        [ObservableProperty]
        private string locationName;

        [ObservableProperty]
        private string phoneNum;

        [ObservableProperty]
        private string streetAddress;

        [ObservableProperty]
        private string suiteOrBuilding;

        [ObservableProperty]
        private string city;

        [ObservableProperty]
        private string zipCode;

        [ObservableProperty]
        private string state;

        [ObservableProperty]
        private string country;

        [ObservableProperty]
        private double inputLatitude;

        [ObservableProperty]
        private double inputLongitude;

        [ObservableProperty]
        private DateTime dateAppointment;

        [ObservableProperty]
        private TimeSpan timeAppointment;

        [ObservableProperty]
        private string noteAppointment;

        [ObservableProperty]
        private bool isEdit = false;

        [ObservableProperty]
        private string updateOrSave = "Save";

        [ObservableProperty]
        private bool addressValid = false;

        [ObservableProperty]
        private DateTime dateValidMin = DateTime.Now;
        public ImgOrLabViewModel() 
        {
            //PageTitle = "Imaging and Labs";
            //TheUser = ThisUser;
            //EditMode(Edit);
            //Task task = Init();
        }

        [RelayCommand]
        public async Task Reload()
        {
            PageTitle = "Imaging and Labs";
            TheUser = ThisUser;
            EditMode(Edit);
            await Init();
        }

        public async Task Init()
        {
            try
            {
                if (Proceedures.Count == 0)
                {
                    Proceedures = await ProceedureCalls.GetProceeduresAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                if (IsEdit)
                {
                    await LoadAppointmentAndAddress();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            //Add other asyncs here
        }

        [RelayCommand]
        public async Task Delete()
        {
            try
            {
                await AppointmentAddressCascadeDelete(CurrentAppointment, CurrentAddress);
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex);
                throw;
            }
            await Shell.Current.DisplayAlert("Deleted!", "Appointment has been deleted.", "Okay");

            DashboardViewModel.ThisUser = TheUser;
            await Shell.Current.GoToAsync($"//Dashboard", true);
        }

        [RelayCommand]
        public async Task Cancel()
        {
            try
            {
                DashboardViewModel.ThisUser = TheUser;
                await Shell.Current.GoToAsync($"//Dashboard", true);
            }
            catch( Exception ex )
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void EditMode(bool edit)
        {
            if(edit)
            {
                IsEdit = true;
                UpdateOrSave = "Update";
            }
            else
            {
                IsEdit = false;
                UpdateOrSave = "Save";
            }
        }

        public async Task LoadAppointmentAndAddress()
        {
            CurrentAppointment = await AppointmentCalls.GetAppointmentAsync(EditAppointemntId);
            CurrentAddress = await AddressCalls.GetAddressAsync(CurrentAppointment.AddressId);

            SelectedProceedure = CurrentAppointment.ProceedureId;
            LocationName = CurrentAppointment.LocationName;
            PhoneNum = CurrentAppointment.PhoneNumber;
            StreetAddress = CurrentAddress.StreetAddress;
            SuiteOrBuilding = CurrentAddress.SuiteNumber;
            City = CurrentAddress.City;
            ZipCode = CurrentAddress.ZipCode;
            State = CurrentAddress.State;
            Country = CurrentAddress.Country;
            InputLatitude = CurrentAddress.Latitude;
            InputLongitude = CurrentAddress.Longitude;
            DateAppointment = CurrentAppointment.DateAndTime.Date;
            TimeAppointment = CurrentAppointment.DateAndTime.TimeOfDay;
            NoteAppointment = CurrentAppointment.Notes;
        }

        public void ValidateAddress()
        {
            if ((StreetAddress is not null || StreetAddress != string.Empty)
                && (City is not null || City != string.Empty)
                && (ZipCode is not null || ZipCode != string.Empty)
                && (State is not null || State != string.Empty) 
                && (Country is not null || Country != string.Empty))
            {
                AddressValid = true;
            }
            else
            {
                AddressValid = false;
            }
        }

        public async Task<bool> AppointmentAddressCascadeDelete(Appointment appointment, Address address)
        {
            try
            {
                await AddressCalls.RemoveAddressAsync(address);

                await AppointmentCalls.RemoveAppointmentAsync(appointment);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
                throw;
            }

        }

        [RelayCommand]
        public async void Save()
        {
            if(IsEdit)
            {
                Address newAddress = new()
                {
                    AddressId = CurrentAddress.AddressId,
                    StreetAddress = StreetAddress,
                    SuiteNumber = SuiteOrBuilding,
                    City = City,
                    ZipCode = ZipCode,
                    State = State,
                    Country = Country,
                    Latitude = InputLatitude,
                    Longitude = InputLongitude
                };

                try
                {
                    if (!CurrentAddress.Equals(newAddress))
                    {
                        await AddressCalls.UpdateAddressAsync(newAddress);
                        CurrentAppointment.AddressId = newAddress.AddressId;
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }

                Appointment newAppointment = new()
                {
                    AppointmentId = CurrentAppointment.AppointmentId,
                    ProceedureId = SelectedProceedure + 1,
                    LocationName = LocationName,
                    AddressId = CurrentAppointment.AddressId,
                    Notes = NoteAppointment,
                    PhoneNumber = PhoneNum,
                    Current = CheckDateTimeCurrent(DateAppointment.Date + TimeAppointment),
                    DateAndTime = DateAppointment.Date + TimeAppointment,
                    UserId = CurrentAppointment.UserId
                };

                try
                {
                    if (!CurrentAppointment.Equals(newAppointment))
                    {
                        await AppointmentCalls.UpdateAppointmentAsync(newAppointment);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }

                Debug.WriteLine(newAppointment);
                Debug.WriteLine(newAddress);

                await Shell.Current.DisplayAlert("Updated!", "Appointment is updated.", "Okay");

                DashboardViewModel.ThisUser = TheUser;
                await Shell.Current.GoToAsync($"//Dashboard", true);
            }
            else
            {

                try
                {
                    Address newAddress = new()
                    {
                        AddressId = 0,
                        StreetAddress = StreetAddress,
                        SuiteNumber = SuiteOrBuilding,
                        City = City,
                        ZipCode = ZipCode,
                        State = State,
                        Country = Country,
                        Latitude = InputLatitude,
                        Longitude = InputLongitude
                    };

                    await AddressCalls.AddAddressAsync(newAddress);

                    Appointment newAppointment = new()
                    {
                        AppointmentId = 0,
                        ProceedureId = SelectedProceedure + 1,
                        LocationName = LocationName,
                        AddressId = newAddress.AddressId,
                        Notes = NoteAppointment,
                        PhoneNumber = PhoneNum,
                        Current = CheckDateTimeCurrent(DateAppointment.Date + TimeAppointment),
                        DateAndTime = DateAppointment.Date + TimeAppointment,
                        UserId = TheUser.UserId
                    };

                    await AppointmentCalls.AddAppointmentAsync(newAppointment);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }

                await Shell.Current.DisplayAlert("Saved!", "Appointemnt was saved.", "Okay");

                DashboardViewModel.ThisUser = TheUser;
                await Shell.Current.GoToAsync($"//Dashboard", true);
            }
        }

        private DateTime ReconstructDateTime(DateTime date, TimeSpan time)
        {
            return date.Date + time; ;
        }

        private bool CheckDateTimeCurrent(DateTime dateTime)
        {
            if(dateTime < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        [RelayCommand]
        private async Task GetCoordinates()
        {
            try
            {
                string address = $"{StreetAddress}, {City}, {State} {ZipCode}, {Country}";
                var url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + HttpUtility.UrlEncode(address) + "&key=AIzaSyCz6a7X5kUHfLZIw9CBwaWa1m4LYfcxzBo";

                var client = new HttpClient();
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(result);

                dynamic geo = JsonConvert.DeserializeObject(result);

                //Parse AND set Latitude double and Longitude double
                InputLatitude = (double)geo.results[0].geometry.location.lat;
                InputLongitude = (double)geo.results[0].geometry.location.lng;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [RelayCommand]
        public async Task BackToLogin()
        {
            TheUser = null;
            ThisUser = null;

            // Load new page
            await Shell.Current.GoToAsync("//MainPage", false);
        }
    }
}
