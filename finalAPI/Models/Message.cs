using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace finalAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public string Text { get; set; }
        [JsonIgnore]
        public virtual Thread Thread { get; set; }
    }
}
