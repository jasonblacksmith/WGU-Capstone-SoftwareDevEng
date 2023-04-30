using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    internal class ResultCalls : IResultCalls
    {
        public Result result;
        public ObservableCollection<Result> results = new();

        //Creates and adds new Result record to DB
        public async Task<Result> AddResultAsync(Result result)
        {
            Result AddResult = result;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddResult);
                return AddResult;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Result record from the DB
        public async Task<Result> GetResultAsync(int pk)
        {
            try
            {
                Result GetResult = await SqLiteDataService.Db.GetAsync<Result>(pk);
                return GetResult;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Results int the table
        public async Task<ObservableCollection<Result>> GetResultsAsync()
        {
            List<Result> Results = await SqLiteDataService.Db.Table<Result>().ToListAsync();
            foreach (Result Result in Results)
            {
                results.Add(Result);
            }
            return results;
        }

        //Removes or Deletes the desired Result record from the DB
        public async Task<bool> RemoveResultAsync(Result result)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Result>(result.ResultId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Result in the Result table in the DB
        public async Task<Result> UpdateResultAsync(Result result)
        {
            Result UpdateResult = result;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateResult);
                return UpdateResult;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
