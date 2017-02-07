using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitler.Domain.Model
{
    public class HashTag
    {
        public HashTag()
        {
            Twits = new List<Twit>();
        }

        public int HashValue { get; set; }

        public virtual ICollection<Twit> Twits { get; set; } 

    }
}
