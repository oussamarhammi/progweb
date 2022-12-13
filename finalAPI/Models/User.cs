using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace finalAPI.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public virtual List<Message> Messages { get; set; }
        [JsonIgnore]
        public string FileName { get; set; }
        [JsonIgnore]
        public string MimeType { get; set; }
    }
}
