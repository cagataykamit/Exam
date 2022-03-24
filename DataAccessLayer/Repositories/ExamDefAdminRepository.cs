using DataAccessLayer.Entities;
using ExamDataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ExamDefAdminRepository : BaseRepository<ExamDefAdmin>
    {
        public ExamDefAdminRepository(ExamDbContext ctx) : base(ctx)
        {

        }

        public override ExamDefAdmin GetById(int id)
        {
            var exam = _ctx.AdminExamDefs.Include(c => c.Questions).ThenInclude(c => c.Choices).FirstOrDefault(c => c.Id == id);
            exam.Questions.OrderBy(c => c.QuestionOrder);
            foreach (var question in exam.Questions)
                question.Choices.OrderBy(c => c.Order);

            return exam;
        }
    }
}
