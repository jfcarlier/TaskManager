using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Models
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public SectionDTO Section { get; set; }
    }
}
