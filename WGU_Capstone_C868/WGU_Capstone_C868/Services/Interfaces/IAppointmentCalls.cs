namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IAppointmentCalls
    {
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<bool> RemoveAppointmentAsync(Appointment appointment);
        Task<Appointment> GetAppointmentAsync(int pk);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<ObservableCollection<Appointment>> GetAppointmentsAsync();
    }
}