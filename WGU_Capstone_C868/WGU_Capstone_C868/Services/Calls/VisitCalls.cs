using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class VisitCalls : IVisitCalls
    {
        public Visit visit;
        public ObservableCollection<Visit> visits = new();

        //Creates and adds new Visit record to DB
        public async Task<Visit> AddVisitAsync(Visit visit)
        {
            Visit AddVisit = visit;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddVisit);
                return AddVisit;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Visit record from the DB
        public async Task<Visit> GetVisitAsync(int pk)
        {
            try
            {
                Visit GetVisit = await SqLiteDataService.Db.GetAsync<Visit>(pk);
                return GetVisit;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Visits int the table
        public async Task<ObservableCollection<Visit>> GetVisitsAsync()
        {
            List<Visit> Visits = await SqLiteDataService.Db.Table<Visit>().ToListAsync();
            foreach (Visit Visit in Visits)
            {
                visits.Add(Visit);
            }
            return visits;
        }

        //Removes or Deletes the desired Visit record from the DB
        public async Task<bool> RemoveVisitAsync(Visit visit)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Visit>(visit.VisitId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Visit in the Visit table in the DB
        public async Task<Visit> UpdateVisitAsync(Visit visit)
        {
            Visit UpdateVisit = visit;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateVisit);
                return UpdateVisit;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
