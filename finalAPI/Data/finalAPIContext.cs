using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using finalAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace finalAPI.Data
{
    public class finalAPIContext : IdentityDbContext<User>
    {
        public finalAPIContext (DbContextOptions<finalAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            User u1 = new User
            {
                Id = "11111111-1111-1111-111111111111",
                UserName = "chad",
                Email = "chad@chad.chad",
                NormalizedEmail = "CHAD@CHAD.CHAD",
                NormalizedUserName = "CHAD",
                FileName = "5e27b508-7ed6-41ad-bac4-6f266d96ee80.png",
                MimeType = "image/png"
            };
            u1.PasswordHash = hasher.HashPassword(u1, "chadpass");
            User u2 = new User
            {
                Id = "11111111-1111-1111-111111111112",
                UserName = "stacy",
                Email = "stacy@stacy.stacy",
                NormalizedEmail = "STACY@STACY.STACY",
                NormalizedUserName = "STACY",
                FileName = "c73529a7-63f3-4dc0-8d12-356c93a4e924.png",
                MimeType = "image/png"
            };
            u2.PasswordHash = hasher.HashPassword(u2, "stacypass");
            User u3 = new User
            {
                Id = "11111111-1111-1111-111111111113",
                UserName = "karen",
                Email = "karen@karen.karen",
                NormalizedEmail = "KAREN@KAREN.KAREN",
                NormalizedUserName = "KAREN",
                FileName = "1dda9998-c27b-4acf-807d-b75b95cbe57d.png",
                MimeType = "image/png"
            };
            u3.PasswordHash = hasher.HashPassword(u3, "karenpass");
            builder.Entity<User>().HasData(u1, u2, u3);

            builder.Entity<Thread>().HasData(
            new {
                Id = 1, Title = "When does Cyberpunk 2077 release ??"
            },
            new
            {
                Id = 2, Title = "Where is the manager ?!"
            },
            new
            {
                Id = 3, Title = "The Gingerbread Man Dilemma"
            });

            builder.Entity<Message>().HasData(
            new
            {
                Id = 1,
                UserId = "11111111-1111-1111-111111111113",
                ThreadId = 1,
                Text = "It's incredible. Delay, after delay, after delay, after delay. This game is never gonna come out and my son needs it ! Does anyone know where is the cyberpunk 2077 manager ?"
            },
            new
            {
                Id = 6,
                UserId = "11111111-1111-1111-111111111113",
                ThreadId = 1,
                Text = "j'Espere jene coule pas"
            },
            new
            {
                Id = 2,
                UserId = "11111111-1111-1111-111111111111",
                ThreadId = 1,
                Text = "Karen, the game is out since december 2020... Anyway, do your son a favor and tell him the developers cancelled the project."
            },
            new 
            {
                Id = 3,
                UserId = "11111111-1111-1111-111111111113",
                ThreadId = 2,
                Text = "Does anyone know where is the manager on bluedit ? I had terrible service so far. Most people here aare so entitled. Those millenials, I tell you."
            },
            new 
            {
                Id = 4,
                UserId = "11111111-1111-1111-111111111111",
                ThreadId = 3,
                Text = "Okay, hear me out. If a gingerbread man is standing in a gingerbread house, is he made of house or is the house made of flesh ? Plz help me out on this one I haven't slept for 3 days."
            },
            new
            {
                Id = 5,
                UserId = "11111111-1111-1111-111111111112",
                ThreadId = 3,
                Text = "For god's sake Chad what did you take and where can I get some ?"
            });
        }

        public DbSet<finalAPI.Models.Thread> Thread { get; set; }

        public DbSet<finalAPI.Models.Message> Message { get; set; }
    }
}
