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

        public DbSet<Board> Boards { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
