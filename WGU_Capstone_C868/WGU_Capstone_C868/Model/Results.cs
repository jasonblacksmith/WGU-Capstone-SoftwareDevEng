using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Results")]
    public class Results : IResults
    {
        [PrimaryKey, AutoIncrement]
        [Column("ResultsId")]
        public int ResultsId { get; set; }
        [Indexed]
        [Column("AppointmentId")]
        public int AppointmentId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
        [Indexed]
        [Column("ProceedureId")]
        public int ProceedureId { get; set; }
        [Indexed]
        [Column("FileCollectionId")]
        public int FileCollectionId { get; set; }
        [Indexed]
        [Column("DoctorsNoteId")]
        public int DoctorsNoteId { get; set; }
        [Column("OtherNotes")]
        public string OtherNotes { get; set; }
    }
}
