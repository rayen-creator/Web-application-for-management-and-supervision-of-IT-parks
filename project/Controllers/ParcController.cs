using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Controllers
{
    [Authorize(Roles = "Admin,OrdinaryUser,nomodifier")]
    public class ParcController : Controller
    {
        private readonly ParcInfoContext _context;

        public ParcController(ParcInfoContext context)
        {
            _context = context;
        }

        // GET: Parc
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parcs.ToListAsync());
        }


        [Authorize(Roles = "Admin, OrdinaryUserNoModif,nomodifier")]
        // GET: Parc/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin,OrdinaryUser,nomodifier")]
        // POST: Parc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Address,Tel,Fax")] Parc parc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parc);
        }

        [Authorize(Roles = "Admin,OrdinaryUser")]
        // GET: Parc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parc = await _context.Parcs.SingleOrDefaultAsync(m => m.ID == id);
            if (parc == null)
            {
                return NotFound();
            }
            return View(parc);
        }

        [Authorize(Roles = "Admin,OrdinaryUser")]
        // POST: Parc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,Tel,Fax")] Parc parc)
        {
            if (id != parc.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcExists(parc.ID))
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
            return View(parc);
        }

        [Authorize(Roles = "Admin,OrdinaryUser")]
        // GET: Parc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parc = await _context.Parcs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Parcs.Remove(parc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        

        private bool ParcExists(int id)
        {
            return _context.Parcs.Any(e => e.ID == id);
        }
    }
}
