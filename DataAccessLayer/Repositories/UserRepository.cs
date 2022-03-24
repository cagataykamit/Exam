using DataAccessLayer.Entities;
using ExamDataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ExamDbContext ctx) : base(ctx)
        {
        }

        public User GetExamByIdAndPassword(int id, string password)
        {
            User result = _ctx.Users.FirstOrDefault(c => c.Id == id && c.Password == password);
            return result;
        }

        public User GetUser(User user)
        {
            var result = _ctx.Users.FirstOrDefault(x => x.Password == user.Password && x.UserName == user.UserName);
            return result;
        }
    }
}
