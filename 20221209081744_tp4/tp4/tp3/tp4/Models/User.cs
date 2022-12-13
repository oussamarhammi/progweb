using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace tp3.Models
{
    public class User :IdentityUser
    {

        public virtual List<Galerie> GalerieUtil { get; set; }
    }
}
