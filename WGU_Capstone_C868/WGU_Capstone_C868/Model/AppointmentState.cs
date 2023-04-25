using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("AppointmentState")]
    public class AppointmentState : IAppointmentState
    {
        [PrimaryKey, AutoIncrement]
        [Column("StatId")]
        public int StateId { get; set; }
        [Column("Active")]
        public bool Active { get; set; }
        [Column("Cancelled")]
        public bool Cancelled { get; set; }
        [Column("Rescheduled")]
        public bool Rescheduled { get; set; }
        [Column("Complete")]
        public bool Complete { get; set;}
    }
}
