namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IVisit
    {
        DateTime DateAndTime { get; set; }
        int UserId { get; set; }
        int VisitId { get; set; }
        int VisitTypeId { get; set; }
    }
}