using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class ExamDefFullViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} gerekli"), Display(Name = "İsim"), StringLength(30, MinimumLength = 3, ErrorMessage = "{0} en az {2} en fazla {1} karakter olabilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} gerekli")]
        public string Text { get; set; }

        public int QuestionId { get; set; }
        [Required(ErrorMessage = "{0} gerekli")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "{0} gerekli")]
        public string Choice_Id { get; set; }
        [Required(ErrorMessage = "{0} gerekli")]
        public string Choice_Num { get; set; }

        [Required(ErrorMessage = "{0} gerekli")]
        public string Choice_Text { get; set; }
        [Required(ErrorMessage = "{0} gerekli")]
        public string CorrectAnswer { get; set; }

    }
}
