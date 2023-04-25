using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("User")]
    public class User : IUser
    {
        [PrimaryKey, AutoIncrement]
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Name")]
        public string Name { get; set; }
    }
}
