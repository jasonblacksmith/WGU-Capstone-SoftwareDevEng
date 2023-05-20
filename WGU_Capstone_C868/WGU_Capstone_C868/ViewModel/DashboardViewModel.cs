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
        private static UserCalls UserCalls = new UserCalls();

        //TODO: Link to MRI's and Scans Page with Next Upcoming Proceedure
        //TODO: Get the Name of the Facility and Phone Number to display
        private static AppointmentCalls AppointmentCalls = new AppointmentCalls();
        private static AppointmentStateCalls AppointmentStateCalls = new AppointmentStateCalls();
        private static ProceedureCalls ProceedureCalls = new ProceedureCalls();
        //TODO: Link to a map of the location
        private static AddressCalls AddressCalls = new AddressCalls();
        //TODO: Sublink to Past Results
        private static ResultCalls ResultCalls = new ResultCalls();
        //TODO: !!!Empty State "Add your first proceedure!" Links to adding a new Proceedure Appointment

        //TODO: Relapse Diary Card
        //TODO: Link to Relapse Diary Page
        //TODO: Counter of days since last active relapse
        private RelapseCalls RelapseCalls = new RelapseCalls();

        //TODO: Notes/Questions Card
        //TODO: Link to Notes and Questions Page
        //TODO: Link to Last visit | Next visit

        [ObservableProperty]
        internal User dashboardUser = CriticalObjects.UserData;

        [ObservableProperty]
        internal string appointmentLocationName = CriticalObjects.AppointmentData.LocationName;

        [ObservableProperty]
        internal string appointmentPhone = CriticalObjects.AppointmentData.PhoneNumber;

        [ObservableProperty]
        internal string upcomingProceedure = CriticalObjects.ProceedureData.Title;

        [ObservableProperty]
        internal string appointmentTime = CriticalObjects.AppointmentData.DateAndTime.ToString();

        internal string mapsLocation;

        //TODO: Dashboard Page
        public async Task Init()
        {
            pageTitle = "Dashboard";
            await SetMRIsAndScansAppointment(dashboardUser.UserId);
            await ThisProceedure(CriticalObjects.AppointmentData.ProceedureId);
        }

        private static async Task<Address> ThisAddress()
        {
            Address address = new Address();
            address = await AddressCalls.GetAddressAsync(CriticalObjects.AppointmentData.AddressId);
            return CriticalObjects.AddressData = address; 
        } 

        private static async Task<Proceedure> ThisProceedure(int proceedureId)
        {
            Proceedure proceedure = new Proceedure();
            proceedure = await ProceedureCalls.GetProceedureAsync(proceedureId);
            return CriticalObjects.ProceedureData = proceedure;
        }

        private static async Task<Appointment> SetMRIsAndScansAppointment(int userId)
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
