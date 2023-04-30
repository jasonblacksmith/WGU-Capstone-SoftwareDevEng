using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class ProceedureCalls : IProceedureCalls
    {
        public Proceedure proceedure;
        public ObservableCollection<Proceedure> proceedures = new();

        //Creates and adds new Proceedure record to DB
        public async Task<Proceedure> AddProceedureAsync(Proceedure proceedure)
        {
            Proceedure AddProceedure = proceedure;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddProceedure);
                return AddProceedure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Proceedure record from the DB
        public async Task<Proceedure> GetProceedureAsync(int pk)
        {
            try
            {
                Proceedure GetProceedure = await SqLiteDataService.Db.GetAsync<Proceedure>(pk);
                return GetProceedure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Proceedures int the table
        public async Task<ObservableCollection<Proceedure>> GetProceeduresAsync()
        {
            List<Proceedure> Proceedures = await SqLiteDataService.Db.Table<Proceedure>().ToListAsync();
            foreach (Proceedure Proceedure in Proceedures)
            {
                proceedures.Add(Proceedure);
            }
            return proceedures;
        }

        //Removes or Deletes the desired Proceedure record from the DB
        public async Task<bool> RemoveProceedureAsync(Proceedure proceedure)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Proceedure>(proceedure.ProceedureId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Proceedure in the Proceedure table in the DB
        public async Task<Proceedure> UpdateProceedureAsync(Proceedure proceedure)
        {
            Proceedure UpdateProceedure = proceedure;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateProceedure);
                return UpdateProceedure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
