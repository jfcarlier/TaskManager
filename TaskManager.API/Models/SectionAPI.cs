
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManager.API.Models
{
    public class SectionAPI
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int BoardId { get; set; }

        [JsonIgnore]
        public BoardAPI Board { get; set; }

        [JsonIgnore]
        public IEnumerable<TaskAPI> Tasks { get; set; }
    }
}
