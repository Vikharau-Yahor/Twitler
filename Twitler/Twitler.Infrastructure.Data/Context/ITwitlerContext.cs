using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Twitler.Domain.Model;

namespace Twitler.Data.Context
{
    public interface ITwitlerContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Twit> Twits { get; set; }
        DbSet<HashTag> HashTags { get; set; }

        DbEntityEntry Entry(object entry);
        int SaveChanges();
    }
}
