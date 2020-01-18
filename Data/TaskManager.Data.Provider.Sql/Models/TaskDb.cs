using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskManager.Data.Provider.Sql.Models
{
    [Table("Tasks")]
    public class TaskDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey(nameof(Section))]
        public int SectionId { get; set; }

        public SectionDb Section { get; set; }
    }
}
