﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitler.Domain.Model
{
    public class Twit
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DatePost { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
