using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Models
{
    public class BoardDTO
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public IEnumerable<SectionDTO> Sections { get; set; }
    }
}
