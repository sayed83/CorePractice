using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CorePractice.Data;
using CorePractice.Models;
using CorePractice.Component;
using System.Data.SqlClient;

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
        public ActionResult Index()
        {
            var query = "";
            query = $"Exec prcgetAspnetUserList 'MenuPermission'";
            SqlParameter[] sqlParameter1 = new SqlParameter[1];
            sqlParameter1[0] = new SqlParameter("@Criteria", "MenuPermission");
            List<MenuPermission> menuPermission = Helper.ExecProcMapTList<MenuPermission>("prcgetAspnetUserList", sqlParameter1).ToList();
            var resultUsers = new List<MenuPermission>();
            foreach (var menu in menuPermission)
            {
                var resultUser = new MenuPermission();
                resultUser.UserId = menu.UserId;
                resultUser.useridPermission = menu.useridPermission;
                resultUser.UserName = menu.UserName;
                resultUser.CompanyName = menu.CompanyName;
                resultUser.comid = menu.comid;
                resultUser.Email = menu.UserName;
                resultUser.MenuPermissionId = menu.MenuPermissionId;
                resultUsers.Add(resultUser);
            }
            ViewBag.MenuPermission = resultUsers;
            return View();
        }

        public class MenuPermission
        {
            public int MenuPermissionId { get; set; }
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string comid { get; set; }
            public string CompanyName { get; set; }
            public string useridPermission { get; set; }

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
        public IActionResult Create(string comid, string UserId,int? MenuPermissionId, int? isDelete)
        {

            return View();
        }

        // POST: MenuPermission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
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

