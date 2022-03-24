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
    public class QuestionRepository : BaseRepository<Question>
    {
        public QuestionRepository(ExamDbContext ctx) : base(ctx)
        {

        }
        public override List<Question> List()
        {
            return _ctx.Questions.Include(c => c.ExamDef).ToList();
        }
    }
}
