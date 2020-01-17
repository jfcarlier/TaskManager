using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Models
{
    public class SectionAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TaskAPI> Tasks { get; set; }
    }
}
