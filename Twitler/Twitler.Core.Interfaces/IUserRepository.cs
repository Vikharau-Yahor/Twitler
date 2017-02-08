using System;
using Twitler.Domain.Model;

namespace Twitler.Domain.Interfaces
{
    public interface IUserRepository
    {
        User Find(string email, Guid password);
        User Get(string email);
    }
}
