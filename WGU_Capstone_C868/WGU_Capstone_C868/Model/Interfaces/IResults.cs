namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IResults
    {
        int AppointmentId { get; set; }
        int DoctorsNoteId { get; set; }
        int FileCollectionId { get; set; }
        string OtherNotes { get; set; }
        int ProceedureId { get; set; }
        int ResultsId { get; set; }
        int UserId { get; set; }
    }
}