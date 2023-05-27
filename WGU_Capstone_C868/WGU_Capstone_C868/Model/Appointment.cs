using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Appointment")]
    public class Appointment : IAppointment
    {
        [PrimaryKey, AutoIncrement]
        [Column("AppointmentId")]
        public int AppointmentId { get; set; }
        [Indexed]
        [Column("ProceedureId")]
        public int ProceedureId { get; set; }
        [Column("LocationName")]
        public string LocationName { get; set; }
        [Indexed]
        [Column("AddressId")]
        public int AddressId { get; set; }
        [Column("Notes")]
        public string Notes { get; set; }
        [Column("Phone")]
        public string PhoneNumber { get; set; }
        [Column("Current")]
        public bool Current { get; set; }
        [Column("DateAndTime")]
        public DateTime DateAndTime { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
    }
}
