using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        readonly UserCalls userCalls = new();

        internal MoqDataLoader moqDataLoader = new();

        public CriticalObjects CritObj = new CriticalObjects();
        
        public ObservableCollection<User> Users = new();
        public int UserId;
        public bool forLogin = true;
        public string password;
        public string userName;

        [ObservableProperty]
        public User theUser;
        [ObservableProperty]
        public string regexValidator = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        [ObservableProperty]
        public string loginCreate = "  Login  ";
        [ObservableProperty]
        public string userNameInput;
        [ObservableProperty]
        public string passwordInput;
        [ObservableProperty]
        public string name;

        [RelayCommand]
        public void SetAsLogin()
        {
            forLogin = true;
            LoginCreate = "  Login  ";
        }

        [RelayCommand]
        public void ClearLogin()
        {
            UserNameInput = null;
            PasswordInput = null;
        }

        [RelayCommand]
        public async void SetAsCreate()
        {
            forLogin = false;
            LoginCreate = "  Create  ";
            await Shell.Current.DisplayAlert("Password Requirements", "Password must have 1 lowercase and 1 capital letter, 1 number, 1 special character, and be 8 characters or longer.", "OK");
        }

        [RelayCommand]
        public async Task LoginOrCreate()
        {
            userName = UserNameInput;
            password = PasswordInput;
            if (forLogin && (userName is not null && password is not null))
            {
                await ValidateUser(userName, password);
            }
            else if (!forLogin && (userName is not null && ValidPassword(password)))
            {
                await AddNewUser(userName, password);
            }
            else
            {
                return;
            }
        }

        //For Testing Only!!!
        [RelayCommand]
        public async Task KillTestData()
        {
            await SqLiteDataService.BurnAndRebuild();
            await LoadData.Init();
            await Shell.Current.DisplayAlert("Restart the App", "You must close and reopen the application for data to refresh!", "Close");
            Application.Current.Quit();
            return;
        }

        //For Testing Only!!!
        [RelayCommand]
        public void LoadMockData()
        {
            moqDataLoader.Init();
            return;
        }

        public async Task ValidateUser(string userName, string password)
        {
            Users = await userCalls.GetUsersAsync();

            foreach(User U in Users)
            {
                if (userName == U.UserName)
                {
                    if (password == U.Password)
                    {
                        TheUser = U;
                        DashboardViewModel.ThisUser = TheUser;
                        await Shell.Current.GoToAsync($"//Dashboard", true);
                    }
                }
            }
            if(TheUser == null)
            {
                await Shell.Current.DisplayAlert("Could Not Login",
                $"Please check Username and Password and try again.", "OK");
                return;
            }
        }

        async Task AddNewUser(string userName, string password)
        {
            Users = await userCalls.GetUsersAsync();
            foreach (User U in Users)
            {
                if (userName == U.UserName)
                {
                    await Shell.Current.DisplayAlert("Username Exists", "Username alreeady exists, choose another.", "OK");
                    return;
                }
            }

            TheUser = new()
            {
                UserName = userName,
                Password = password,
                Name = await Shell.Current.DisplayPromptAsync("Please input Name", "Please input your Name", "Ok", "Cancel", "Name", 60, Keyboard.Text, "")
            };

            try
            {
                await userCalls.AddUserAsync(TheUser);
                await Shell.Current.DisplayAlert("User created!", "User created successfully! Please Login using your Username and Password", "OK");
                SetAsLogin();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        bool ValidPassword(string Password)
        {
            Match match = Regex.Match(Password, RegexValidator);
            if(Password is not null && match.Success)
            {
                return true;
            }
            else { return false; }
        }
    }
}
