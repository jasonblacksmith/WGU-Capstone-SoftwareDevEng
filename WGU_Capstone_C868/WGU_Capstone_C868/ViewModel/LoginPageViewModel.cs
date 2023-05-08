using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class LoginPageViewModel : BaseViewModel    
    {
        UserCalls UserCalls = new();

        public ObservableCollection<User> Users = new();


        [ObservableProperty]
        public string regexValidator = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        [ObservableProperty]
        public User user;

        public bool forLogin = true;

        [ObservableProperty]
        public string loginCreate;

        [ObservableProperty]
        public string userNameInput;

        [ObservableProperty]
        public string passwordInput;

        [ObservableProperty]
        public string name;

        public string password;

        public string userName;

        [RelayCommand]
        public void SetAsLogin()
        {
            forLogin = true;
            LoginCreate = "  Login  ";
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
            userName = userNameInput;
            password = passwordInput;
            if (forLogin && (userName is not null && password is not null))
            {
                await ValidateUser(userName, password);
            }
            else if (!forLogin && (userName is not null && password is not null))
            {
                await AddNewUser(userName, password);
            }
            else
            {
                return;
            }
        }
        //TODO: Implement the freaking DataBase you NUTTER!!!
        public async Task ValidateUser(string userName, string password)
        {
            UserCalls userCalls = new();

            Users = await userCalls.GetUsersAsync();

            foreach(User U in Users)
            {
                if (userName == U.UserName)
                {
                    if (password == U.Password)
                    {
                        user = U;
                        await Shell.Current.GoToAsync("//Dashboard", true);
                        //return;
                    }
                }
            }
            if(user == null)
            {
                await Shell.Current.DisplayAlert("Could Not Login",
                $"Please check Username and Password and try again.", "OK");
                return;
            }
        }

        async Task AddNewUser(string userName, string password)
        {
            User user = new()
            {
                UserName = userName,
                Password = password,
                Name = await Shell.Current.DisplayPromptAsync("Please input Name", "Please input your Name", "Ok", "Cancel", "Name", 60, Keyboard.Text, "")
            };

            await UserCalls.AddUserAsync(user);
        }



        //On Create: Input a Username and Password(Establish best practices template for both)
        //Add User Method call and confirm
        //Login for new user initiated

        //On Login: Get the List of users and then iterate through to find a match
        //If Match, login and display user name
        //If no match, Alert no match
    }
}
