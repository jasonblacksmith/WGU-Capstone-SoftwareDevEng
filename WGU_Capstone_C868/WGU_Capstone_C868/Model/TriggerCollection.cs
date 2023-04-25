using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGU_Capstone_C868.Model
{
    [Table("TriggerCollection")]
    public class TriggerCollection
    {
        [PrimaryKey, AutoIncrement]
        [Column("TriggerCollectionId")]
        public int TriggerCollectionId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
    }
}
