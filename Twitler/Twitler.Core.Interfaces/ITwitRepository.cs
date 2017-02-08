using System;
using System.Collections.Generic;
using Twitler.Domain.Model;

namespace Twitler.Domain.Interfaces
{
    public interface ITwitRepository
    {
        List<Twit> GetAll();
        List<Twit> GetByHashTags(int[] hashValues);
        Twit GetIfOwned(string userEmailOwner, int twitId);
        void Add(Twit twit);
        void Delete(Twit twit);
    }
}
