using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WGU_Capstone_C868.Model
{
    [Table("FileCollection")]
    public class FileCollection
    {
        [PrimaryKey, AutoIncrement]
        [Column("FileCollectionId")]
        public int FileId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
    }
}
