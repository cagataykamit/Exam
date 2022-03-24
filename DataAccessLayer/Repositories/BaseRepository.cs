using DataAccessLayer;
using ExamDataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDataAccessLayer.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected ExamDbContext _ctx;
        public BaseRepository(ExamDbContext ctx)
        {
            _ctx = ctx;
        }

        public virtual T GetById(int id)
        {
            return _ctx.Set<T>().FirstOrDefault(c => c.Id == id);
        }

        public virtual List<T> List()
        {
            return _ctx.Set<T>().ToList();
        }

        public int AddOrUpdate(T entity)
        {
            if (entity.Id <= 0)
            {
                _ctx.Set<T>().Add(entity);
            }
            else
            {
                _ctx.Set<T>().Update(entity);
            }
            _ctx.SaveChanges();
            return entity.Id;
        }

        public void Delete(int id)
        {
            var removed = GetById(id);
            _ctx.Set<T>().Remove(removed);
            _ctx.SaveChanges();
        }
    }
}
