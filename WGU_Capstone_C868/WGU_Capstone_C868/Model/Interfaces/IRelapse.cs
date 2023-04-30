namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IRelapse
    {
        DateTime DateAndTime { get; set; }
        string Location { get; set; }
        int RelapseId { get; set; }
        int SymptomCollectionId { get; set; }
        int TriggersCollectionId { get; set; }
        int UserId { get; set; }
    }
}