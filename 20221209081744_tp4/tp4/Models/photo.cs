using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tp3.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string NomFichier { get; set; }
        public string TypeFichier { get; set; }
        [JsonIgnore]
        public virtual Galerie Galerie { get; set; }

        public int? GalerieId { get; set; }

        [JsonIgnore]
        public virtual Galerie? GalerieCouverture { get; set; }
    }
}
