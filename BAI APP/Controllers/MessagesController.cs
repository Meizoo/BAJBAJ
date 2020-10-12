using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BAI_APP.Context;
using BAI_APP.Models;
using BAI_APP.ViewModels;
using BAI_APP.Helpers.Extensions;

namespace BAI_APP.Controllers
{
    public class MessagesController : Controller
    {
        private readonly BaiContext _context;

        public MessagesController(BaiContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var baiContext = _context.Messages.AsQueryable().Include(m => m.Moderators).Include(x => x.Sender);
            return View(await baiContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetObjectFromJson<User>("User") == null)
                return RedirectToAction("Index");

            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageVM message)
        {
            if (ModelState.IsValid)
            {
                var mes = new Message()
                {
                    Content = message.Content,
                    CreationDate = DateTime.Now,
                    Id = Guid.NewGuid().ToString("N"),
                    SenderId = message.SenderId
                };
                _context.Add(mes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.Include(x => x.Moderators).FirstOrDefaultAsync(x => x.Id == id);
            var messageVm = new MessageVM(message);
            if (message == null)
            {
                return NotFound();
            }
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            var users = _context.Users.Where(x => x.Id != user.Id).Select(c => new
            {
                UserId = c.Id,
                UserLogin = c.Login
            }).ToList();
            ViewData["Users"] = new MultiSelectList(users, "UserId", "UserLogin", messageVm.ModeratorsId);
            return View(messageVm);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, MessageVM messageVm)
        {
            if (id != messageVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var message = await _context.Messages
                        .Include(x => x.Moderators)
                        .FirstOrDefaultAsync(x => x.Id == id);

                    var xd = messageVm.ModeratorsId?.Select(x => new MessageModerator()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        MessageId = id,
                        ModeratorId = x,
                        CreationDate = DateTime.Now
                    }).ToList() ?? new List<MessageModerator>();
                    if (xd.Count != 0)
                    {
                        message.Moderators = xd;
                    }

                    message.Content = messageVm.Content;
                    message.ModifiedDate = DateTime.Now;
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(messageVm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(messageVm);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(string id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
