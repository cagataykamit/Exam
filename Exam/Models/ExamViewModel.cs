using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class ExamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public List<QuestionDefAdminViewModel> Questions { get; set; }

        public List<AnswerViewModel> Answers { get; set; }

    }
}
