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
        public Proceedure proceedure = new();
        public ObservableCollection<Proceedure> proceedures = new();

        //Creates and adds new Proceedure record to DB
        public async Task<Proceedure> AddProceedureAsync(Proceedure proceedure)
        {
            this.proceedure = proceedure;
            try
            {
                await SqLiteDataService.Db.InsertAsync(this.proceedure);
                return this.proceedure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        //Returns the desired Proceedure record from the DB
        public async Task<Proceedure> GetProceedureAsync(int pk)
        {
            try
            {
                this.proceedure = await SqLiteDataService.Db.GetAsync<Proceedure>(pk);
                return this.proceedure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        //Returns an ObservableCollection of all Proceedures int the table
        public async Task<ObservableCollection<Proceedure>> GetProceeduresAsync()
        {
            try
            {
                List<Proceedure> Proceedures = new();

                proceedures.Clear();
                Proceedures.Clear();

                Proceedures = await SqLiteDataService.Db.Table<Proceedure>().ToListAsync();
                foreach (Proceedure Proceedure in Proceedures)
                {
                    proceedures.Add(Proceedure);
                }
                return proceedures;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        //Removes or Deletes the desired Proceedure record from the DB
        public async Task<bool> RemoveProceedureAsync(Proceedure proceedure)
        {
            try
            {
                await SqLiteDataService.Db.DeleteAsync<Proceedure>(proceedure.ProceedureId);

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
            this.proceedure = proceedure;
            try
            {
                await SqLiteDataService.Db.UpdateAsync(this.proceedure);
                return this.proceedure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
