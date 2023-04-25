using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model;

namespace WGU_Capstone_C868.Services
{
    public static class SqLiteDataService
    {
        public static SQLiteAsyncConnection Db;

        public static async Task Init()
        {
            if (Db != null)
            {
                return;
            }

            Db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath
           (Environment.SpecialFolder.Personal), "MSTrackerAppDb.db0"));

            await Db.CreateTablesAsync<Address, Appointment, AppointmentState, DoctorsNote, Model.File>();
            await Db.CreateTablesAsync<FileCollection, Proceedure, Questions, Relapse, Results>();
            await Db.CreateTablesAsync<Symptom, SymptomCollection, Model.Trigger, TriggerCollection, User>();
            await Db.CreateTablesAsync<Visit, VisitType>();

            //TODO: Finish Service Layer!!!
        }
    }
}
