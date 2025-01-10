using Microsoft.EntityFrameworkCore;
using RahulCrud.Models.Entities;

namespace RahulCrud.Data
{
    public class ApplicationDdContext:DbContext
    {
        public ApplicationDdContext(DbContextOptions<ApplicationDdContext> options):base(options) 
        {
            
        }
        public DbSet<Student> students { get; set; }

    }
}
