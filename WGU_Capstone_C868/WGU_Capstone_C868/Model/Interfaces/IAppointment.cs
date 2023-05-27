namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IAppointment
    {
        int AppointmentId { get; set; }
        int ProceedureId { get; set; }
        string LocationName { get; set; }
        int AddressId { get; set; }
        string Notes { get; set; }
        string PhoneNumber { get; set; }
        bool Current { get; set; }
        DateTime DateAndTime { get; set; }
        int UserId { get; set; }
    }
}