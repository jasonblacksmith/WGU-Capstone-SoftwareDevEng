using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Proceedure")]
    public class Proceedure : IProceedure
    {
        [PrimaryKey, AutoIncrement]
        [Column("ProceedureId")]
        public int ProceedureId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
    }
}
