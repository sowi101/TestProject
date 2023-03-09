using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using TestProject.Models;


namespace TestProject.Data
{
    public class DataContext : DbContext
    {
            public DataContext(DbContextOptions<DataContext> options)
                : base(options)
            {
            }

        public DbSet<Competence> Competences { get; set; }
        public DbSet<Project> Projects { get; set; }
            
        }
}
