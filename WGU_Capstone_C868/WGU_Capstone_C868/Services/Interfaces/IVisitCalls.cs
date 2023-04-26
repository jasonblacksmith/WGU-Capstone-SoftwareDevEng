namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IVisitCalls
    {
        Task<Visit> AddVisitAsync(Visit visit);
        Task<bool> RemoveVisitAsync(Visit visit);
        Task<Visit> GetVisitAsync(int Pk);
        Task<Visit> UpdateVisitAsync(Visit visit);
        Task<ObservableCollection<Visit>> GetVisitsAsync();
    }
}
