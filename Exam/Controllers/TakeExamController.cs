using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Controllers
{
    public class TakeExamController : Controller
    {
        ExamDefAdminRepository _examDefAdminRepository;
        public TakeExamController(ExamDefAdminRepository examDefAdminRepository)
        {
            _examDefAdminRepository = examDefAdminRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListExams()
        {
            return RedirectToAction("ListExams", "Exam");
        }

        [HttpGet]
        public IActionResult TakeExam(int id, int examId)
        {
            ExamViewModel examViewModel = new ExamViewModel();
            if (examId > 0)
            {
                ExamDefAdmin examDefAdmin = _examDefAdminRepository.GetExamById(examId);
                examViewModel.Id = examDefAdmin.Id;
                examViewModel.Name = examDefAdmin.Name;
                examViewModel.Text = examDefAdmin.Text;
                examViewModel.Questions = new List<QuestionDefAdminViewModel>();
                examViewModel.Answers = new List<AnswerViewModel>();
                foreach (var question in examDefAdmin.Questions)
                {
                    QuestionDefAdminViewModel questionViewModel = new QuestionDefAdminViewModel()
                    {
                        Id = question.Id,
                        Text = question.Text,
                        ExamId = question.ExamId,
                        Choices = new List<ChoiceViewModel>(),
                        CorrectChoiceId = -1
                    };

                    foreach (var choice in question.Choices)
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

                    examViewModel.Questions.Add(questionViewModel);
                    AnswerViewModel answerViewModel = new AnswerViewModel()
                    {
                        QuestionId = question.Id,
                        SelectedChoice = -1
                    };
                    examViewModel.Answers.Add(answerViewModel);
                }
            }
            ViewBag.Id = examViewModel.Id;

            return View(examViewModel);
        }

        [HttpPost]
        public IActionResult TakeExam(ExamViewModel examViewModel)
        {
            ExamDefAdmin examDefAdmin = _examDefAdminRepository.GetExamById(examViewModel.Id);
            examViewModel.Questions = new List<QuestionDefAdminViewModel>();
            foreach (var question in examDefAdmin.Questions)
            {
                QuestionDefAdminViewModel questionViewModel = new QuestionDefAdminViewModel();
                questionViewModel.CorrectChoiceId = question.CorrectChoiceId;
                examViewModel.Questions.Add(questionViewModel);
            }

            return View(examViewModel);
        }
        public JsonResult ExamResult(int examId)
        {
            ExamDefAdmin examDefAdmin = _examDefAdminRepository.GetExamById(examId);
            List<int> correctAnswers = new List<int>();
            foreach (var question in examDefAdmin.Questions)
                correctAnswers.Add(question.CorrectChoiceId);

            return new JsonResult(new { success= true, data=correctAnswers});
        }
    }
}
