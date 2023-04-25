namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface ISymptom
    {
        string Description { get; set; }
        bool IsNew { get; set; }
        int SymptomCollectionId { get; set; }
        int SymptomId { get; set; }
        string Title { get; set; }
    }
}