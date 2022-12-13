using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tp3.Models;

namespace tp3.Data
{
    public class tp3Context : IdentityDbContext<User>
    {
        public tp3Context (DbContextOptions<tp3Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Photo>()
                .HasOne(e => e.Galerie)
                .WithMany(p => p.PhotoList);

            builder.Entity<Photo>()
                .HasOne(e => e.GalerieCouverture)
                .WithOne(p => p.PhotoCouverture)
                .HasForeignKey<Photo>(e => e.GalerieId);


            base.OnModelCreating(builder);
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            User u1 = new User
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "FakeBob",
                Email= "bob@bob.com",
                NormalizedEmail = "BOB@BOB.COM",
                NormalizedUserName="FAKEBOB"    
            };
            User u2 = new User
            {
                Id = "11111111-1111-1111-1111-111111111112",
                UserName = "FakeBIb",
                Email = "bib@bib.com",
                NormalizedEmail = "BIB@BIB.COM",
                NormalizedUserName = "FAKEBIB"
            };
            u2.PasswordHash = hasher.HashPassword(u2, "123456789");
            builder.Entity<User>().HasData(u2);

            u1.PasswordHash = hasher.HashPassword(u1, "123456789" +
                "");
            builder.Entity<User>().HasData(u1);


            builder.Entity<Galerie>().HasData(new 
            {
                Id = 1,
                Name="Test",
                IsPublic = true
               
            }, 
            new 
            {
                Id = 2,
                Name = "Test1",
                IsPublic = true
              
            }
            );

            builder.Entity<Galerie>()
                .HasMany(u => u.Utilisateur)
                .WithMany(c => c.GalerieUtil)
                .UsingEntity(e =>
                {
                    e.HasData(new { UtilisateurId = u1.Id, GalerieUtilId = 1 });
                    e.HasData(new { UtilisateurId = u1.Id, GalerieUtilId = 2 });
                    e.HasData(new { UtilisateurId = u2.Id, GalerieUtilId = 1 });
                    e.HasData(new { UtilisateurId = u2.Id, GalerieUtilId = 2 });
                });
        }

        public DbSet<tp3.Models.Galerie> Galerie { get; set; }

        public DbSet<tp3.Models.Photo> Photo { get; set; }

      
    }
}
