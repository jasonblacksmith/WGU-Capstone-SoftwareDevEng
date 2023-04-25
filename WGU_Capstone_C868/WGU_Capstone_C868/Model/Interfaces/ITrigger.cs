namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface ITrigger
    {
        string Description { get; set; }
        bool IsNew { get; set; }
        string Title { get; set; }
        int TriggerCollectionId { get; set; }
        int TriggerId { get; set; }
    }
}