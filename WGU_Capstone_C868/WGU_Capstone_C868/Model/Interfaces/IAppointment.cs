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
        int StateId { get; set; }
        bool Current { get; set; }
        int UserId { get; set; }
    }
}