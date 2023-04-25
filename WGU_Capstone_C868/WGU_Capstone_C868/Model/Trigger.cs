using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Trigger")]
    public class Trigger : ITrigger
    {
        [PrimaryKey, AutoIncrement]
        [Column("TriggerId")]
        public int TriggerId { get; set; }
        [Indexed]
        [Column("TriggerCollectionId")]
        public int TriggerCollectionId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("IsNew")]
        public bool IsNew { get; set; }
    }
}
