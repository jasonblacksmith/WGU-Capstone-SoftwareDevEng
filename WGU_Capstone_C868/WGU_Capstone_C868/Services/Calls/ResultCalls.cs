using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGU_Capstone_C868.Services.Calls
{
    internal class ResultCalls
    {
        public Address address;
        public ObservableCollection<Address> addresses = new();

        //Creates and adds new Address record to DB
        public async Task<Address> AddAddressAsync(Address address)
        {
            Address AddAddress = address;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddAddress);
                return AddAddress;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Address record from the DB
        public async Task<Address> GetAddressAsync(int pk)
        {
            try
            {
                Address GetAddress = await SqLiteDataService.Db.GetAsync<Address>(pk);
                return GetAddress;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Addresses int the table
        public async Task<ObservableCollection<Address>> GetAddressesAsync()
        {
            List<Address> Addresses = await SqLiteDataService.Db.Table<Address>().ToListAsync();
            foreach (Address Address in Addresses)
            {
                addresses.Add(Address);
            }
            return addresses;
        }

        //Removes or Deletes the desired Address record from the DB
        public async Task<bool> RemoveAddressAsync(Address address)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Address>(address.AddressId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Address in the Address table in the DB
        public async Task<Address> UpdateAddressAsync(Address address)
        {
            Address UpdateAddress = address;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateAddress);
                return UpdateAddress;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
