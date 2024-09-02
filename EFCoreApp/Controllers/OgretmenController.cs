using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly DataContext _context;

        public OgretmenController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ogretmenler = await _context.Ogretmenler.ToListAsync();
            return View(ogretmenler);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            if (ModelState.IsValid)
            {
                _context.Ogretmenler.Add(model);
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
            var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(x => x.OgretmenId == id);
            if (ogretmen == null)
            {
                return NotFound();
            }
            return View(ogretmen);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId)
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
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
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
            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if (ogretmen == null)
            {
                return NotFound();
            }
            return View(ogretmen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int OgretmenId)
        {
            var ogretmen = await _context.Ogretmenler.FindAsync(OgretmenId);
            if (ogretmen == null)
            {
                return NotFound();
            }
            _context.Ogretmenler.Remove(ogretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
