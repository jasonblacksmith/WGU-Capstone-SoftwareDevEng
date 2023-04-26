using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
            await Db.CreateTablesAsync<FileCollection, Proceedure, Question, Relapse, Result>();
            await Db.CreateTablesAsync<Symptom, SymptomCollection, Model.Trigger, TriggerCollection, User>();
            await Db.CreateTablesAsync<Visit, VisitType>();

            Debug.WriteLine("Tables Created");
        }

        public static async Task BurnAndRebuild()
        {
            await Db.DropTableAsync<Address>();
            await Db.DropTableAsync<Appointment>();
            await Db.DropTableAsync<AppointmentState>();
            await Db.DropTableAsync<DoctorsNote>();
            await Db.DropTableAsync<Model.File>();
            await Db.DropTableAsync<FileCollection>();
            await Db.DropTableAsync<Proceedure>();
            await Db.DropTableAsync<Question>();
            await Db.DropTableAsync<Relapse>();
            await Db.DropTableAsync<Result>();
            await Db.DropTableAsync<Symptom>();
            await Db.DropTableAsync<SymptomCollection>();
            await Db.DropTableAsync<Model.Trigger>();
            await Db.DropTableAsync<TriggerCollection>();
            await Db.DropTableAsync<User>();
            await Db.DropTableAsync<Visit>();
            await Db.DropTableAsync<VisitType>();

            Debug.WriteLine("Tables Burned");

            await Db.CreateTablesAsync<Address, Appointment, AppointmentState, DoctorsNote, Model.File>();
            await Db.CreateTablesAsync<FileCollection, Proceedure, Question, Relapse, Result>();
            await Db.CreateTablesAsync<Symptom, SymptomCollection, Model.Trigger, TriggerCollection, User>();
            await Db.CreateTablesAsync<Visit, VisitType>();

            Debug.WriteLine("Tables Rebuilt");
        }
    }
}
