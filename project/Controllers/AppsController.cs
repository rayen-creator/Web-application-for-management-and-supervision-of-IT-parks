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
    public class AppsController : Controller
    {
        private readonly ParcInfoContext _context;

        public AppsController(ParcInfoContext context)
        {
            _context = context;
        }

        // GET: Apps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apps.ToListAsync());
        }

        // GET: Apps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = await _context.Apps
                .SingleOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // GET: Apps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] App app)
        {
            if (ModelState.IsValid)
            {
                _context.Add(app);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(app);
        }

        // GET: Apps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = await _context.Apps.SingleOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Apps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] App app)
        {
            if (id != app.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(app);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppExists(app.Id))
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
            return View(app);
        }

        // GET: Apps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = await _context.Apps.SingleOrDefaultAsync(m => m.Id == id);
            _context.Apps.Remove(app);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        //// POST: Apps/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var app = await _context.Apps.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.Apps.Remove(app);
        //    await _context.SaveChangesAsync();
        //}

        private bool AppExists(int id)
        {
            return _context.Apps.Any(e => e.Id == id);
        }
    }
}
