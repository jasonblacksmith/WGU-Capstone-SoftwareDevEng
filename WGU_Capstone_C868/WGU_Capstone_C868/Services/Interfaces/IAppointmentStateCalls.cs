namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IAppointmentStateCalls
    {
        Task<AppointmentState> GetAppointmentStateAsync(int pk);
        Task<ObservableCollection<AppointmentState>> GetAppointmentStatesAsync();
    }
}
