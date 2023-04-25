namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IDoctorsNote
    {
        string Content { get; set; }
        string DoctorsName { get; set; }
        int DoctorsNoteId { get; set; }
        int? ResultsId { get; set; }
        string Title { get; set; }
        int VisitId { get; set; }
        bool WithResults { get; set; }
    }
}