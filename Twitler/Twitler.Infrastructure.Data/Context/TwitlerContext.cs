using System.Data.Entity;
using Twitler.Domain.Model;

namespace Twitler.Data.Context
{
    public class TwitlerContext : DbContext, ITwitlerContext
    {
        public TwitlerContext() : base("TwitlerDB")
        { }

        public TwitlerContext(string dbConnectionString) : base(dbConnectionString)
        { }

        public DbSet<User> Users { get; set; }
    }
}
