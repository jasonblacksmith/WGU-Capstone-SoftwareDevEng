using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        private static List<Proceedure> Proceedures = new();

        private static List<VisitType> VisitTypes = new();

        internal static VisitType visitType = new();
        internal static VisitType visitType1 = new();

        internal static Proceedure proceedure = new();
        internal static Proceedure proceedure1 = new();

        public static async Task Init()
        {
            if (Db != null)
            {
                return;
            }

            Db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath
           (Environment.SpecialFolder.Personal), "MSTrackerAppDb.db0"));

            await Db.CreateTablesAsync<Address, Appointment, DoctorsNote, Model.File>();
            await Db.CreateTablesAsync<FileCollection, Proceedure, Question, Relapse, Result>();
            await Db.CreateTablesAsync<Symptom, SymptomCollection, Model.Trigger, TriggerCollection, User>();
            await Db.CreateTablesAsync<Visit, VisitType>();

            Debug.WriteLine("Tables Created");
            
        #region StaticDataInserts

            proceedure.ProceedureId = 1;
            proceedure.Title = "MRI";
            Proceedures.Add(proceedure);
            proceedure1.ProceedureId = 2;
            proceedure1.Title = "Labs";
            Proceedures.Add(proceedure1);

            await Db.InsertAllAsync(Proceedures);

            visitType.VisitTypeId = 1;
            visitType.Title = "Check Up";
            VisitTypes.Add(visitType);
            visitType1.VisitTypeId = 2;
            visitType1.Title = "Results Follow Up";
            VisitTypes.Add(visitType1);

            await Db.InsertAllAsync(VisitTypes);

            Debug.WriteLine("Static Data Loaded");
        #endregion
        }

        public static async Task BurnAndRebuild()
        {
            await Db.DropTableAsync<Address>();
            await Db.DropTableAsync<Appointment>();
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

            await Db.CreateTablesAsync<Address, Appointment, DoctorsNote, Model.File>();
            await Db.CreateTablesAsync<FileCollection, Proceedure, Question, Relapse, Result>();
            await Db.CreateTablesAsync<Symptom, SymptomCollection, Model.Trigger, TriggerCollection, User>();
            await Db.CreateTablesAsync<Visit, VisitType>();

            Debug.WriteLine("Tables Recreated");

            #region StaticDataInserts

            proceedure.ProceedureId = 1;
            proceedure.Title = "MRI";
            Proceedures.Add(proceedure);
            proceedure1.ProceedureId = 2;
            proceedure1.Title = "Labs";
            Proceedures.Add(proceedure1);

            await Db.InsertAllAsync(Proceedures);

            visitType.VisitTypeId = 1;
            visitType.Title = "Check Up";
            VisitTypes.Add(visitType);
            visitType1.VisitTypeId = 2;
            visitType1.Title = "Results Follow Up";
            VisitTypes.Add(visitType1);

            await Db.InsertAllAsync(VisitTypes);

            Debug.WriteLine("Static Data Loaded");
            #endregion
        }
    }
}
