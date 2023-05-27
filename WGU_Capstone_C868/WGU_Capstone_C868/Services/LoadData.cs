using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.Services
{
    internal class LoadData
    {
        internal static Proceedure proceedure = new();
        internal static Proceedure proceedure1 = new();

        private static ProceedureCalls ProceedureCalls = new();
        private static ObservableCollection<Proceedure> Proceedures = new();

        public static async Task Init() 
        {
            await AddProceedures();
            Debug.WriteLine("Static Data Loaded");
        }

        private static async Task AddProceedures()
        {
            Proceedures = await ProceedureCalls.GetProceeduresAsync();

            if (Proceedures.Count == 0)
            {
                proceedure.ProceedureId = 1;
                proceedure.Title = "MRI";
                Proceedures.Add(proceedure);
                proceedure1.ProceedureId = 2;
                proceedure1.Title = "Labs";
                Proceedures.Add(proceedure1);

                await SqLiteDataService.Db.InsertAllAsync(Proceedures);
            }
        }
    }
}
