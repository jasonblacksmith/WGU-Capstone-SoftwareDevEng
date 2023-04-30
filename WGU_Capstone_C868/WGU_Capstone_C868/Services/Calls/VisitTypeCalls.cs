using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class VisitTypeCalls : IVisitTypeCalls
    {
        public VisitType visitType;
        public ObservableCollection<VisitType> visitTypes = new();

        //Creates and adds new VisitType record to DB
        public async Task<VisitType> AddVisitTypeAsync(VisitType visitType)
        {
            VisitType AddVisitType = visitType;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddVisitType);
                return AddVisitType;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired VisitType record from the DB
        public async Task<VisitType> GetVisitTypeAsync(int pk)
        {
            try
            {
                VisitType GetVisitType = await SqLiteDataService.Db.GetAsync<VisitType>(pk);
                return GetVisitType;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all VisitTypes int the table
        public async Task<ObservableCollection<VisitType>> GetVisitTypesAsync()
        {
            List<VisitType> VisitTypes = await SqLiteDataService.Db.Table<VisitType>().ToListAsync();
            foreach (VisitType VisitType in VisitTypes)
            {
                visitTypes.Add(VisitType);
            }
            return visitTypes;
        }

        //Removes or Deletes the desired VisitType record from the DB
        public async Task<bool> RemoveVisitTypeAsync(VisitType visitType)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<VisitType>(visitType.VisitTypeId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired VisitType in the VisitType table in the DB
        public async Task<VisitType> UpdateVisitTypeAsync(VisitType visitType)
        {
            VisitType UpdateVisitType = visitType;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateVisitType);
                return UpdateVisitType;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
