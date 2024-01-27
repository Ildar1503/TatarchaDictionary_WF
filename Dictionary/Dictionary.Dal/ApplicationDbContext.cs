using DictionaryTatarcha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Dal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder)
            : base(optionsBuilder) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=DESKTOP-S059N9O;Database=Dictionary;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Noun> Nouns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Adjective> Adjectivies { get; set; }
        public DbSet<Verb> Verbs { get; set; }
    }
}
