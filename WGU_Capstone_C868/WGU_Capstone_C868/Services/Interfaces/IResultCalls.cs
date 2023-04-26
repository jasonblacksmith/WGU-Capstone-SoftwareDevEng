namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IResultCalls
    {
        Task<Result> AddResultAsync(Result result);
        Task<bool> RemoveResultAsync(Result result);
        Task<Result> GetResultAsync(int pk);
        Task<Result> UpdateResultAsync(Result result);
        Task<ObservableCollection<Result>> GetResultsAsync();
    }
}
