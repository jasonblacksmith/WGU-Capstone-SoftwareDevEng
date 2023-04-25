namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IAppointmentState
    {
        public int StateId { get; set; }
        public bool Active { get; set; }
        public bool Cancelled { get; set; }
        public bool Rescheduled { get; set; }
        public bool Complete { get; set; }
    }
}