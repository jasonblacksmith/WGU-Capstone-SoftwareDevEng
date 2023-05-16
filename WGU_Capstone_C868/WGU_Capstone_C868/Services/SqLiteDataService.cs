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

        private static List<AppointmentState> AppointmentStates = new();

        private static List<Proceedure> Proceedures = new();

        internal static Proceedure proceedure;

        internal static AppointmentState appointmentState;

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

            appointmentState = new AppointmentState();
            appointmentState.StateId = 1;
            appointmentState.Name = "Active";
            AppointmentStates.Add(appointmentState);
            appointmentState.StateId = 2;
            appointmentState.Name = "Canceled";
            AppointmentStates.Add(appointmentState);
            appointmentState.StateId = 3;
            appointmentState.Name = "Complete";
            AppointmentStates.Add(appointmentState);

            await Db.InsertAllAsync(AppointmentStates);

            proceedure = new Proceedure();
            proceedure.ProceedureId = 1;
            proceedure.Title = "MRI";
            Proceedures.Add(proceedure);
            proceedure.ProceedureId = 2;
            proceedure.Title = "Labs";

            await Db.InsertAllAsync(Proceedures);

            Debug.WriteLine("Static Data Loaded");
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

        //TODO: Populate static lists to db AppointmentState, Proceedure
    }
}
