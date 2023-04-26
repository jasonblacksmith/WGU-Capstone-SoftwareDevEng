namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IProceedureCalls
    {
        Task<Proceedure> GetProceedureAsync(int pk);
        Task<ObservableCollection<Proceedure>> GetProceeduresAsync();
    }
}
