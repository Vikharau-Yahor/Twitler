using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Twitler.Utils.HashTools
{
    public class HashExtractor : IHashExtractor
    {
        private const string HashPattern = @"#[а-яА-Яa-zA-Z]+";
        private IHashConverter _hashConverter;

        public HashExtractor(IHashConverter hashConverter)
        {
            _hashConverter = hashConverter;
        }

        public int[] GetFromString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new int[0];

            var hashTagsMatches = Regex.Matches(text, HashPattern);

            return (from object m in hashTagsMatches
                select _hashConverter.GetHashValue(m.ToString())).Distinct().ToArray();
        }
    }
}
