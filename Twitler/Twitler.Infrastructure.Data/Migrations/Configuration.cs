using Twitler.Data.Context;
using Twitler.Domain.Model;
using Twitler.Utils.Encryptors;

namespace Twitler.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TwitlerContext>
    {
        private IEncryptor _encryptor;
        public Configuration()
        {
            _encryptor = new MD5Encryptor();
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TwitlerContext context)
        {
            FillDbInitialData(context);
            base.Seed(context);
        }

        private void FillDbInitialData(TwitlerContext context)
        {
            AddUsers(context);
            context.SaveChanges();
        }

        private void AddUsers(TwitlerContext context)
        {
            context.Users.Add(new User { Email = "aleksandrov@gmail.com", Password = _encryptor.Encrypt("12345") });
            context.Users.Add(new User { Email = "Semenov@gmail.com", Password = _encryptor.Encrypt("333444") });
        }
    }
}
