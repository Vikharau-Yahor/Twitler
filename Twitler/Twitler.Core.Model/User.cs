using System;
using System.Collections.Generic;

namespace Twitler.Domain.Model
{
    public class User
    {
        public User()
        {
            Twits = new List<Twit>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public Guid Password { get; set; }

        public virtual ICollection<Twit> Twits { get; set; }
    }
}
