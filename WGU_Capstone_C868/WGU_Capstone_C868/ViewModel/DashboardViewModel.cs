using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class DashboardViewModel : LoginPageViewModel
    {
        public static User ThisUser = new User();
        public Appointment AppointmentData = new Appointment();

        public bool initialDataLoaded;

        private static AppointmentCalls AppointmentCalls = new AppointmentCalls();
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

        //TODO: Dashboard Page
        public async Task Init()
        {
            try
            {
                PageTitle = "Dashboard";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                TheUser = ThisUser;
                //CritObj.UserData = ThisUser;
                Debug.WriteLine(TheUser.Name);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                AppointmentData = await SetMRIsAndScansAppointment(TheUser.UserId);
                Debug.WriteLine("Appointment: " + AppointmentData.LocationName + " " + AppointmentData.AppointmentId);
                AppointmentLocationName = AppointmentData.LocationName;
                AppointmentPhone = AppointmentData.PhoneNumber;
                AppointmentTime = AppointmentData.DateAndTime.ToString("dd:MM:yy hh:mm:tt");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                CritObj.ProceedureData = await ThisProceedure(AppointmentData.ProceedureId);
                Debug.WriteLine("Proceedure: " + CritObj.ProceedureData.Title + " " + CritObj.ProceedureData.ProceedureId);

                UpcomingProceedure = $"Upcoming: {CritObj.ProceedureData.Title}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                CritObj.AddressData = await ThisAddress(AppointmentData.AddressId);
                Debug.WriteLine("Address: " + CritObj.AddressData.StreetAddress + " " + CritObj.AddressData.AddressId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            RefreshView refresh = new();
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
            ObservableCollection<Appointment> appointments = await AppointmentCalls.GetAppointmentsAsync();
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
        public async Task ImageAndLabsButton()
        {
            ImgOrLabViewModel.ThisUser = TheUser;
            await Shell.Current.GoToAsync($"//ImageOrLabPage", true);
        }
    }
}
