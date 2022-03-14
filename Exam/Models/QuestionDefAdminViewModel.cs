using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class QuestionDefAdminViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} gerekli"), Display(Name = "Soru"), StringLength(1000, MinimumLength = 8, ErrorMessage = "{0} en az {2} en fazla {1} karakter olabilir.")]
        public string Text { get; set; }
        [Display(Name = "Sınav Id")]
        public int ExamId { get; set; }

        public List<ChoiceViewModel> Choices { get; set; }

        public int CorrectChoiceId { get; set; }
    }
}
