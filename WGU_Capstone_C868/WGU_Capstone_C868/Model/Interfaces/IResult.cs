namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IResult
    {
        int AppointmentId { get; set; }
        int DoctorsNoteId { get; set; }
        int FileCollectionId { get; set; }
        string OtherNotes { get; set; }
        int ProceedureId { get; set; }
        int ResultId { get; set; }
        int UserId { get; set; }
    }
}