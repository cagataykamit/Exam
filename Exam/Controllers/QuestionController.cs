using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Controllers
{
    public class QuestionController : Controller
    {
        QuestionRepository _questionRepository;
        public QuestionController(QuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListQuestions()
        {
            List<Question> questions = _questionRepository.ListQuestions();
            List<QuestionDefAdminViewModel> vmQuestions = new List<QuestionDefAdminViewModel>();

            foreach (var question in questions)
            {
                QuestionDefAdminViewModel questionViewModel = new QuestionDefAdminViewModel
                {
                    Id = question.Id,
                    ExamId = question.ExamId,
                    Text = question.Text
                };

                vmQuestions.Add(questionViewModel);
            }

            return View(vmQuestions);
        }

        [HttpPost]
        public IActionResult AddOrUpdateQuestion(QuestionDefAdminViewModel questionViewModel)
        {
            Question question = new Question();
            question.Id = questionViewModel.Id;
            question.ExamId = questionViewModel.ExamId;
            question.Text = questionViewModel.Text;
            
            _questionRepository.AddOrUpdateQuestion(question);
            return RedirectToAction("ListQuestions");
        }

        public IActionResult AddOrUpdateQuestion(int id, int examId)
        {
            QuestionDefAdminViewModel questionViewModel = new QuestionDefAdminViewModel();
            if (id > 0)
            {
                Question question = _questionRepository.GetQuestionById(id);
                questionViewModel.Id = question.Id;
                questionViewModel.ExamId = examId > 0 ? examId : question.ExamId;
                questionViewModel.Text = question.Text;
            }
            return View(questionViewModel);
        }

        public IActionResult Delete(int id)
        {
            _questionRepository.Delete(id);
            return RedirectToAction("ListQuestions");
        }
        public IActionResult AddChoice(int id)
        {
            return RedirectToAction("AddOrUpdateChoice", "Choice", new { questionId = id });
        }
    }
}
