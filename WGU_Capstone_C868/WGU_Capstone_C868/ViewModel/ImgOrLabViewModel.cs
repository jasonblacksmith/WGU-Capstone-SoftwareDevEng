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
        public string selectedProceedure;

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
        private string latitute;

        [ObservableProperty]
        private string longitude;

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

        public ImgOrLabViewModel() 
        {
            PageTitle = "Imaging and Labs";
            TheUser = ThisUser;
            EditMode(Edit);
            Init();

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


            //Add other asyncs here
        }

        [RelayCommand]
        public void Delete()
        {

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

        }

        public void OnSelectedProceedureChanged()
        {

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
        public void Save()
        {
            Appointment newAppointment = new() { 
                
            };
            Address newAddress = new() {
                StreetAddress = StreetAddress,
                SuiteNumber = SuiteOrBuilding,
                City = City,
                ZipCode = ZipCode,
                State = State,
                Country = Country,
                Latitude = Double.Parse(Latitute),
                Longitude = Double.Parse(Longitude)
            };


            Debug.WriteLine(newAppointment);
            Debug.WriteLine(newAddress);

            if(IsEdit)
            {

            }
            else
            {

            }
        }
    }
}
