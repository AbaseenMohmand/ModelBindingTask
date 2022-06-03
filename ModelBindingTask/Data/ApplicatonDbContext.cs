using Microsoft.EntityFrameworkCore;
using ModelBindingTask.Models;

namespace ModelBindingTask.Data
{
    public class ApplicatonDbContext : DbContext
    {
        public ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options ) : base( options )
        {
                
        }

        public DbSet<Student> Students { get; set; }
    }
}
