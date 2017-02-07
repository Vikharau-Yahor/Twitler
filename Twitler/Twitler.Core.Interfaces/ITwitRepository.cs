using System;
using System.Collections.Generic;
using Twitler.Domain.Model;

namespace Twitler.Domain.Interfaces
{
    public interface ITwitRepository
    {
        List<Twit> GetAll();
    }
}
