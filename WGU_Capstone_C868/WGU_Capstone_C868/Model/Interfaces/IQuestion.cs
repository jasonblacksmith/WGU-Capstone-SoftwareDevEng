namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IQuestion
    {
        string QuestionContent { get; set; }
        int QuestionId { get; set; }
        int UserId { get; set; }
        int VisitId { get; set; }
    }
}