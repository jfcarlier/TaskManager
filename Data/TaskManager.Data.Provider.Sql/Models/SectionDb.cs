﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskManager.Data.Provider.Sql.Models
{
    [Table("Sections")]
    public class SectionDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey(nameof(Board))]
        public int BoardId { get; set; }

        public BoardDb Board { get; set; }

        public ICollection<TaskDb> Tasks { get; set; }
    }
}
