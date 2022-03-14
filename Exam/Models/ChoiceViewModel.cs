using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class ChoiceViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public QuestionDefAdminViewModel Question { get; set; }
    }
}
