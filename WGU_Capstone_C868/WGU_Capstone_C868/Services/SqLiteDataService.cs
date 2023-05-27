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

        public static async Task Init()
        {
            if (Db != null)
            {
                return;
            }

            Db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath
           (Environment.SpecialFolder.Personal), "MSTrackerAppDb.db0"));

            await Db.CreateTablesAsync<Address, Appointment>();
            await Db.CreateTablesAsync<Proceedure, Relapse>();
            await Db.CreateTablesAsync<Symptom, Model.Trigger, User>();

            Debug.WriteLine("Tables Created");
        }

        public static async Task BurnAndRebuild()
        {
            await Db.DropTableAsync<Address>();
            await Db.DropTableAsync<Appointment>();
            await Db.DropTableAsync<Proceedure>();
            await Db.DropTableAsync<Relapse>();
            await Db.DropTableAsync<Symptom>();
            await Db.DropTableAsync<Model.Trigger>();
            await Db.DropTableAsync<User>();

            Debug.WriteLine("Tables Burned");

            await Db.CreateTablesAsync<Address, Appointment>();
            await Db.CreateTablesAsync<Proceedure, Relapse>();
            await Db.CreateTablesAsync<Symptom, Model.Trigger, User>();

            Debug.WriteLine("Tables Recreated");
        }
    }
}
