namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IDoctorsNoteCalls
    {
        Task<DoctorsNote> AddDoctorsNoteAsync(DoctorsNote doctorsNote);
        Task<bool> RemoveDoctorsNoteAsync(DoctorsNote doctorsNote);
        Task<DoctorsNote> GetDoctorsNoteAsync(int pk);
        Task<DoctorsNote> UpdateDoctorsNoteAsync(DoctorsNote doctorsNote);
        Task<ObservableCollection<DoctorsNote>> GetDoctorsNotesAsync();
    }
}
