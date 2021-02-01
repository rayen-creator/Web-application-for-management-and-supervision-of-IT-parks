using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Controllers
{
    public class PcsController : Controller
    {
        private readonly ParcInfoContext _context;

        public PcsController(ParcInfoContext context)
        {
            _context = context;
        }

        // GET: Pcs
        public async Task<IActionResult> Index()
        {
            var parcInfoContext = _context.Pcs.Include(p => p.Parc);
            return View(await parcInfoContext.ToListAsync());
        }

        //// GET: Pcs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pc = await _context.Pcs
        //        .Include(p => p.Parc)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (pc == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(pc);
        //}

        // GET: Pcs/Create
        public IActionResult Create()
        {
            ViewData["ParcID"] = new SelectList(_context.Parcs, "ID", "Name",null);
            return View();
        }

        // POST: Pcs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IP,Adress_Mac,Name,ParcID,AD")] Pc pc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcID"] = new SelectList(_context.Parcs, "ID", "Name", pc.ParcID);
            return View(pc);
        }

        // GET: Pcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pc = await _context.Pcs.SingleOrDefaultAsync(m => m.Id == id);
            if (pc == null)
            {
                return NotFound();
            }
            ViewData["ParcID"] = new SelectList(_context.Parcs, "ID", "Name", pc.ParcID);
            return View(pc);
        }

        // POST: Pcs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IP,Adress_Mac,Name,ParcID,AD")] Pc pc)
        {
            if (id != pc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PcExists(pc.Id))
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
            ViewData["ParcID"] = new SelectList(_context.Parcs, "ID", "Name", pc.ParcID);
            return View(pc);
        }

        // GET: Pcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pc = await _context.Pcs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pcs.Remove(pc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        //// POST: Pcs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var pc = await _context.Pcs.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.Pcs.Remove(pc);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PcExists(int id)
        {
            return _context.Pcs.Any(e => e.Id == id);
        }
    }
}
