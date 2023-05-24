using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;

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
        private static ResultCalls ResultCalls = new();

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
            PageTitle = "Imaging and Labs";
            TheUser = ThisUser;
            EditMode(Edit);
            Task task = Init();

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
                await AddressCalls.RemoveAddressAsync(CurrentAddress);
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex);
                throw;
            }
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

        public bool ValidateAddress()
        {
            //Add in address from fields and see if it is all there!
            return false;
        }

        public async Task<bool> AppointmentAddressCascadeDelete(Appointment appointment, Address address)
        {
            try
            {
                //Get Address by Appointment AddressId
                
                await AddressCalls.RemoveAddressAsync(address);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            return false;
        }

        [RelayCommand]
        public void GetLocation(Address address)
        {
            //TODO: Get the Location coordinates  
            //Constrict the Request URL from the address
            //Request and see if a valid location is returned
            //If Not: Alert
            //If Yes: Confirm and populate data into fields
        }

        [RelayCommand]
        public async void Save()
        {
            if(IsEdit)
            {
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

                Debug.WriteLine(newAppointment);
                Debug.WriteLine(newAddress);

                if (!CurrentAddress.Equals(newAddress))
                {
                    await AddressCalls.UpdateAddressAsync(newAddress);
                }
                if (!CurrentAppointment.Equals(newAppointment))
                { 
                    await AppointmentCalls.UpdateAppointmentAsync(newAppointment);
                }
            }
            else
            {
                Appointment newAppointment = new()
                {
                    AppointmentId = 0,
                    ProceedureId = SelectedProceedure + 1,
                    LocationName = LocationName,
                    AddressId = CurrentAppointment.AddressId,
                    Notes = NoteAppointment,
                    PhoneNumber = PhoneNum,
                    Current = CheckDateTimeCurrent(DateAppointment.Date + TimeAppointment),
                    DateAndTime = DateAppointment.Date + TimeAppointment,
                    UserId = TheUser.UserId
                };
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
                await AppointmentCalls.AddAppointmentAsync(newAppointment);
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
    }
}
