using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ChoiceRepository
    {
        ExamDbContext _examDbContext;
        public ChoiceRepository(ExamDbContext examDbContext)
        {
            _examDbContext = examDbContext;
        }

        public List<Choice> ListChoices()
        {
           return _examDbContext.Choices.Include(c => c.Question).ToList();
        }
        
        public void AddOrUpdateChoice(Choice choice)
        {
            if (choice.Id<=0)
            {
                _examDbContext.Choices.Add(choice);
            }
            else
            {
                _examDbContext.Choices.Update(choice);
            }
            _examDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Choice removed = GetChoiceById(id);
            _examDbContext.Choices.Remove(removed);
            _examDbContext.SaveChanges();
        }

        public Choice GetChoiceById(int id)
        {
            return _examDbContext.Choices.FirstOrDefault(c => c.Id == id);
        }
    }
}
