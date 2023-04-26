namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IFileCollectionCalls
    {
        Task<FileCollection> AddFileCollectionAsync(FileCollection fileCollection);
        Task<bool> RemoveFileCollectionAsync(FileCollection fileCollection);
        Task<FileCollection> GetFileCollectionAsync(int pk);
        Task<FileCollection> UpdateFileCollectionAsync(FileCollection fileCollection);
        Task<ObservableCollection<FileCollection>> GetFileCollectionsAsync();
    }
}
