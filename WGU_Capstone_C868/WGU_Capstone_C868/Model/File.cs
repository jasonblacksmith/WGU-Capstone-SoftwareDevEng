using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("File")]
    public class File : IFile
    {
        [PrimaryKey, AutoIncrement]
        [Column("FileId")]
        public int FileId { get; set; }
        [Column("FileType")]
        public string FileType { get; set; }
        [Column("FileTitle")]
        public string FileTitle { get; set; }
        [Column("FilePath")]
        public string FilePath { get; set; }
        [Indexed]
        [Column("FileCollectionId")]
        public int FileCollectionId { get; set; }
    }
}
