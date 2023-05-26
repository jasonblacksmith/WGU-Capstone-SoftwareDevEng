using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class TriggerCalls : ITriggerCalls
    {
        public Model.Trigger trigger;
        public ObservableCollection<Model.Trigger> triggers = new();

        //Creates and adds new Trigger record to DB
        public async Task<Model.Trigger> AddTriggerAsync(Model.Trigger trigger)
        {
            Model.Trigger AddTrigger = trigger;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddTrigger);
                return AddTrigger;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Model.Trigger record from the DB
        public async Task<Model.Trigger> GetTriggerAsync(int pk)
        {
            try
            {
                Model.Trigger GetTrigger = await SqLiteDataService.Db.GetAsync<Model.Trigger>(pk);
                return GetTrigger;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Model.Triggers int the table
        public async Task<ObservableCollection<Model.Trigger>> GetTriggersAsync()
        {
            List<Model.Trigger> Triggers = new();

            triggers.Clear();
            Triggers.Clear();

            Triggers = await SqLiteDataService.Db.Table<Model.Trigger>().ToListAsync();
            foreach (Model.Trigger Trigger in Triggers)
            {
                triggers.Add(Trigger);
            }
            return triggers;
        }

        //Removes or Deletes the desired Model.Trigger record from the DB
        public async Task<bool> RemoveTriggerAsync(Model.Trigger trigger)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Model.Trigger>(trigger.TriggerId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Model.Trigger in the Model.Trigger table in the DB
        public async Task<Model.Trigger> UpdateTriggerAsync(Model.Trigger trigger)
        {
            Model.Trigger UpdateTrigger = trigger;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateTrigger);
                return UpdateTrigger;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
