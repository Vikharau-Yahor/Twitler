using System;
using System.ComponentModel.DataAnnotations;

namespace Twitler.ViewModels.Twit
{
    public class PostedTwitJm
    {
        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        [Required]
        public DateTime DatePost { get; set; }
    }
}
