using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Models
{
    public class SectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BoardId { get; set; }
        public BoardDTO Board { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}
