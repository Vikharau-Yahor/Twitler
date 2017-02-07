using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Twitler.Data.Context;
using Twitler.Domain.Model;
using Twitler.Utils.Encryptors;

namespace Twitler.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TwitlerContext>
    {
        private readonly IEncryptor _encryptor;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            _encryptor = new MD5Encryptor();
        }

        protected override void Seed(TwitlerContext context)
        {
            FillDbInitialData(context);
        }

        private void FillDbInitialData(TwitlerContext context)
        {
            AddUsers(context);
            AddTwits(context);
            context.SaveChanges();
        }

        private void AddUsers(TwitlerContext context)
        {
            context.Users.Add(new User { Email = "aleksandrov@gmail.com", Password = _encryptor.Encrypt("12345") });
            context.Users.Add(new User { Email = "Semenov@gmail.com", Password = _encryptor.Encrypt("333444") });
            context.Users.Add(new User { Email = "lepra@yandex.ru", Password = _encryptor.Encrypt("23456") });
            context.Users.Add(new User { Email = "celina@google.by", Password = _encryptor.Encrypt("123456") });
        }

        private void AddTwits(TwitlerContext context)
        {
            Random minuteRandom = new Random();
            Random messageRandom = new Random();

            List<string> messageList = new List<string>
            {
                "Hello", "Как выглядит чашка кофе в разных странах мира",
                "Слишком резко похолодало.","Where can I get this bath bomb?",
                "спиритические сеансы по вызову лифтера", "Makeup inspired by your pet"
            };

            foreach (var user in context.Users)
            {
                user.Twits.Add(new Twit
                {
                    User = user,
                    DatePost = DateTime.Now.AddMinutes(minuteRandom.Next(1, 500)),
                    Message = user.Email + " say " + messageList[messageRandom.Next(0, messageList.Count)],
                });
            }
        }
    }
}
