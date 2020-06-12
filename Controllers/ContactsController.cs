using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameListMVC.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;

namespace GameListMVC.Controllers
{
    [Authorize]

    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Contact Contact{ get; set; }
        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Authorize]
        public IActionResult Mail([Bind] Contact Contact)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("gamelist.project@onet.pl");
            mail.Subject = Contact.ContactSubject;
            mail.Body = Contact.ContactMessage;
            mail.IsBodyHtml = false;
            mail.Sender = new MailAddress("gamelist.project@onet.pl");

                mail.From = new MailAddress(Contact.ContactEmail);

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.poczta.onet.pl");
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("gamelist.project@onet.pl", "Glpmvc12");
                smtpClient.Send(mail);
            TempData["msg"] = "<script>alert('Wysłano pomyślnie!');</script>";
            
                return RedirectToAction("Index");
            }
        [Authorize]
        public IActionResult Index()
        {

            return View();
        }


        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var contact = await _context.Contacts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }

            // GET: Contacts/Create
            public IActionResult Create()
            {
                return View();
            }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContactName,ContactLastName,ContactEmail,ContactMessage")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContactName,ContactLastName,ContactEmail,ContactMessage")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }


    }
}

