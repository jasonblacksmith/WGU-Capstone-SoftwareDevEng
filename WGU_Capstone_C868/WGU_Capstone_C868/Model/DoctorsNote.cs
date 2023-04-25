using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("DoctorsNote")]
    public class DoctorsNote : IDoctorsNote
    {
        [PrimaryKey, AutoIncrement]
        [Column("DoctorsNoteId")]
        public int DoctorsNoteId { get; set; }
        [Indexed]
        [Column("VisitId")]
        public int VisitId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Content")]
        public string Content { get; set; }
        [Column("DoctorsName")]
        public string DoctorsName { get; set; }
        [Column("WithResults")]
        public bool WithResults { get; set; }
        [Column("ResultsId")]
        public int? ResultsId { get; set; }
    }
}
