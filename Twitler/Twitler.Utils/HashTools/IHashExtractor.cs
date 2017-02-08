using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitler.Utils.HashTools
{
    public interface IHashExtractor
    {
        int[] GetFromString(string text);
    }
}
