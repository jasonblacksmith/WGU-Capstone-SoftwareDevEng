using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WGU_Capstone_C868.Model;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class DashboardViewModel : BaseViewModel, IQueryAttributable, INotifyPropertyChanged
    {
        public int ThisUserId { get; private set; }

        public bool initialDataLoaded;

        public CriticalObjects CritObj = new();

        private static UserCalls UserCalls = new UserCalls();
        private static AppointmentCalls AppointmentCalls = new AppointmentCalls();
        private static AppointmentStateCalls AppointmentStateCalls = new AppointmentStateCalls();
        private static ProceedureCalls ProceedureCalls = new ProceedureCalls();
        private static AddressCalls AddressCalls = new AddressCalls();
        private static ResultCalls ResultCalls = new ResultCalls();
        private static RelapseCalls RelapseCalls = new RelapseCalls();
        //TODO: !!!Empty State "Add your first proceedure!" Links to adding a new Proceedure Appointment

        //TODO: Relapse Diary Card
        //TODO: Link to Relapse Diary Page
        //TODO: Counter of days since last active relapse


        //TODO: Notes/Questions Card
        //TODO: Link to Notes and Questions Page
        //TODO: Link to Last visit | Next visit

        [ObservableProperty]
        public User theUser;

        [ObservableProperty]
        internal string appointmentLocationName;

        [ObservableProperty]
        internal string appointmentPhone;

        [ObservableProperty]
        internal string appointmentTime;

        [ObservableProperty]
        internal string upcomingProceedure;

        internal string mapsLocation;

        public DashboardViewModel()
        {
            Init();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> navigationParameter)
        {
            ThisUserId = (Int32)navigationParameter["User"];
            OnPropertyChanged("User");
        }

        //TODO: Dashboard Page
        public async void Init()
        {
            pageTitle = "Dashboard";

            CritObj.UserData = await ThisUser(ThisUserId);
            Debug.WriteLine(CritObj.UserData.Name);
            theUser = CritObj.UserData;

            CritObj.AppointmentData = await SetMRIsAndScansAppointment(ThisUserId);
            Debug.WriteLine("Appointment: " + CritObj.AppointmentData.LocationName + " " + CritObj.AppointmentData.AppointmentId);

            //ThisUser = CritObj.UserData;
            appointmentLocationName = CritObj.AppointmentData.LocationName;
            appointmentPhone = CritObj.AppointmentData.PhoneNumber;
            appointmentTime = CritObj.AppointmentData.DateAndTime.ToString();

            CritObj.ProceedureData = await ThisProceedure(CritObj.AppointmentData.ProceedureId);
            Debug.WriteLine("Proceedure: " + CritObj.ProceedureData.Title + " " + CritObj.ProceedureData.ProceedureId);

            upcomingProceedure = CritObj.ProceedureData.Title;

            CritObj.AddressData = await ThisAddress(CritObj.AppointmentData.AddressId);
            Debug.WriteLine("Address: " + CritObj.AddressData.StreetAddress + " " + CritObj.AddressData.AddressId);
        }

        public async Task<User> ThisUser(int UserId)
        {
            User user = new();
            try
            {
                user = await UserCalls.GetUserAsync(UserId);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<Address> ThisAddress(int addressId)
        {
            Address address = new Address();
            try
            {
                address = await AddressCalls.GetAddressAsync(addressId);
                return address;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }

        } 

        public async Task<Proceedure> ThisProceedure(int proceedureId)
        {
            Proceedure proceedure = new Proceedure();
            try
            {
                proceedure = await ProceedureCalls.GetProceedureAsync(proceedureId);
                return proceedure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<Appointment> SetMRIsAndScansAppointment(int userId)
        {
            ObservableCollection<Appointment> appointments  = await AppointmentCalls.GetAppointmentsAsync();
            ObservableCollection<Appointment> usersAppointments = new();
            Appointment appointment = new();
            try
            {
                foreach (Appointment a in appointments)
                {
                    if (a.UserId == userId)
                    {
                        usersAppointments.Add(a);
                    }
                }
                appointment = usersAppointments.OrderByDescending(a => a.DateAndTime).First();
                return appointment;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }            
        }

        [RelayCommand]
        public async Task OpenMaps()
        {
            Location location = new Location(CritObj.AddressData.Latitude,CritObj.AddressData.Longitude);
            var options = new MapLaunchOptions
            {
                Name = CritObj.AppointmentData.LocationName
            };

            try
            {
                await Map.Default.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
