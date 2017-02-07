using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Twitler.Domain.Model;

namespace Twitler.Data.Context
{
    public class TwitlerContext : DbContext, ITwitlerContext
    {
        public TwitlerContext() : base("TwitlerDB")
        {
        }

        public TwitlerContext(string dbConnectionString) : base(dbConnectionString)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Twit> Twits { get; set; }
        public DbSet<HashTag> HashTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HashTag>().HasKey(p => new {p.HashValue})
                .Property(p => p.HashValue)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Twit>()
                .HasMany(t => t.HashTags)
                .WithMany(ht => ht.Twits);

            base.OnModelCreating(modelBuilder);
        }
    }
}
