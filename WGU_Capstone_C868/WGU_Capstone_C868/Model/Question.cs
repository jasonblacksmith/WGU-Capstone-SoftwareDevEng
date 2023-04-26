using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Questions")]
    public class Question : IQuestion
    {
        [PrimaryKey, AutoIncrement]
        [Column("QuestionId")]
        public int QuestionId { get; set; }
        [Indexed]
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("Question")]
        public string QuestionContent { get; set; }
        [Indexed]
        [Column("VisitId")]
        public int VisitId { get; set; }
    }
}
