using ExamDataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public int ExamId { get; set; }
        public int QuestionOrder { get; set; }
        public int CorrectChoiceId { get; set; }
        [ForeignKey("ExamId")]
        public ExamDefAdmin ExamDef { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
