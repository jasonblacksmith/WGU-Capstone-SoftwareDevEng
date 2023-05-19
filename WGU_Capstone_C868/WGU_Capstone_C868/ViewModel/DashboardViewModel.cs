using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model;
using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private UserCalls UserCalls = new UserCalls();

        //TODO: Link to MRI's and Scans Page with Next Upcoming Proceedure
        //TODO: Get the Name of the Facility and Phone Number to display
        private AppointmentCalls AppointmentCalls = new AppointmentCalls();
        private AppointmentStateCalls AppointmentStateCalls = new AppointmentStateCalls();
        private ProceedureCalls ProceedureCalls = new ProceedureCalls();
        //TODO: Link to a map of the location
        private AddressCalls AddressCalls = new AddressCalls();
        //TODO: Sublink to Past Results
        private ResultCalls ResultCalls = new ResultCalls();
        //TODO: !!!Empty State "Add your first proceedure!" Links to adding a new Proceedure Appointment

        //TODO: Relapse Diary Card
        //TODO: Link to Relapse Diary Page
        //TODO: Counter of days since last active relapse
        private RelapseCalls RelapseCalls = new RelapseCalls();

        //TODO: Notes/Questions Card
        //TODO: Link to Notes and Questions Page
        //TODO: Link to Last visit | Next visit

        [ObservableProperty]
        internal User dashboardUser;

        [ObservableProperty]
        internal string appointmentLocationName = CriticalObjects.AppointmentData.LocationName;

        [ObservableProperty]
        internal string appointmentPhone = CriticalObjects.AppointmentData.PhoneNumber;

        [ObservableProperty]
        internal string upcomingProceedure = CriticalObjects.ProceedureData.Title;

        [ObservableProperty]
        internal string appointmentTime = CriticalObjects.AppointmentData.DateAndTime.ToString();

        internal string mapsLocation = "";

        private async Task<Address> ThisAddress()
        {
            Address address = new Address();
            address = await AddressCalls.GetAddressAsync(CriticalObjects.AppointmentData.AddressId);
            return address; 
        } 

        private async Task<Proceedure> ThisProceedure()
        {
            Proceedure proceedure = new Proceedure();
            proceedure = await ProceedureCalls.GetProceedureAsync(CriticalObjects.AppointmentData.ProceedureId);
            return CriticalObjects.ProceedureData = proceedure;
        }
        //TODO: Dashboard Page
        public DashboardViewModel()
        {
            pageTitle = "Dashboard";
            dashboardUser = CriticalObjects.UserData;
        }

        private async Task<DashboardViewModel> InitializeAsync()
        {
            _ = await SetMRIsAndScansAppointment(dashboardUser.UserId);
            _ = await ThisAddress();
            _ = await ThisProceedure();
            return this;
        }

        public static Task<DashboardViewModel> CreateAsync()
        {
            var ret = new DashboardViewModel();
            return ret.InitializeAsync();
        }

        public static async Task UseDashboardViewModelAsync()
        {
            _ = await CreateAsync();
        }

        private async Task<Appointment> SetMRIsAndScansAppointment(int userId)
        {
            ObservableCollection<Appointment> _appointments = await AppointmentCalls.GetAppointmentsAsync();

            foreach (Appointment a in _appointments)
            {
                if (a.UserId != userId)
                {
                    _appointments.Remove(a);
                }
            }
            return CriticalObjects.AppointmentData = _appointments.OrderByDescending(a => a.DateAndTime).First();
        }

        [RelayCommand]
        public async Task OpenMaps()
        {
            Location location = new Location(CriticalObjects.AddressData.Latitude,CriticalObjects.AddressData.Longitude);
            var options = new MapLaunchOptions
            {
                Name = CriticalObjects.AppointmentData.LocationName
                //, NavigationMode = NavigationMode.Driving
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
