using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services;
using WGU_Capstone_C868.Model;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class AddressCalls : IAddressCalls
    {
        public Address address = new();
        public ObservableCollection<Address> addresses = new();

        //Creates and adds new Address record to DB
        public async Task<Address> AddAddressAsync(Address address)
        {
            this.address = address;
            try
            {
                await SqLiteDataService.Db.InsertAsync(this.address);
                return this.address;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        //Returns the desired Address record from the DB
        public async Task<Address> GetAddressAsync(int pk)
        {
            try
            {
                address = await SqLiteDataService.Db.GetAsync<Address>(pk);
                return address;
            } 
            catch (Exception ex)
            { 
                Debug.WriteLine(ex);
                throw;
            }
        }

        //Returns an ObservableCollection of all Addresses int the table
        public async Task<ObservableCollection<Address>> GetAddressesAsync()
        {
            try
            {
                List<Address> Addresses = new();

                addresses.Clear();
                Addresses.Clear();

                Addresses = await SqLiteDataService.Db.Table<Address>().ToListAsync();
                foreach (Address Address in Addresses)
                {
                    addresses.Add(Address);
                }
                return addresses;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        //Removes or Deletes the desired Address record from the DB
        public async Task<bool> RemoveAddressAsync(Address address)
        {
            try
            {
                await SqLiteDataService.Db.DeleteAsync<Address>(address.AddressId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Address in the Address table in the DB
        public async Task<Address> UpdateAddressAsync(Address address)
        {
            Address UpdateAddress = new();
            UpdateAddress = address;
            try
            {
                await SqLiteDataService.Db.UpdateAsync(UpdateAddress);
                return UpdateAddress;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
