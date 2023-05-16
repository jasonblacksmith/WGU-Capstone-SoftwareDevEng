using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private UserCalls UserCalls = new UserCalls();

        //TODO: MRI's and Scans Card
        //TODO: Link to MRI's and Scans Page with Next Upcoming Proceedure
        //TODO: Display data form Appointments, Upcoming, Soonest to date
        //TODO: Get the Name of the Facility and Phone Number to display
        private AppointmentCalls AppointmentCalls = new AppointmentCalls();
        private AppointmentStateCalls AppointmentStateCalls = new AppointmentStateCalls();
        private ProceedureCalls ProceedureCalls = new ProceedureCalls();
        //TODO: Link to a map of the location
        private AddressCalls AddressCalls = new AddressCalls();
        //TODO: Sublink to Past Results
        private ResultCalls ResultCalls = new ResultCalls();
        //TODO: Empty State "Add your first proceedure!" Links to adding a new Proceedure Appointment

        //TODO: Relapse Diary Card
        //TODO: Link to Relapse Diary Page
        //TODO: Counter of days since last active relapse
        private RelapseCalls RelapseCalls = new RelapseCalls();

        //TODO: Notes/Questions Card
        //TODO: Link to Notes and Questions Page
        //TODO: Link to Last visit | Next visit

        public int UserId;

        [ObservableProperty]
        internal User dashboardUser;

        //TODO: Dashboard Page
        public DashboardViewModel()
        {
            pageTitle = "Dashboard";
            dashboardUser = CriticalObjects.UserData;
        }
    }
}
