namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface ISymptomCollectionCalls
    {
        Task<SymptomCollection> AddSymptomCollectionAsync(SymptomCollection symptomCollection);
        Task<bool> RemoveSymptomCollectionAsync(SymptomCollection symptomCollection);
        Task<SymptomCollection> GetSymptomCollectionAsync(int pk);
        Task<SymptomCollection> UpdateSymptomCollectionAsync(SymptomCollection symptomCollection);
        Task<ObservableCollection<SymptomCollection>> GetSymptomCollectionsAsync();
    }
}
