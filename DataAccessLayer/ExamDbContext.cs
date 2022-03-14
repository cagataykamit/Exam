
using DataAccessLayer.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ExamDefAdmin> AdminExamDefs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                                                new User { Id = 1, UserName = "admin1", Password = "1234",Role= "Admin" },
                                                new User { Id = 2, UserName = "admin2", Password = "1234",Role= "Admin" },
                                                new User { Id = 3, UserName = "user1", Password = "1234", Role = "User" },
                                                new User { Id = 4, UserName = "user2", Password = "1234", Role = "User" }
                                               );
        }
    }
}