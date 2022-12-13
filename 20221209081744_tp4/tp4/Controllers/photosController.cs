using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using tp3.Data;
using tp3.Models;

namespace tp3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class photosController : ControllerBase
    {
        private readonly tp3Context _context;

        public photosController(tp3Context context)
        {
            _context = context;
        }

        // GET: api/photos
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Photo>>> Getphoto()
        {
            return await _context.Photo.ToListAsync();
        }

        // GET: api/photos/5
        [HttpGet("{size}/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Photo>> Getphoto(string size, int id)
        {
            var photo = await _context.Photo.FindAsync(id);

            if (photo == null)
            
                return NotFound();
            
            Match m = Regex.Match(size,"originale|miniature");
            if (!m.Success)

                return BadRequest();

            byte[] bytes = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/images/" + size + "/" + photo.NomFichier);


            return File(bytes, photo.TypeFichier);
        }

        // PUT: api/photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putphoto(int id, Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!photoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("couverture/{id}")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Photo>> PostphotoCouverture(int id)
        {
            var galerie = await _context.Galerie.FindAsync(id);
            if (galerie == null)
            {
                return NotFound();
            }
            try
            {
                IFormCollection formCollection = await Request.ReadFormAsync();
                IFormFile file = formCollection.Files.GetFile("photo");
                Image image = Image.Load(file.OpenReadStream());

                Photo photo = new Photo();
               photo.NomFichier = Guid.NewGuid().ToString()
                    + Path.GetExtension(file.FileName);
                photo.TypeFichier = file.ContentType;

                image.Save(Directory.GetCurrentDirectory() + "/images/originale/" + photo.NomFichier);
                image.Mutate(i =>
                i.Resize(new ResizeOptions()
                {
                    Mode = ResizeMode.Min,
                    Size = new Size() { Height = 320 }
                }));
                image.Save(Directory.GetCurrentDirectory() + "/images/miniature/" + photo.NomFichier);
                galerie.PhotoCouverture=photo;
                photo.GalerieCouverture = galerie;
                _context.Photo.Add(photo);
                await _context.SaveChangesAsync();
            }


            catch (Exception) { }
            return Ok();
        }


        [HttpPost]
        [Route("couvertureChange/{id}")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Photo>> ChangerphotoCouverture(int id)
        {
            var galerie = await _context.Galerie.FindAsync(id);
            if (galerie == null)
            {
                return NotFound();
            }
            try
            {
                    IFormCollection formCollection = await Request.ReadFormAsync();
                    IFormFile file = formCollection.Files.GetFile("photo");

                    Image image = Image.Load(file.OpenReadStream());

                    Photo photo = new Photo();
                    photo.NomFichier = Guid.NewGuid().ToString()
                         + Path.GetExtension(file.FileName);
                    photo.TypeFichier = file.ContentType;

                    image.Save(Directory.GetCurrentDirectory() + "/images/originale/" + photo.NomFichier);
                    image.Mutate(i =>
                    i.Resize(new ResizeOptions()
                    {
                        Mode = ResizeMode.Min,
                        Size = new Size() { Height = 320 }
                    }));
                    image.Save(Directory.GetCurrentDirectory() + "/images/miniature/" + photo.NomFichier);
                    if(galerie.PhotoCouverture != null)
                    {
                        
                         Photo anciennePhoto = galerie.PhotoCouverture;
                    galerie.PhotoCouverture = null;

                        System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images//miniature" + photo.NomFichier);
                        System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images//originale" + photo.NomFichier);
                        _context.Photo.Remove(anciennePhoto);
                        _context.SaveChanges();
                    }
                    galerie.PhotoCouverture = photo;
                    photo.GalerieCouverture = galerie;
                    _context.Photo.Add(photo);
                    await _context.SaveChangesAsync();
                
                   
            }
            catch (Exception) { }
            return Ok();
        }



        // POST: api/photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Photo>> Postphoto(int id)
        {
            var galerie = await _context.Galerie.FindAsync(id);
            if (galerie == null)
            {
                return NotFound();
            }
            try
            {

                _context.Photo.Remove(galerie.PhotoCouverture);
                IFormCollection formCollection = await Request.ReadFormAsync();
                IFormFile file = formCollection.Files.GetFile("image");
                
                Image image = Image.Load(file.OpenReadStream());

                Photo photo = new Photo();
                photo.NomFichier = Guid.NewGuid().ToString()
                    + Path.GetExtension(file.FileName);
                photo.TypeFichier = file.ContentType;

                image.Save(Directory.GetCurrentDirectory() + "/images/originale/" + photo.NomFichier);
                image.Mutate(i =>
                i.Resize(new ResizeOptions()
                {
                    Mode = ResizeMode.Min,
                    Size = new Size() { Height = 320 }
                }));
                image.Save(Directory.GetCurrentDirectory() + "/images/miniature/" + photo.NomFichier);
                galerie.PhotoList.Add(photo);
                photo.Galerie= galerie;
                _context.Photo.Add(photo);
                await _context.SaveChangesAsync();
            }
            
            
            catch (Exception){}
            return Ok();
        }

        // DELETE: api/photos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletephoto(int id)
        {
            var photo = await _context.Photo.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images//miniature" + photo.NomFichier);
            System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images//originale" + photo.NomFichier);
            _context.Photo.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool photoExists(int id)
        {
            return _context.Photo.Any(e => e.Id == id);
        }
    }
}
