using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class UserCalls : IUserCalls
    {
        public User user;
        public ObservableCollection<User> users = new();

        //Creates and adds new User record to DB
        public async Task<User> AddUserAsync(User user)
        {
            User AddUser = user;
            try
            {
                await SqLiteDataService.Db.InsertAsync(AddUser);
                return AddUser;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired User record from the DB
        public async Task<User> GetUserAsync(int pk)
        {
            User GetUser = new();
            try
            {
                GetUser = await SqLiteDataService.Db.GetAsync<User>(pk);
                return GetUser;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Users int the table
        public async Task<ObservableCollection<User>> GetUsersAsync()
        {
            List<User> Users = new();
            Users = await SqLiteDataService.Db.Table<User>().ToListAsync();
            foreach (User User in Users)
            {
                users.Add(User);
            }
            return users;
        }

        //Removes or Deletes the desired User record from the DB
        public async Task<bool> RemoveUserAsync(User user)
        {
            try
            {
                await SqLiteDataService.Db.DeleteAsync<User>(user);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired User in the User table in the DB
        public async Task<User> UpdateUserAsync(User user)
        {
            User UpdateUser = user;
            try
            {
                await SqLiteDataService.Db.UpdateAsync(UpdateUser);
                return UpdateUser;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
