using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Provider.Sql.Models;

namespace TaskManager.Data.Provider.Sql
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) 
            : base(options)
        {
        }

        public DbSet<BoardDb> Boards { get; set; }
        public DbSet<SectionDb> Sections { get; set; }
        public DbSet<TaskDb> Tasks { get; set; }
    }
}
