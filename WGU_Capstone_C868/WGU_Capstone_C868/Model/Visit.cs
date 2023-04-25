using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Visit")]
    public class Visit : IVisit
    {
        [PrimaryKey, AutoIncrement]
        [Column("VisitId")]
        public int VisitId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
        [Indexed]
        [Column("VisitTypeId")]
        public int VisitTypeId { get; set; }
        [Column("DateAndTime")]
        public DateTime DateAndTime { get; set; }
    }
}
