using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository
    {
        ExamDbContext _examDbContext;

        public UserRepository(ExamDbContext examDbContext)
        {
            _examDbContext = examDbContext;
        }

        public User GetExamByIdAndPassword(int id, string password)
        {
            User result = _examDbContext.Users.FirstOrDefault(c => c.Id == id && c.Password == password);
            return result;
        }

        public User GetUser(User user)
        {
            var result = _examDbContext.Users.FirstOrDefault(x => x.Password == user.Password && x.UserName == user.UserName);
            return result;
        }
    }
}
