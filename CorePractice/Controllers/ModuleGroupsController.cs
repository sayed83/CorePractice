using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CorePractice.Data;
using CorePractice.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CorePractice.Controllers
{
    public class ModuleGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModuleGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public Image _currentBitmap;
        string _FileName = "";
        string _path = "";
        string fileName = null;
        string Extension = null;

        // GET: ModuleGroups
        public async Task<IActionResult> Index()
        {
            var moduleGroups = _context.ModuleGroups.Include(m => m.Modules);
            return View(await moduleGroups.ToListAsync());
        }

        // GET: ModuleGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleGroup = await _context.ModuleGroups
                .Include(m => m.Modules)
                .FirstOrDefaultAsync(m => m.ModuleGroupId == id);
            if (moduleGroup == null)
            {
                return NotFound();
            }

            return View(moduleGroup);
        }

        // GET: ModuleGroups/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleId");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleGroupId,ModuleGroupName,ModuleGroupCaption,ModuleGroupImage,ImagePath,ImageExtension,SLNO,ModuleId")] ModuleGroup moduleGroup, string imageDatatest)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any())
           .Select(x => new { x.Key, x.Value.Errors });

                if (moduleGroup.ModuleGroupId > 0)
                {
                    _context.Entry(moduleGroup).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    _context.Add(moduleGroup);
                    _context.Entry(moduleGroup).GetDatabaseValues();
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                  
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleId", moduleGroup.ModuleId);
            return View(moduleGroup);
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

        // GET: ModuleGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleGroup = await _context.ModuleGroups.FindAsync(id);
            if (moduleGroup == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleName", moduleGroup.ModuleId);
            return View("Create",moduleGroup);
        }

        // POST: ModuleGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleGroupId,ModuleGroupName,ModuleGroupCaption,ModuleGroupImage,ImagePath,ImageExtension,SLNO,ModuleId")] ModuleGroup moduleGroup)
        {
            if (id != moduleGroup.ModuleGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleGroupExists(moduleGroup.ModuleGroupId))
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
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleId", moduleGroup.ModuleId);
            return View(moduleGroup);
        }

        // GET: ModuleGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleGroup = await _context.ModuleGroups
                .Include(m => m.Modules)
                .FirstOrDefaultAsync(m => m.ModuleGroupId == id);
            if (moduleGroup == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            return View("Create",moduleGroup);
        }

        // POST: ModuleGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var moduleGroup = await _context.ModuleGroups.FindAsync(id);
                _context.ModuleGroups.Remove(moduleGroup);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Json(new { Success = 1, ModuleGroupId = moduleGroup.ModuleGroupId, ex = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }            
        }

        private bool ModuleGroupExists(int id)
        {
            return _context.ModuleGroups.Any(e => e.ModuleGroupId == id);
        }
    }
}
