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
    public class ExamController : Controller
    {
        ExamDefAdminRepository _examDefAdminRepository;
        public ExamController(ExamDefAdminRepository examDefAdminRepository)
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
                    Name = exam.Name
                };

                vmExams.Add(examDefAdminViewModel);
            }

            return View(vmExams);
        }

        public IActionResult TakeExam(int id)
        {
            return RedirectToAction("TakeExam", "TakeExam", new { examId = id });
        }
    }
}
