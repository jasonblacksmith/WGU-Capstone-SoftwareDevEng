using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Platform;
using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class DashboardViewModel : LoginPageViewModel
    {
        public static User ThisUser = new();
        #region Variables and Classes for Img/Lab Appointemnts

        public Appointment AppointmentData = new Appointment();
        public Address AddressData = new Address();
        public Proceedure ProceedureData = new Proceedure();

        private static AppointmentCalls AppointmentCalls = new AppointmentCalls();
        private static ProceedureCalls ProceedureCalls = new ProceedureCalls();
        private static AddressCalls AddressCalls = new AddressCalls();
        //private static ResultCalls ResultCalls = new ResultCalls();

        [ObservableProperty]
        private bool upcomingTrue;

        [ObservableProperty]
        internal string appointmentLocationName;

        [ObservableProperty]
        internal string appointmentPhone;

        [ObservableProperty]
        internal string appointmentTime;

        [ObservableProperty]
        internal string upcomingProceedure;

        [ObservableProperty]
        internal bool noAppointment;

        [ObservableProperty]
        internal bool yesAppointment;

        [ObservableProperty]
        internal string daysSinceLastRelapse;

        internal string mapsLocation;

        #endregion

        #region Init
        public DashboardViewModel()
        {

        }

        [RelayCommand]
        public async Task Reload()
        {
            AppointmentData = null;
            AddressData = null;
            AppointmentLocationName = null;
            AppointmentPhone = null;
            AppointmentTime = null;
            UpcomingProceedure = null;
            mapsLocation = null;
            await Init();
        }

        public async Task Init()
        {
            PageTitle = "Dashboard";

            TheUser = ThisUser;
            Debug.WriteLine(TheUser.Name);

            try
            {
                AppointmentData = await SetMRIsAndScansAppointment(TheUser.UserId);

                if (AppointmentData == null)
                {
                    NoAppointment = true;
                    YesAppointment = false;
                }
                else
                {
                    NoAppointment = false;
                    YesAppointment = true;
                    Debug.WriteLine("Appointment: " + AppointmentData.LocationName + " " + AppointmentData.AppointmentId);
                    AppointmentLocationName = AppointmentData.LocationName;
                    AppointmentPhone = AppointmentData.PhoneNumber;
                    AppointmentTime = AppointmentData.DateAndTime.ToString("MM:dd:yy hh:mm:tt");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                if (YesAppointment)
                {
                    ProceedureData = await ProceedureCalls.GetProceedureAsync(AppointmentData.ProceedureId); ;
                    Debug.WriteLine("Proceedure: " + ProceedureData.Title + " " + ProceedureData.ProceedureId);

                    UpcomingProceedure = $"Upcoming: {ProceedureData.Title}";
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                if(YesAppointment)
                {
                    AddressData = await ThisAddress(AppointmentData.AddressId);
                    Debug.WriteLine("Address: " + AddressData.StreetAddress + " " + AddressData.AddressId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            RefreshView refresh = new();

            DaysSinceLastRelapse = await DaysSinceLastRelapseLogic();
        }
        #endregion

        #region Variables and Classes for Relapse Diary
        public static Relapse ThisRelapse = new();

        private static RelapseCalls RelapseCalls = new RelapseCalls();

        [RelayCommand]
        public async Task ToRelapseDiary()
        {
            RelapseDiaryViewModel.ThisUser = TheUser;
            RelapseDiaryViewModel.ThisDaysSinceMessage = DaysSinceLastRelapse;
            await Shell.Current.GoToAsync("//RelapseDiary", true);
        }

        public async Task<string> DaysSinceLastRelapseLogic()
        {
            ObservableCollection<Relapse> relapseRecords = new();
            try
            {
                relapseRecords = await RelapseCalls.GetRelapsesAsync();
                List<DateTime> days = new List<DateTime>();
                foreach (Relapse r in relapseRecords)
                {
                    if(TheUser.UserId == r.UserId)
                    {
                        days.Add(r.DateAndTime);
                    }
                }
                DateTime MaxDateTime = days.Max<DateTime>();
                TimeSpan timeSpan = DateTime.Today.Subtract(MaxDateTime);
                int daysSince = timeSpan.Days;
                string DaysSinceMessage = $"Days since last relapse: {daysSince}";
                return DaysSinceMessage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
                throw;
            }
        }

        #endregion

        #region ViewModel Methods for Img/Lab Appointments

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
            ObservableCollection<Appointment> appointments = await AppointmentCalls.GetAppointmentsAsync();
            try
            {
                foreach (Appointment a in appointments)
                {
                    //Check to see if there is a current Appointment and
                    //make sure to set ones set current that are out dated for the user
                    //to not current.
                    if (a.DateAndTime < DateTime.Now)
                    {
                        a.Current = false;
                        await AppointmentCalls.UpdateAppointmentAsync(a);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            ObservableCollection<Appointment> appointments1 = await AppointmentCalls.GetAppointmentsAsync();
            try
            {
                foreach (Appointment a in appointments1)
                {
                    //Return the current appointment for the user
                    if ((a.UserId == userId) && (a.DateAndTime > DateTime.Now) && (a.Current == true))
                    {
                        return a;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [RelayCommand]
        public async Task OpenMaps()
        {
            Location location = new Location(AddressData.Latitude, AddressData.Longitude);
            var options = new MapLaunchOptions
            {
                Name = AppointmentData.LocationName
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

        [RelayCommand]
        public async Task AddNewAppointment()
        {
            ImgOrLabViewModel.ThisUser = TheUser;
            ImgOrLabViewModel.Edit = false;
            await Shell.Current.GoToAsync($"//ImageOrLabPage", true);
        }

        [RelayCommand]
        public async Task OpenCurrentAppointment()
        {
            ImgOrLabViewModel.ThisUser = TheUser;
            ImgOrLabViewModel.Edit = true;
            ImgOrLabViewModel.EditAppointemntId = AppointmentData.AppointmentId;
            await Shell.Current.GoToAsync($"//ImageOrLabPage", true);
        }

        #endregion

        [RelayCommand]
        public async Task BackToLogin()
        {
            TheUser = null;
            ThisUser = null;

            // Load new page
            await Shell.Current.GoToAsync("//MainPage", true);
        }
    }
}
