using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
    public class ContactController : Controller
    {
        private readonly DataContext _context;

        public ContactController(DataContext context)
        {
            _context = context;
        }

        // GET: Manager/Contact
        public async Task<IActionResult> Index()
        {
              return View(await _context.Contact.OrderByDescending(item => item.Id).ToListAsync());
        }

        // GET: Manager/Contact/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contactViewModel = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactViewModel == null)
            {
                return NotFound();
            }

            return View(contactViewModel);
        }

        // GET: Manager/Contact/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manager/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FullName,PhoneNumber,Email,Contents,ShortContent,ContactImages,ContactURL,IsGroup,ContactEnable,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactViewModel);
        }

        // GET: Manager/Contact/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contactViewModel = await _context.Contact.FindAsync(id);
            if (contactViewModel == null)
            {
                return NotFound();
            }
            return View(contactViewModel);
        }

        // POST: Manager/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FullName,PhoneNumber,Email,Contents,ShortContent,ContactImages,ContactURL,IsGroup,ContactEnable,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ContactViewModel contactViewModel)
        {
            if (id != contactViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime now = System.DateTime.Now;
                    contactViewModel.CreatedAt = now;
                    contactViewModel.CreatedBy = "Khach";
                    contactViewModel.UpdatedAt = now;
                    contactViewModel.ContactEnable = true;
                    contactViewModel.UpdatedBy = "quản trị viên";

                    _context.Update(contactViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactViewModelExists(contactViewModel.Id))
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
            return View(contactViewModel);
        }

        // GET: Manager/Contact/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contactViewModel = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactViewModel == null)
            {
                return NotFound();
            }

            return View(contactViewModel);
        }

        // POST: Manager/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contact == null)
            {
                return Problem("Entity set 'DataContext.Contact'  is null.");
            }
            var contactViewModel = await _context.Contact.FindAsync(id);
            if (contactViewModel != null)
            {
                _context.Contact.Remove(contactViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactViewModelExists(int id)
        {
          return _context.Contact.Any(e => e.Id == id);
        }
    }
}
