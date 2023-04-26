using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("SymptomCollection")]
    public class SymptomCollection : ISymptomCollection
    {
        [PrimaryKey, AutoIncrement]
        [Column("SymptomCollectionId")]
        public int SymptomCollectionId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
    }
}
