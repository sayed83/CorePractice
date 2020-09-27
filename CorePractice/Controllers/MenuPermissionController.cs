using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CorePractice.Data;
using CorePractice.Models;

namespace CorePractice.Controllers
{
    public class MenuPermissionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuPermissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuPermission
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuPermission_Masters.ToListAsync());
        }

        // GET: MenuPermission/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPermission_Master = await _context.MenuPermission_Masters
                .FirstOrDefaultAsync(m => m.MenuPermissionMasterId == id);
            if (menuPermission_Master == null)
            {
                return NotFound();
            }

            return View(menuPermission_Master);
        }

        // GET: MenuPermission/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuPermission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuPermissionMasterId,useridPermission,comid,userid,useridUpdate,Active,AddedBy,AddedDate")] MenuPermission_Master menuPermission_Master)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuPermission_Master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuPermission_Master);
        }

        // GET: MenuPermission/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPermission_Master = await _context.MenuPermission_Masters.FindAsync(id);
            if (menuPermission_Master == null)
            {
                return NotFound();
            }
            return View(menuPermission_Master);
        }

        // POST: MenuPermission/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuPermissionMasterId,useridPermission,comid,userid,useridUpdate,Active,AddedBy,AddedDate")] MenuPermission_Master menuPermission_Master)
        {
            if (id != menuPermission_Master.MenuPermissionMasterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuPermission_Master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuPermission_MasterExists(menuPermission_Master.MenuPermissionMasterId))
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
            return View(menuPermission_Master);
        }

        // GET: MenuPermission/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPermission_Master = await _context.MenuPermission_Masters
                .FirstOrDefaultAsync(m => m.MenuPermissionMasterId == id);
            if (menuPermission_Master == null)
            {
                return NotFound();
            }

            return View(menuPermission_Master);
        }

        // POST: MenuPermission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuPermission_Master = await _context.MenuPermission_Masters.FindAsync(id);
            _context.MenuPermission_Masters.Remove(menuPermission_Master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuPermission_MasterExists(int id)
        {
            return _context.MenuPermission_Masters.Any(e => e.MenuPermissionMasterId == id);
        }
    }
}
