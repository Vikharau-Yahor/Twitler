using System;
using System.Data.Entity;
using Twitler.Domain.Model;

namespace Twitler.Data.Context
{
    public interface ITwitlerContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Twit> Twits { get; set; }
    }
}
