namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IFileCalls
    {
        Task<Model.File> AddFileAsync(Model.File file);
        Task<bool> RemoveFileAsync(Model.File file);
        Task<Model.File> GetFileAsync(int pk);
        Task<Model.File> UpdateFileAsync(Model.File file);
        Task<ObservableCollection<Model.File>> GetFilesAsync();
    }
}
