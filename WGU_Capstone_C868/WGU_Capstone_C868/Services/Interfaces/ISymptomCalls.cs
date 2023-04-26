namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface ISymptomCalls
    {
        Task<Symptom> AddSymptomAsync(Symptom symptom);
        Task<bool> RemoveSymptomAsync(Symptom symptom);
        Task<Symptom> GetSymptomAsync(int pk);
        Task<Symptom> UpdateSymptomAsync(Symptom symptom);
        Task<ObservableCollection<Symptom>> GetSymptomsAsync();
    }
}
