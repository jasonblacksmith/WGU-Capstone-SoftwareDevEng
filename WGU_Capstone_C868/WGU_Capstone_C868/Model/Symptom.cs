using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Symptom")]
    public class Symptom : ISymptom
    {
        [PrimaryKey, AutoIncrement]
        [Column("SymptomId")]
        public int SymptomId { get; set; }
        [Indexed]
        [Column("SymptomCollectionId")]
        public int SymptomCollectionId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("IsNew")]
        public bool IsNew { get; set; }
    }
}
