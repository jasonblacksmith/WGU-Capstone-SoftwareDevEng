namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IVisitTypeCalls
    {
        Task<VisitType> GetVisitTypeAsync(int pK);
        Task<ObservableCollection<VisitType>> GetVisitTypesAsync();
    }
}
