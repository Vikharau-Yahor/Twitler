using System;

namespace Twitler.Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Guid Password { get; set; }
    }
}
