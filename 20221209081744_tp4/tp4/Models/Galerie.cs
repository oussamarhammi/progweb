using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tp3.Models
{
    public class Galerie
    { 
            public int Id { get; set; }

            public string Name { get; set; }

             public bool IsPublic { get; set; }
        [JsonIgnore]
            public virtual List<User> Utilisateur { get; set; }

        public virtual List<Photo> PhotoList { get; set; }

        public virtual Photo? PhotoCouverture  { get; set; }

    }
}
