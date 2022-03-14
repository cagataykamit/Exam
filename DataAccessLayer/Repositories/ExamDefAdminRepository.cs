using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ExamDefAdminRepository
    {
        ExamDbContext _examDbContext;
        public ExamDefAdminRepository(ExamDbContext examDbContext)
        {
            _examDbContext = examDbContext;
        }

        public List<ExamDefAdmin> GetExams()
        {
            var exams = _examDbContext.AdminExamDefs.ToList();
            return exams;
        }

        public ExamDefAdmin GetExamById(int id)
        {
            var exam = _examDbContext.AdminExamDefs.Include(c => c.Questions).ThenInclude(c => c.Choices).FirstOrDefault(c => c.Id == id);
            exam.Questions.OrderBy(c => c.QuestionOrder);
            foreach (var question in exam.Questions)
                question.Choices.OrderBy(c => c.Order);

            return exam;
        }

        public int InsertOrUpdate(ExamDefAdmin exam)
        {
            if (exam.Id <= 0)
            {
                _examDbContext.AdminExamDefs.Add(exam);
            }
            else
            {
                _examDbContext.AdminExamDefs.Update(exam);
            }
            _examDbContext.SaveChanges();
            return exam.Id;
        }

        public void Delete(int id)
        {
            var removed = GetExamById(id);
            _examDbContext.AdminExamDefs.Remove(removed);
            _examDbContext.SaveChanges();
        }
    }
}
