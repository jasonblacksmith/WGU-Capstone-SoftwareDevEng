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

        public DoctorsNote doctorsNote = new();
        public ObservableCollection<DoctorsNote> doctorsNotes = new();

        //Creates and adds new DoctorsNote record to DB
        public async Task<DoctorsNote> AddDoctorsNoteAsync(DoctorsNote doctorsNote)
        {
            DoctorsNote AddDoctorsNote = doctorsNote;
            try
            {
                await SqLiteDataService.Db.InsertAsync(AddDoctorsNote);
                return AddDoctorsNote;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired DoctorsNote record from the DB
        public async Task<DoctorsNote> GetDoctorsNoteAsync(int pk)
        {
            DoctorsNote doctorsNote = new();
            try
            {
                doctorsNote = await SqLiteDataService.Db.GetAsync<DoctorsNote>(pk);
                return doctorsNote;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all DoctorsNotes int the table
        public async Task<ObservableCollection<DoctorsNote>> GetDoctorsNotesAsync()
        {
            try
            {
                List<DoctorsNote> DoctorsNotes = new();
                DoctorsNotes = await SqLiteDataService.Db.Table<DoctorsNote>().ToListAsync();
                foreach (DoctorsNote doctorsNote in DoctorsNotes)
                {
                    doctorsNotes.Add(doctorsNote);
                }
                return doctorsNotes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        //Removes or Deletes the desired DoctorsNote record from the DB
        public async Task<bool> RemoveDoctorsNoteAsync(DoctorsNote doctorsNote)
        {
            try
            {
                await SqLiteDataService.Db.DeleteAsync<DoctorsNote>(doctorsNote.DoctorsNoteId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired DoctorsNote in the DoctorsNote table in the DB
        public async Task<DoctorsNote> UpdateDoctorsNoteAsync(DoctorsNote doctorsNote)
        {
            DoctorsNote UpdateDoctorsNote = doctorsNote;
            try
            {
                await SqLiteDataService.Db.UpdateAsync(UpdateDoctorsNote);
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
