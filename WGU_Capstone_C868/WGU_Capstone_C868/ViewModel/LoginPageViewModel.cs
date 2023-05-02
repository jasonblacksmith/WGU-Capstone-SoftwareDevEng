using WGU_Capstone_C868.Services;
using WGU_Capstone_C868.Model;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class LoginPageViewModel : BaseViewModel    
    {
        public ObservableCollection<User> Users = new();
        public User User;
        


        async Task ValidateUser(string userName, string password)
        {
            Services.Calls.UserCalls userCalls = new();

            Users = await userCalls.GetUsersAsync();

            foreach(User U in Users)
            {
                if (userName == U.UserName)
                {
                    if (password == U.Password)
                    {
                        User = U;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Could Not Login",
                        $"Please check Username and Password and try again.", "OK");
                        return;
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Could Not Login",
                    $"Please check Username and Password and try again.", "OK");
                    return;
                }
            }
        }
        //On Create: Input a Username and Password(Establish best practices template for both)
        //Add User Method call and confirm
        //Login for new user initiated

        //On Login: Get the List of users and then iterate through to find a match
        //If Match, login and display user name
        //If no match, Alert no match
    }
}
