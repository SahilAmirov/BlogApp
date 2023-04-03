using BlogApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Context
{
    public class BlogAppDbContext : DbContext
    {

        public DbSet<Writer> Writers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server=SAHIL; Database=BlogAppDb; Integrated Security=true;");
            optionsBuilder.UseSqlServer("Server=104.247.162.242\\MSSQLSERVER2017;Database=akadem58_sae;User Id=akadem58_sae;Password=Ls2rb8~86;");
        }

    }
}
