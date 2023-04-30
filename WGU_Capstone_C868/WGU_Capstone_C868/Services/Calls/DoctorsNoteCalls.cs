using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class DoctorsNoteCalls : IDoctorsNoteCalls
    {

        public DoctorsNote doctorsNote;
        public ObservableCollection<DoctorsNote> doctorsNotes = new();

        //Creates and adds new Appointment record to DB
        public async Task<DoctorsNote> AddDoctorsNoteAsync(DoctorsNote doctorsNote)
        {
            DoctorsNote AddDoctorsNote = doctorsNote;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddDoctorsNote);
                return AddDoctorsNote;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Appointment record from the DB
        public async Task<DoctorsNote> GetDoctorsNoteAsync(int pk)
        {
            try
            {
                DoctorsNote  GetDoctorsNote = await SqLiteDataService.Db.GetAsync<DoctorsNote>(pk);
                return GetDoctorsNote;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Addresses int the table
        public async Task<ObservableCollection<DoctorsNote>> GetDoctorsNotesAsync()
        {
            List<DoctorsNote> DoctorsNotes = await SqLiteDataService.Db.Table<DoctorsNote>().ToListAsync();
            foreach (DoctorsNote doctorsNote in DoctorsNotes)
            {
                doctorsNotes.Add(doctorsNote);
            }
            return doctorsNotes;
        }

        //Removes or Deletes the desired Address record from the DB
        public async Task<bool> RemoveDoctorsNoteAsync(DoctorsNote doctorsNote)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<DoctorsNote>(doctorsNote.DoctorsNoteId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Address in the Address table in the DB
        public async Task<DoctorsNote> UpdateDoctorsNoteAsync(DoctorsNote doctorsNote)
        {
            DoctorsNote UpdateDoctorsNote = doctorsNote;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateDoctorsNote);
                return UpdateDoctorsNote;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

    }
}
