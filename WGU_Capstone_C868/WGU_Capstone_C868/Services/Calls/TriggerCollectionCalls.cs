using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class TriggerCollectionCalls : ITriggerCollectionCalls
    {
        public TriggerCollection triggerCollection;
        public ObservableCollection<TriggerCollection> triggerCollections = new();

        //Creates and adds new TriggerCollection record to DB
        public async Task<TriggerCollection> AddTriggerCollectionAsync(TriggerCollection triggerCollection)
        {
            TriggerCollection AddTriggerCollection = triggerCollection;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddTriggerCollection);
                return AddTriggerCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired TriggerCollection record from the DB
        public async Task<TriggerCollection> GetTriggerCollectionAsync(int pk)
        {
            try
            {
                TriggerCollection GetTriggerCollection = await SqLiteDataService.Db.GetAsync<TriggerCollection>(pk);
                return GetTriggerCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all TriggerCollections int the table
        public async Task<ObservableCollection<TriggerCollection>> GetTriggerCollectionsAsync()
        {
            List<TriggerCollection> TriggerCollections = await SqLiteDataService.Db.Table<TriggerCollection>().ToListAsync();
            foreach (TriggerCollection TriggerCollection in TriggerCollections)
            {
                triggerCollections.Add(TriggerCollection);
            }
            return triggerCollections;
        }

        //Removes or Deletes the desired TriggerCollection record from the DB
        public async Task<bool> RemoveTriggerCollectionAsync(TriggerCollection triggerCollection)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<TriggerCollection>(triggerCollection.TriggerCollectionId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired TriggerCollection in the TriggerCollection table in the DB
        public async Task<TriggerCollection> UpdateTriggerCollectionAsync(TriggerCollection triggerCollection)
        {
            TriggerCollection UpdateTriggerCollection = triggerCollection;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateTriggerCollection);
                return UpdateTriggerCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
