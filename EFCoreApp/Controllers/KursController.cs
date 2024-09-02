using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            if (ModelState.IsValid)
            {
                _context.Kurslar.Add(model);
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

            var kurs = await _context
                .Kurslar
                .Include(x => x.KursKayitlari)
                .ThenInclude(x => x.Ogrenci)
                .FirstOrDefaultAsync(x => x.KursId == id);

            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kurs model)
        {
            if (id != model.KursId)
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
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
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
            if (id == null)
            {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm([FromForm] int KursId)
        {
            var kurs = await _context.Kurslar.FindAsync(KursId);
            if (kurs == null)
            {
                return NotFound();
            }
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
