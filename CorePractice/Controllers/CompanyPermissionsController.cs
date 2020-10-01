using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CorePractice.Data;
using CorePractice.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CorePractice.Controllers
{
    public class CompanyPermissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyPermissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompanyPermissions
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompanyPermissions.ToListAsync());
        }

        public JsonResult getUserCompany(string UserId)
        {
            var SavedList = _context.CompanyPermissions.Where(x => x.UserId == UserId).ToList();

            var dataList = new List<CompanyPermissionVM>();

            var d = _context.Companys.FirstOrDefault();
            var cp = _context.CompanyPermissions.Where(x => x.UserId == UserId && x.ComId.ToString() == d.CompanyCode).FirstOrDefault();
            var data = new CompanyPermissionVM();

            data.UserId = UserId;
            data.CompanyName = d.CompanyName;
            data.ComId = Guid.Parse(d.CompanyCode);
            if (cp != null)
            {
                data.isChecked = cp.isChecked;
                data.isDefault = cp.isDefault;
                data.CompanyPermissionId = cp.CompanyPermissionId;
            }
            foreach (var cc in SavedList)
            {
                foreach (var item in dataList)
                {
                    if (item.ComId == cc.ComId)
                    {
                        item.isChecked = cc.isChecked;
                        item.isDefault = cc.isDefault;
                        item.CompanyPermissionId = cc.CompanyPermissionId;
                    }
                }
            }
            return Json(data);
        }

            // GET: CompanyPermissions/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyPermission = await _context.CompanyPermissions
                .FirstOrDefaultAsync(m => m.CompanyPermissionId == id);
            if (companyPermission == null)
            {
                return NotFound();
            }

            return View(companyPermission);
        }

        // GET: CompanyPermissions/Create
        public IActionResult Create()
        {
            var listado = _context.Users.ToList();

             //listado = (from user in _context.Users
             //                   join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
             //                   join role in _context.Roles on userRoles.RoleId equals role.Id
             //                   select new { UserId = user.Id, UserName = user.UserName, RoleId = role.Id, RoleName = role.Name })
             //           .ToListAsync();
            var data = new CompanyPermissionVM();
            List<AspnetUserList> aspNetUser = new List<AspnetUserList>();

            foreach (var user in listado)
            {
                var u = new AspnetUserList();
                u.UserId = user.Id;
                u.UserName = user.UserName;
                aspNetUser.Add(u);
            }
            ViewBag.UserId = new SelectList(aspNetUser, "UserId", "UserName");

           // var comid = HttpContext.Session.GetString("comid");
            var comName = _context.Companys.Select(x => x.CompanyName).FirstOrDefault();
            var cp = _context.CompanyPermissions.Select(x => new { x.ComId, x.UserId, x.CompanyPermissionId, x.isChecked, x.isDefault }).FirstOrDefault();

            data = new CompanyPermissionVM();
            data.UserId = cp.UserId;
            data.CompanyName = comName;
            data.ComId = cp.ComId;
            data.isChecked = cp.isChecked;
            data.isDefault = cp.isDefault;
            data.CompanyPermissionId = cp.CompanyPermissionId;

            return View(data);
        }

        // POST: CompanyPermissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(string companyPermission)
        {

            JObject str = JObject.Parse(companyPermission);
            var result = str["strfy"].ToString();

            var cp = JsonConvert.DeserializeObject<CompanyPermission>(result);
            if (ModelState.IsValid)
            {
                //var userid = HttpContext.Session.GetString("userid");
                if (cp.CompanyPermissionId > 0)
                {
                    if (cp.isChecked == 1)
                    {
                        _context.Entry(cp).State = EntityState.Modified;
                    }
                    else
                    {
                        var x = _context.CompanyPermissions.Where(p => p.CompanyPermissionId == cp.CompanyPermissionId).FirstOrDefault();
                        _context.CompanyPermissions.Remove(x);
                    }
                    _context.SaveChanges();
                }
                else
                {
                    if (cp.isChecked == 1)
                    {
                        _context.CompanyPermissions.Add(cp);
                        _context.SaveChanges();
                    }
                }


                return Json(new { Success = 1, ex = "" });
            }
            return View(companyPermission);
        }

        // GET: CompanyPermissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyPermission = await _context.CompanyPermissions.FindAsync(id);
            if (companyPermission == null)
            {
                return NotFound();
            }
            return View(companyPermission);
        }

        // POST: CompanyPermissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyPermissionId,ComId,isDefault,isChecked,UserId")] CompanyPermission companyPermission)
        {
            if (id != companyPermission.CompanyPermissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyPermission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyPermissionExists(companyPermission.CompanyPermissionId))
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
            return View(companyPermission);
        }

        // GET: CompanyPermissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyPermission = await _context.CompanyPermissions
                .FirstOrDefaultAsync(m => m.CompanyPermissionId == id);
            if (companyPermission == null)
            {
                return NotFound();
            }

            return View(companyPermission);
        }

        // POST: CompanyPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyPermission = await _context.CompanyPermissions.FindAsync(id);
            _context.CompanyPermissions.Remove(companyPermission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyPermissionExists(int id)
        {
            return _context.CompanyPermissions.Any(e => e.CompanyPermissionId == id);
        }
    }
}
