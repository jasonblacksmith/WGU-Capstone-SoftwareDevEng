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
        [Column("RelapseId")]
        public int RelapseId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("Location")]
        public string Location { get; set; }
        [Column("DateAndTime")]
        public DateTime DateAndTime { get; set; }
        [Indexed]
        [Column("EntryNotes")]
        public string Notes { get; set; }
        [Indexed]
        [Column("TriggersCollectionId")]
        public int TriggerCollectionId { get; set; }
        [Indexed]
        [Column("SymptomCollectionId")]
        public int SymptomCollectionId { get; set; }
    }
}
