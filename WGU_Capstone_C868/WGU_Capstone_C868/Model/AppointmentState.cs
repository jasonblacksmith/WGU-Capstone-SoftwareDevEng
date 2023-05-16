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
        [Column("Name")]
        public string Name { get; set; }
    }
}
