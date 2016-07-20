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
    public class course_instructorController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public course_instructorController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var c = _context.course.SingleOrDefault(m => m.id == new Guid(id));
            ViewBag.courseId = c.id;
            ViewBag.courseCode = c.course_code;
            ViewBag.countRecords = _context.course_instructor.Where(ci => ci.course_code == c.course_code).Count();
            return View(new course_instructor());
        }

        public IActionResult DetailsAsTable(string id, string code)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ci = _context.course_instructor.Where(m => m.course_code == code);
            if (ci == null)
            {
                return NotFound();
            }

            return View(ci.ToList());
        }

        // GET: course_instructor/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_instructor = await _context.course_instructor.SingleOrDefaultAsync(m => m.instructor_code == id);
            if (course_instructor == null)
            {
                return NotFound();
            }

            return View(course_instructor);
        }

        // GET: course_instructor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: course_instructor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("instructor_code,course_code,confirm_date,id,instructor_cost,ref_doc,x_log,x_note,x_status")] course_instructor course_instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course_instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course_instructor);
        }

        public IActionResult EditAsTable()
        {
            var p = _context.instructor.OrderBy(m => m.instructor_code).ToList();
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        // GET: course_instructor/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_instructor = await _context.course_instructor.SingleOrDefaultAsync(m => m.instructor_code == id);
            if (course_instructor == null)
            {
                return NotFound();
            }
            return View(course_instructor);
        }

        // POST: course_instructor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("instructor_code,course_code,confirm_date,id,instructor_cost,ref_doc,x_log,x_note,x_status")] course_instructor course_instructor)
        {
            if (id != course_instructor.instructor_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course_instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!course_instructorExists(course_instructor.instructor_code))
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
            return View(course_instructor);
        }

        // GET: course_instructor/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_instructor = await _context.course_instructor.SingleOrDefaultAsync(m => m.instructor_code == id);
            if (course_instructor == null)
            {
                return NotFound();
            }

            return View(course_instructor);
        }

        // POST: course_instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course_instructor = await _context.course_instructor.SingleOrDefaultAsync(m => m.instructor_code == id);
            _context.course_instructor.Remove(course_instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool course_instructorExists(string id)
        {
            return _context.course_instructor.Any(e => e.instructor_code == id);
        }
    }
}
