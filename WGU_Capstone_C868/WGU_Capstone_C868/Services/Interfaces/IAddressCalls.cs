namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IAddressCalls
    {
        Task<Address> AddAddressAsync(Address address);
        Task<bool> RemoveAddressAsync(Address address);
        Task<Address> GetAddressAsync(int pk);
        Task<Address> UpdateAddressAsync(Address address);
        Task<ObservableCollection<Address>> GetAddressesAsync();
    }
}