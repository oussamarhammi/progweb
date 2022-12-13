using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalAPI.Models
{
    public class MessageDTO
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public int MessageCount { get; set; }
        public string Text { get; set; }
        public Boolean HasAvatar { get; set; }
    }
}
