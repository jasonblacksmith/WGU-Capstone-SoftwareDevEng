namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IUserCalls
    {
        Task<User> AddUserAsync(User user);
        Task<bool> RemoveUserAsync(User user);
        Task<User> GetUserAsync(int pk);
        Task<User> UpdateUserAsync(User user);
        Task<ObservableCollection<User>> GetUsersAsync();
    }
}
