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
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            return View(await _context.Modules.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleId,ModuleCode,ModuleName,ModuleCaption,ModuleDescription,ModuleLink,ModuleImage,ModuleImagePath,ModuleImageExtension,IsInActive,SLNO")] Module @module, string imageDatatest)
        {
            module.IsInActive = 0;

            var errors = ModelState.Where(x => x.Value.Errors.Any())
          .Select(x => new { x.Key, x.Value.Errors });

            if (ModelState.IsValid)
            {
                if (module.ModuleId > 0)
                {
                    _context.Entry(module).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    _context.Add(@module);
                    await _context.SaveChangesAsync();
                    _context.Entry(module).GetDatabaseValues();
                    return RedirectToAction(nameof(Index));
                }
                    
            }
            return View(@module);
        }
        private Image HandleImageUpload(byte[] binaryImage, string path)
        {
            Image img = RezizeImage(Image.FromStream(BytearrayToStream(binaryImage)), 300, 300);
            img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            return img;
        }
        private static Image RezizeImage(Image img, int maxWidth, int maxHeight)
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
        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Title = "Edit";
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            return View("Create",@module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleId,ModuleCode,ModuleName,ModuleCaption,ModuleDescription,ModuleLink,ModuleImage,ModuleImagePath,ModuleImageExtension,IsInActive,SLNO")] Module @module)
        {
            if (id != @module.ModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.ModuleId))
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
            return View(@module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (@module == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";

            return View("Create",@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var @module = await _context.Modules.FindAsync(id);
                _context.Modules.Remove(@module);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Json(new { Success = 1, ModuleId = module.ModuleId, ex = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = 0,
                    ex = ex.InnerException.InnerException.Message.ToString()
                });
            }
            
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.ModuleId == id);
        }
    }
}
