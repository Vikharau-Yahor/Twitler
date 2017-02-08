using System;

namespace Twitler.ViewModels.Twit
{
    public class TwitVm
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DatePost { get; set; }
        public string UserName { get; set; }
        public bool CanBeModified { get; set; }
    }
}