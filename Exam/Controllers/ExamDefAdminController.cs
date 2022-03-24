using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Exam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExamDefAdminController : Controller
    {
        ExamDefAdminRepository _examDefAdminRepository;
        public ExamDefAdminController(ExamDefAdminRepository examDefAdminRepository)
        {
            _examDefAdminRepository = examDefAdminRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ListExams()
        {
            List<ExamDefAdmin> exams = _examDefAdminRepository.List();
            List<ExamDefAdminViewModel> vmExams = new List<ExamDefAdminViewModel>();

            foreach (var exam in exams)
            {
                ExamDefAdminViewModel examDefAdminViewModel = new ExamDefAdminViewModel
                {
                    Id = exam.Id,
                    Name = exam.Name,
                    CreatedDate = exam.CreatedDate
                };

                vmExams.Add(examDefAdminViewModel);
            }

            return View(vmExams);
        }


        [HttpPost]
        public IActionResult AddOrUpdateExam(ExamDefAdminViewModel examDefAdminViewModel)
        {
            ExamDefAdmin examDefAdmin = new ExamDefAdmin()
            {
                Id = examDefAdminViewModel.Id,
                Name = examDefAdminViewModel.Name,
                Text = examDefAdminViewModel.Text,
                Questions = new List<Question>(),
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")        
            };

            for (int i = 0; i < ExamDefinition.QuestionCount; i++)
            {
                Question question = new Question()
                {
                    Id = examDefAdminViewModel.Questions[i].Id,
                    Text = examDefAdminViewModel.Questions[i].Text,
                    ExamId = examDefAdmin.Id,
                    QuestionOrder = i + 1,
                    CorrectChoiceId = examDefAdminViewModel.Questions[i].CorrectChoiceId,
                    ExamDef = examDefAdmin,
                    Choices = new List<Choice>()
                };

                for (int j = 0; j < ExamDefinition.ChoiceCount; j++)
                {
                    Choice choice = new Choice()
                    {
                        Id = examDefAdminViewModel.Questions[i].Choices[j].Id,
                        Order = j + 1,
                        Text = examDefAdminViewModel.Questions[i].Choices[j].Text,
                        QuestionId = question.Id,
                        Question = question
                    };
                    question.Choices.Add(choice);
                }

                examDefAdmin.Questions.Add(question);
            }

            int savedId = _examDefAdminRepository.AddOrUpdate(examDefAdmin);
            return RedirectToAction("AddOrUpdateExam", new { id = savedId });
        }

        public IActionResult AddOrUpdateExam(int id)
        {
            ExamDefAdminViewModel examDefAdminViewModel = new ExamDefAdminViewModel();
            
            if (id > 0)
            {
                ExamDefAdmin examDefAdmin = _examDefAdminRepository.GetById(id);
                examDefAdminViewModel.Id = examDefAdmin.Id;
                examDefAdminViewModel.Name = examDefAdmin.Name;
                examDefAdminViewModel.Text = examDefAdmin.Text;
                examDefAdminViewModel.Questions = new List<QuestionDefAdminViewModel>();
                foreach (var question in examDefAdmin.Questions)
                {
                    QuestionDefAdminViewModel questionViewModel = new QuestionDefAdminViewModel()
                    {
                        Id = question.Id,
                        Text = question.Text,
                        ExamId = question.ExamId,                        
                        Choices = new List<ChoiceViewModel>(),
                        CorrectChoiceId = question.CorrectChoiceId
                    };

                    foreach(var choice in question.Choices)
                    {
                        ChoiceViewModel choiceViewModel = new ChoiceViewModel()
                        {
                            Id = choice.Id,
                            Text = choice.Text,
                            QuestionId = choice.QuestionId,
                            Question = questionViewModel
                        };

                        questionViewModel.Choices.Add(choiceViewModel);
                    }

                    examDefAdminViewModel.Questions.Add(questionViewModel);
                }
            }
            ViewBag.Id = examDefAdminViewModel.Id;
            return View(examDefAdminViewModel);
        }

        public IActionResult Delete(int id)
        {
            _examDefAdminRepository.Delete(id);
            return RedirectToAction("ListExams");
        }

        public IActionResult GetLinkContent(string link)
        {
            return Content(ExamDefAdminViewModel.GetContentByHeader(link), "text/html");
        }
    }
}
