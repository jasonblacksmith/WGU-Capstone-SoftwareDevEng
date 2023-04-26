using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("TriggerCollection")]
    public class TriggerCollection : ITriggerCollection
    {
        [PrimaryKey, AutoIncrement]
        [Column("TriggerCollectionId")]
        public int TriggerCollectionId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
    }
}
