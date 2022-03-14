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
    public class ChoiceController : Controller
    {
        ChoiceRepository _choiceRepository;
        public ChoiceController(ChoiceRepository choiceRepository)
        {
            _choiceRepository = choiceRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListChoices()
        {
            List<ChoiceViewModel> vmChoices = new List<ChoiceViewModel>();
            var choices = _choiceRepository.ListChoices();
            foreach (var choice in choices)
            {
                ChoiceViewModel choiceViewModel = new ChoiceViewModel()
                {
                    Id = choice.Id,
                    Text = choice.Text,
                    QuestionId = choice.QuestionId
                };
                vmChoices.Add(choiceViewModel);
            }
            return View(vmChoices);
        }
        [HttpGet]
        public IActionResult AddOrUpdateChoice(int id, int questionId)
        {
            ChoiceViewModel choiceViewModel = new ChoiceViewModel();
            if (id > 0)
            {
                Choice choice = _choiceRepository.GetChoiceById(id);
                choiceViewModel.Id = choice.Id;
                choiceViewModel.Text = choice.Text;
                choiceViewModel.QuestionId = questionId > 0 ? questionId : choice.QuestionId;
            }
            return View(choiceViewModel);
        }
        [HttpPost]
        public IActionResult AddOrUpdateChoice(ChoiceViewModel choiceViewModel)
        {
            Choice choice = new Choice();

            choice.Id = choiceViewModel.Id;
            choice.QuestionId = choiceViewModel.QuestionId;
            choice.Text = choiceViewModel.Text;

            _choiceRepository.AddOrUpdateChoice(choice);
            return RedirectToAction("ListChoices");
        }

        public IActionResult Delete(int id)
        {
            _choiceRepository.Delete(id);
            return RedirectToAction("ListChoices");
        }
    }
}
