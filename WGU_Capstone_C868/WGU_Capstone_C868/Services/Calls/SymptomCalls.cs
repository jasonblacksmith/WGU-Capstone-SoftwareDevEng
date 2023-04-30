using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class SymptomCalls : ISymptomCalls
    {
        public Symptom symptom;
        public ObservableCollection<Symptom> symptoms = new();

        //Creates and adds new Symptom record to DB
        public async Task<Symptom> AddSymptomAsync(Symptom symptom)
        {
            Symptom AddSymptom = symptom;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddSymptom);
                return AddSymptom;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Symptom record from the DB
        public async Task<Symptom> GetSymptomAsync(int pk)
        {
            try
            {
                Symptom GetSymptom = await SqLiteDataService.Db.GetAsync<Symptom>(pk);
                return GetSymptom;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Symptoms int the table
        public async Task<ObservableCollection<Symptom>> GetSymptomsAsync()
        {
            List<Symptom> Symptoms = await SqLiteDataService.Db.Table<Symptom>().ToListAsync();
            foreach (Symptom Symptom in Symptoms)
            {
                symptoms.Add(Symptom);
            }
            return symptoms;
        }

        //Removes or Deletes the desired Symptom record from the DB
        public async Task<bool> RemoveSymptomAsync(Symptom symptom)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Symptom>(symptom.SymptomId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Symptom in the Symptom table in the DB
        public async Task<Symptom> UpdateSymptomAsync(Symptom symptom)
        {
            Symptom UpdateSymptom = symptom;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateSymptom);
                return UpdateSymptom;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
