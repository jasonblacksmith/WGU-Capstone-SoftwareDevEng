using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class SymptomCollectionCalls : ISymptomCollectionCalls
    {
        public SymptomCollection symptomCollection;
        public ObservableCollection<SymptomCollection> symptomCollections = new();

        //Creates and adds new SymptomCollection record to DB
        public async Task<SymptomCollection> AddSymptomCollectionAsync(SymptomCollection symptomCollection)
        {
            SymptomCollection AddSymptomCollection = symptomCollection;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddSymptomCollection);
                return AddSymptomCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired SymptomCollection record from the DB
        public async Task<SymptomCollection> GetSymptomCollectionAsync(int pk)
        {
            try
            {
                SymptomCollection GetSymptomCollection = await SqLiteDataService.Db.GetAsync<SymptomCollection>(pk);
                return GetSymptomCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all SymptomCollections int the table
        public async Task<ObservableCollection<SymptomCollection>> GetSymptomCollectionsAsync()
        {
            List<SymptomCollection> SymptomCollections = await SqLiteDataService.Db.Table<SymptomCollection>().ToListAsync();
            foreach (SymptomCollection SymptomCollection in SymptomCollections)
            {
                symptomCollections.Add(SymptomCollection);
            }
            return symptomCollections;
        }

        //Removes or Deletes the desired SymptomCollection record from the DB
        public async Task<bool> RemoveSymptomCollectionAsync(SymptomCollection symptomCollection)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<SymptomCollection>(symptomCollection.SymptomCollectionId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired SymptomCollection in the SymptomCollection table in the DB
        public async Task<SymptomCollection> UpdateSymptomCollectionAsync(SymptomCollection symptomCollection)
        {
            SymptomCollection UpdateSymptomCollection = symptomCollection;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateSymptomCollection);
                return UpdateSymptomCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
