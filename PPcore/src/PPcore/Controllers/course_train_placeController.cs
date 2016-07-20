using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPcore.Models;

namespace PPcore.Controllers
{
    public class course_train_placeController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public course_train_placeController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: course_train_place
        public async Task<IActionResult> Index()
        {
            return View(await _context.course_train_place.ToListAsync());
        }

        // GET: course_train_place/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_train_place = await _context.course_train_place.SingleOrDefaultAsync(m => m.place_code == id);
            if (course_train_place == null)
            {
                return NotFound();
            }

            return View(course_train_place);
        }

        // GET: course_train_place/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: course_train_place/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("place_code,course_code,confirm_date,id,place_cost,ref_doc,x_log,x_note,x_status")] course_train_place course_train_place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course_train_place);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course_train_place);
        }

        // GET: course_train_place/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_train_place = await _context.course_train_place.SingleOrDefaultAsync(m => m.place_code == id);
            if (course_train_place == null)
            {
                return NotFound();
            }
            return View(course_train_place);
        }

        // POST: course_train_place/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("place_code,course_code,confirm_date,id,place_cost,ref_doc,x_log,x_note,x_status")] course_train_place course_train_place)
        {
            if (id != course_train_place.place_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course_train_place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!course_train_placeExists(course_train_place.place_code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(course_train_place);
        }

        // GET: course_train_place/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_train_place = await _context.course_train_place.SingleOrDefaultAsync(m => m.place_code == id);
            if (course_train_place == null)
            {
                return NotFound();
            }

            return View(course_train_place);
        }

        // POST: course_train_place/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course_train_place = await _context.course_train_place.SingleOrDefaultAsync(m => m.place_code == id);
            _context.course_train_place.Remove(course_train_place);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool course_train_placeExists(string id)
        {
            return _context.course_train_place.Any(e => e.place_code == id);
        }
    }
}
