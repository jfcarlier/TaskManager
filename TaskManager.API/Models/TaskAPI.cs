using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Models
{
    public class TaskAPI
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public int SectionId { get; set; }
        
        [JsonIgnore]
        public SectionAPI Section { get; set; }
    }
}
