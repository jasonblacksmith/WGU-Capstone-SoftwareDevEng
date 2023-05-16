using System.Text.RegularExpressions;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class LoginPageViewModel : BaseViewModel    
    {
        readonly UserCalls userCalls = new();
        
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
            loginCreate = "  Login  ";
        }

        [RelayCommand]
        public async void SetAsCreate()
        {
            forLogin = false;
            loginCreate = "  Create  ";
            await Shell.Current.DisplayAlert("Password Requirements", "Password must have 1 lowercase and 1 capital letter, 1 number, 1 special character, and be 8 characters or longer.", "OK");
        }

        [RelayCommand]
        public async Task LoginOrCreate()
        {
            userName = userNameInput;
            password = passwordInput;
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
                        theUser = U;
                        CriticalObjects.UserData = theUser;
                        await Shell.Current.GoToAsync($"//Dashboard", true);
                    }
                }
            }
            if(theUser == null)
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

            theUser = new()
            {
                UserName = userName,
                Password = password,
                Name = await Shell.Current.DisplayPromptAsync("Please input Name", "Please input your Name", "Ok", "Cancel", "Name", 60, Keyboard.Text, "")
            };

            try
            {
                await userCalls.AddUserAsync(theUser);
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
            Match match = Regex.Match(Password, regexValidator);
            if(Password is not null && match.Success)
            {
                return true;
            }
            else { return false; }
        }
    }
}
