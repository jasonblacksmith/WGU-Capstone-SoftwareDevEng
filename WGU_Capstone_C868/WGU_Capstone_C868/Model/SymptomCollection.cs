using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGU_Capstone_C868.Model
{
    [Table("SymptomCollection")]
    public class SymptomCollection
    {
        [PrimaryKey, AutoIncrement]
        [Column("SymptomCollectionId")]
        public int SymptomCollectionId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
    }
}
