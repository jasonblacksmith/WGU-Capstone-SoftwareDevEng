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
        internal static VisitType visitType = new();
        internal static VisitType visitType1 = new();

        internal static Proceedure proceedure = new();
        internal static Proceedure proceedure1 = new();

        private static ProceedureCalls ProceedureCalls = new();
        private static ObservableCollection<Proceedure> Proceedures = new();

        private static VisitTypeCalls VisitTypeCalls = new();
        private static ObservableCollection<VisitType> VisitTypes = new();

        public static async Task Init() 
        {
            await AddProceedures();
            await AddVisitTypes();
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

        private static async Task AddVisitTypes()
        {
            VisitTypes = await VisitTypeCalls.GetVisitTypesAsync();

            if (VisitTypes.Count == 0)
            {
                visitType.VisitTypeId = 1;
                visitType.Title = "Check Up";
                VisitTypes.Add(visitType);
                visitType1.VisitTypeId = 2;
                visitType1.Title = "Results Follow Up";
                VisitTypes.Add(visitType1);

                await SqLiteDataService.Db.InsertAllAsync(VisitTypes);
            }

            Debug.WriteLine("Static Data Loaded");
        }
    }
}
