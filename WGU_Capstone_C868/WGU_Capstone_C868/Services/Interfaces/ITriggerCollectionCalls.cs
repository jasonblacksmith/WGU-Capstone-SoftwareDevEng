namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface ITriggerCollectionCalls
    {
        Task<TriggerCollection> AddTriggerCollectionAsync(TriggerCollection triggerCollection);
        Task<bool> RemoveTriggerCollectionAsync(TriggerCollection triggerCollection);
        Task<TriggerCollection> GetTriggerCollectionAsync(int pk);
        Task<TriggerCollection> UpdateTriggerCollectionAsync(TriggerCollection triggerCollection);
        Task<ObservableCollection<TriggerCollection>> GetTriggerCollectionsAsync();
    }
}