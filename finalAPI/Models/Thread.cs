using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace finalAPI.Models
{
    public class Thread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public virtual List<Message> Messages { get; set; }
    }
}
