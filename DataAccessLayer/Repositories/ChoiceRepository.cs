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
    public class ChoiceRepository : BaseRepository<Choice>
    {
        public ChoiceRepository(ExamDbContext ctx) : base(ctx)
        {

        }
        public override List<Choice> List()
        {
            return _ctx.Choices.Include(c => c.Question).ToList();
        }
    }
}
