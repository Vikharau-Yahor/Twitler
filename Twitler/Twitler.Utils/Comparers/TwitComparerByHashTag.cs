using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitler.Domain.Model;

namespace Twitler.Utils.Comparers
{
    public class TwitComparerByHashTag : IComparer<Twit>
    {
        private readonly int[] _hashValues;

        public TwitComparerByHashTag(int[] hashValues)
        {
            _hashValues = hashValues;
        }

        public int Compare(Twit x, Twit y)
        {
            if (GetHashTagMathes(x) < GetHashTagMathes(y))
                return 1;
            if (GetHashTagMathes(x) > GetHashTagMathes(y))
                return -1;
            return 0;
        }

        public int GetHashTagMathes(Twit twit)
        {
            return twit.HashTags.Count(ht => _hashValues.Contains(ht.HashValue));
        }
    }
}
