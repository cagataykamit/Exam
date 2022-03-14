using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class QuestionRepository
    {
        ExamDbContext _examDbContext;

        public QuestionRepository(ExamDbContext examDbContext)
        {
            _examDbContext = examDbContext;
        }

        public Question GetQuestionById(int id)
        {
            return _examDbContext.Questions.FirstOrDefault(c => c.Id == id);
        }

        public List<Question> ListQuestions()
        {
            return _examDbContext.Questions.Include(c=>c.ExamDef).ToList();
        }

        public int AddOrUpdateQuestion(Question question)
        {
            if (question.Id<=0)
            {
                _examDbContext.Questions.Add(question);
            }
            else
            {
                _examDbContext.Questions.Update(question);
            }
            _examDbContext.SaveChanges();
            return question.Id;
        }

        public void Delete(int id)
        {
            Question question = GetQuestionById(id);
            _examDbContext.Questions.Remove(question);
            _examDbContext.SaveChanges();
        }
    }
}
