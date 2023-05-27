using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    internal class RelapseCalls : IRelapseCalls
    {
        public Relapse relapse = new();
        public ObservableCollection<Relapse> relapses = new();

        //Creates and adds new Relapse record to DB
        public async Task<Relapse> AddRelapseAsync(Relapse relapse)
        {
            Relapse AddRelapse = relapse;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddRelapse);
                return AddRelapse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Relapse record from the DB
        public async Task<Relapse> GetRelapseAsync(int pk)
        {
            try
            {
                Relapse GetRelapse = await SqLiteDataService.Db.GetAsync<Relapse>(pk);
                return GetRelapse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Relapses int the table
        public async Task<ObservableCollection<Relapse>> GetRelapsesAsync()
        {
            List<Relapse> Relapses = new();

            relapses.Clear();
            Relapses.Clear();

            Relapses = await SqLiteDataService.Db.Table<Relapse>().ToListAsync();
            foreach (Relapse Relapse in Relapses) 
            {
                relapses.Add(Relapse);
            }
            return relapses;
        }

        //Removes or Deletes the desired Relapse record from the DB
        public async Task<bool> RemoveRelapseAsync(Relapse relapse)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Relapse>(relapse.RelapseId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Relapse in the Relapse table in the DB
        public async Task<Relapse> UpdateRelapseAsync(Relapse relapse)
        {
            Relapse UpdateRelapse = relapse;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateRelapse);
                return UpdateRelapse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
