using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;

        public OgrenciController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            if (ModelState.IsValid)
            {
                _context.Ogrenciler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogr = await _context
                .Ogrenciler
                .Include(x => x.KursKayitlari)
                .ThenInclude(x=>x.Kurs)
                .FirstOrDefaultAsync(x => x.OgrenciKimlik == id);

            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciKimlik)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(o => o.OgrenciKimlik == model.OgrenciKimlik))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int OgrenciKimlik)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(OgrenciKimlik);
            if (ogrenci == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
