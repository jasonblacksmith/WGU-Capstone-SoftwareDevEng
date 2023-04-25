using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Relapse")]
    public class Relapse : IRelapse
    {
        [PrimaryKey, AutoIncrement]
        [Column("ResultsId")]
        public int ResultsId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("Location")]
        public string Location { get; set; }
        [Column("DateAndTime")]
        public DateTime DateAndTime { get; set; }
        [Indexed]
        [Column("TriggersCollectionId")]
        public int TriggersCollectionId { get; set; }
        [Indexed]
        [Column("SymptomCollectionId")]
        public int SymptomCollectionId { get; set; }
    }
}
