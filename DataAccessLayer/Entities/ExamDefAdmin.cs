using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ExamDefAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<Question> Questions { get; set; }
        public string CreatedDate { get; set; }
    }
}
