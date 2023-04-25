using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("VisitType")]
    public class VisitType : IVisitType
    {
        [PrimaryKey, AutoIncrement]
        [Column("VisitTypeId")]
        public int VisitTypeId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
    }
}
