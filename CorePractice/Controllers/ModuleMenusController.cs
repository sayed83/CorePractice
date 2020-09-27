using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CorePractice.Data;
using CorePractice.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CorePractice.Controllers
{
    public class ModuleMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModuleMenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ModuleMenus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ModuleMenu.Include(m => m.ImageCriteria).Include(m => m.ModuleGroup).Include(m => m.Modules);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ModuleMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleMenu = await _context.ModuleMenu
                .Include(m => m.ImageCriteria)
                .Include(m => m.ModuleGroup)
                .Include(m => m.Modules)
                .FirstOrDefaultAsync(m => m.ModuleMenuId == id);
            if (moduleMenu == null)
            {
                return NotFound();
            }

            return View(moduleMenu);
        }

        // GET: ModuleMenus/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewData["ImageCriteriaId"] = new SelectList(_context.ImageCriterias, "ImageCriteriaId", "ImageCriteriaCaption");
            ViewData["ModuleGroupId"] = new SelectList(_context.ModuleGroups, "ModuleGroupId", "ModuleGroupName");
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleName");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModuleMenu moduleMenu, string imageDatatest)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Any())
           .Select(x => new { x.Key, x.Value.Errors });

            if (ModelState.IsValid)
            {
                if (moduleMenu.ModuleMenuId > 0)
                {
                    _context.Entry(moduleMenu).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    if (moduleMenu.ParentId == -1)
                    {
                        moduleMenu.ParentId = 0;
                    }
                    var error = ModelState.Where(x => x.Value.Errors.Any())
                    .Select(x => new { x.Key, x.Value.Errors });
                    _context.Add(moduleMenu);
                    await _context.SaveChangesAsync();
                    _context.Entry(moduleMenu).GetDatabaseValues();
                    int id = moduleMenu.ModuleMenuId;
                    return RedirectToAction(nameof(Index));
                }
                    
            }
            ViewData["ImageCriteriaId"] = new SelectList(_context.ImageCriterias, "ImageCriteriaId", "ImageCriteriaCaption", moduleMenu.ImageCriteriaId);
            ViewData["ModuleGroupId"] = new SelectList(_context.ModuleGroups, "ModuleGroupId", "ModuleGroupName", moduleMenu.ModuleGroupId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleName", moduleMenu.ModuleId);
            return View(moduleMenu);
        }

        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        private Image HandleImageUpload(byte[] binaryImage, string path)
        {
            Image img = RezizeImage(Image.FromStream(BytearrayToStream(binaryImage)), 300, 300);
            img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            return img;
        }

        private Image RezizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    //gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }

        // GET: ModuleMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleMenu = await _context.ModuleMenu.FindAsync(id);
            if (moduleMenu == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            ViewData["ImageCriteriaId"] = new SelectList(_context.ImageCriterias, "ImageCriteriaId", "ImageCriteriaCaption", moduleMenu.ImageCriteriaId);
            ViewData["ModuleGroupId"] = new SelectList(_context.ModuleGroups, "ModuleGroupId", "ModuleGroupName", moduleMenu.ModuleGroupId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleName", moduleMenu.ModuleId);
            ViewBag.ParentId = new SelectList(_context.ModuleMenu.Where(x => x.ParentId == moduleMenu.ParentId), "ModuleMenuId", "ModuleMenuName", moduleMenu.ParentId);
            return View("Create",moduleMenu);
        }

        // POST: ModuleMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleMenuId,ModuleMenuName,ModuleMenuCaption,ModuleMenuImage,ImagePath,ModuleImageExtension,ModuleMenuController,ModuleMenuLink,IsInActive,IsParent,Active,SLNO,ModuleId,ModuleGroupId,ImageCriteriaId,ParentId")] ModuleMenu moduleMenu)
        {
            if (id != moduleMenu.ModuleMenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleMenuExists(moduleMenu.ModuleMenuId))
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
            ViewData["ImageCriteriaId"] = new SelectList(_context.ImageCriterias, "ImageCriteriaId", "ImageCriteriaCaption", moduleMenu.ImageCriteriaId);
            ViewData["ModuleGroupId"] = new SelectList(_context.ModuleGroups, "ModuleGroupId", "ModuleGroupName", moduleMenu.ModuleGroupId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleName", moduleMenu.ModuleId);
            return View(moduleMenu);
        }

        // GET: ModuleMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleMenu = await _context.ModuleMenu
                .Include(m => m.ImageCriteria)
                .Include(m => m.ModuleGroup)
                .Include(m => m.Modules)
                .FirstOrDefaultAsync(m => m.ModuleMenuId == id);
            if (moduleMenu == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            return View(moduleMenu);
        }

        // POST: ModuleMenus/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var moduleMenu = await _context.ModuleMenu.FindAsync(id);
                _context.ModuleMenu.Remove(moduleMenu);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Json(new { Success = 1, ModuleMenuId = moduleMenu.ModuleMenuId, ex = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }            
        }

        public JsonResult GetModuleGroup(int id)
        {
            List<ModuleGroup> ModuleGroupList = _context.ModuleGroups.Where(m => m.ModuleId == id).ToList();

            List<SelectListItem> ListOfModuleGroup = new List<SelectListItem>();

            //licities.Add(new SelectListItem { Text = "--Select State--", Value = "0" });
            if (ModuleGroupList != null)
            {
                foreach (ModuleGroup x in ModuleGroupList)
                {
                    ListOfModuleGroup.Add(new SelectListItem { Text = x.ModuleGroupName, Value = x.ModuleGroupId.ToString() });
                }
            }
            return Json(new SelectList(ListOfModuleGroup, "Value", "Text"));
        }

        public JsonResult GetModuleMenu(int id)
        {
            List<ModuleMenu> ParentMenuList = _context.ModuleMenu.Where(m => m.ModuleGroupId == id && m.IsParent == 1).ToList();

            List<SelectListItem> listparentmenu = new List<SelectListItem>();
            if (ParentMenuList != null)
            {
                foreach (ModuleMenu x in ParentMenuList)
                {
                    listparentmenu.Add(new SelectListItem { Text = x.ModuleMenuName, Value = x.ModuleMenuId.ToString() });
                }
            }
            return Json(new SelectList(listparentmenu, "Value", "Text"));
        }

        private bool ModuleMenuExists(int id)
        {
            return _context.ModuleMenu.Any(e => e.ModuleMenuId == id);
        }
    }
}
