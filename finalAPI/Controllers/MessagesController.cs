using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finalAPI.Data;
using finalAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace finalAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly finalAPIContext _context;

        public MessagesController(finalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<MessageDTO>>> GetThreadMessages(int id)
        {
            // L'id fourni est celui du thread dont on souhaite obtenir les messages
            var thread = await _context.Thread.FindAsync(id);

            if (thread == null)
            {
                return NotFound();
            }

            List<MessageDTO> messages = new List<MessageDTO>();

            foreach(Message m in thread.Messages)
            {
                messages.Add(new MessageDTO{
                    Username = m.User.UserName, 
                    UserId = m.User.Id, 
                    MessageCount = m.User.Messages.Count, 
                    Text = m.Text,
                    HasAvatar = m.User.FileName != null}
                );
            }

            return messages;
        }

        // POST: api/Messages
        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<Message>> PostMessage(int id, Message message)
        {
            // Récupérer l'utilisateur qui a envoyé la requête
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = _context.Users.Single(u => u.Id == userId);

            // Retrouver le thread dans lequel on veut ajouter un message
            Thread thread = await _context.Thread.FindAsync(id);
            if(thread == null)
            {
                return NotFound();
            }

            // À COMPLÉTER
            message.User = user;
            user.Messages.Add(message);
            thread.Messages.Add(message);
            message.Thread = thread;
            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMessage", new { id = message.Id }, message);
        }

        [HttpGet("{username}")]
        [Authorize]
        public ActionResult<List<Message>> GetUserMessages(string username)
        {
            User user = _context.Users.SingleOrDefault(u => u.UserName == username);
            // Cette action est à modifier
            if (user == null)
            {
                return BadRequest();
            }
            return  user.Messages;
            
           
        }

    }
}
