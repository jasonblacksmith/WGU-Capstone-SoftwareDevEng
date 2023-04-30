namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface ITriggerCalls
    {
        Task<Model.Trigger> AddTriggerAsync(Model.Trigger trigger);
        Task<bool> RemoveTriggerAsync(Model.Trigger trigger);
        Task<Model.Trigger> GetTriggerAsync(int pk);
        Task<Model.Trigger> UpdateTriggerAsync(Model.Trigger trigger);
        Task<ObservableCollection<Model.Trigger>> GetTriggersAsync();
    }
}
