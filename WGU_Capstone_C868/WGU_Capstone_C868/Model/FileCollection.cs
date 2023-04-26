using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("FileCollection")]
    public class FileCollection : IFileCollection
    {
        [PrimaryKey, AutoIncrement]
        [Column("FileCollectionId")]
        public int FileCollectionId { get; set; }
        [Indexed]
        [Column("ResultsId")]
        public int ResultsId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
    }
}
