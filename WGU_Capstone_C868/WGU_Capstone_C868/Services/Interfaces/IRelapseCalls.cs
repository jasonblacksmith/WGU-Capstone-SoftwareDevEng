namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IRelapseCalls
    {
        Task<Relapse> AddRelapseAsync(Relapse relapse);
        Task<bool> RemoveRelapseAsync(Relapse relapse);
        Task<Relapse> GetRelapseAsync(int pk);
        Task<Relapse> UpdateRelapseAsync(Relapse relapse);
        Task<ObservableCollection<Relapse>> GetRelapsesAsync();
    }
}
