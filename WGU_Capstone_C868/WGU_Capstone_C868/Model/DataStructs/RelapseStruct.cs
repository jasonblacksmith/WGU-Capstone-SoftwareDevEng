using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model;

namespace WGU_Capstone_C868.Model.DataStructs
{
    public class RelapseStruct
    {
        public (int, int, int, int, int, int, int) value;
        public string v1;
        public int v2;
        public string v3;
        public int v4;
        public int v5;
        public int v6;

        public Relapse Relapse = new Relapse();

        public RelapseStruct(DateTime dateAndTime, string location, int relapseId, 
            string notes, int symptomCollectionId, int triggerCollectionId, int userId)
        {
            Relapse.DateAndTime = dateAndTime;
            Relapse.Location = location;
            Relapse.RelapseId = relapseId;
            Relapse.Notes = notes;
            Relapse.SymptomCollectionId = symptomCollectionId;
            Relapse.TriggerCollectionId = triggerCollectionId;
            Relapse.UserId = userId;
        }
    }
}
