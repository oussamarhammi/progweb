using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp3.Data;
using tp3.Models;

namespace tp3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GaleriesController : ControllerBase
    {
        private readonly tp3Context _context;

        public GaleriesController(tp3Context context)
        {
            _context = context;
        }

        // GET: api/Galeries
        [HttpGet]
        public ActionResult<IEnumerable<Galerie>> GetGalerie()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = _context.Users.Single(u => u.Id == userId);
            return user.GalerieUtil;
        }


        // GET: api/Galeries/5
        [HttpGet]
        [AllowAnonymous]
        [Route("publique")]
        public  ActionResult<IEnumerable<Galerie>> GetGaleriePublique()
        {

            var gal =this._context.Galerie.Where(f => f.IsPublic == true).ToList();


            return gal;
        }

        // PUT: api/Galeries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> AjoutProprio(int id, ProprioDTO proprio)
        {
            var galerie = await _context.Galerie.FindAsync(id);
            if (galerie == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = _context.Users.Single(u => u.Id == userId);
            if (!galerie.Utilisateur.Contains(user))
            {
                return BadRequest();
            }
            //trouver l utili qui correspond au nom util remplace id par username et userid par proprio dams propriodto
           
            User user1 = _context.Users.Single(u => u.UserName == proprio.NomUtil);
            user1.GalerieUtil.Add(galerie);
            galerie.Utilisateur.Add(user1);
            
            //user.add.user(creation lien entre galerie et user
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // POST: api/Galeries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Galerie>> PostGalerie(Galerie galerie)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = _context.Users.Single(u => u.Id == userId);
            galerie.Utilisateur = new List<User>();
            galerie.Utilisateur.Add(user);
            user.GalerieUtil.Add(galerie);
            _context.Galerie.Add(galerie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGalerie", new { id = galerie.Id }, galerie);
        }


        // DELETE: api/Galeries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGalerie(int id)
        {
            var galerie = await _context.Galerie.FindAsync(id);
            if (galerie == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = _context.Users.Single(u => u.Id == userId);
            if(!galerie.Utilisateur.Contains(user))
            {
                return BadRequest();
            }
            List<User> proprio = galerie.Utilisateur;
            foreach(User prop in proprio)
            {
                prop.GalerieUtil.Remove(galerie);
            }
            galerie.Utilisateur.Clear();

            _context.Galerie.Remove(galerie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GalerieExists(int id)
        {
            return _context.Galerie.Any(e => e.Id == id);
        }
    }
}
