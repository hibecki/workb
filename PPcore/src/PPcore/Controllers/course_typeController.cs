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
    public class course_typeController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        private void prepareViewBag()
        {
            ViewBag.x_status = new SelectList(new[] { new { Value = "Y", Text = "ใช้งาน" }, new { Value = "N", Text = "ยกเลิก" } }, "Value", "Text", "Y");
        }

        public course_typeController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: course_type
        public async Task<IActionResult> Index()
        {
            return View(await _context.course_type.ToListAsync());
        }

        // GET: course_type/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_type = await _context.course_type.SingleOrDefaultAsync(m => m.ctype_code == id);
            if (course_type == null)
            {
                return NotFound();
            }

            return View(course_type);
        }

        public IActionResult DetailsAsTable(string cgroup_code)
        {
            //ViewBag.product_group = new SelectList(_context.product_group, "product_group_code", "product_group_desc", "1");
            //ViewBag.memberId = memberId;
            var ct = _context.course_type.Where(c => (c.cgroup_code == cgroup_code)).OrderBy(c => c.cgroup_code).ToList();
            return View(ct);
        }

        // GET: course_type/Create
        public IActionResult Create()
        {
            prepareViewBag();
            return View(new course_type());
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ctype_code,cgroup_code,ctype_desc,id,rowversion,x_log,x_note,x_status")] course_type course_type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course_type);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course_type);
        }

        // GET: course_type/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_type = await _context.course_type.SingleOrDefaultAsync(m => m.ctype_code == id);
            if (course_type == null)
            {
                return NotFound();
            }
            return View(course_type);
        }

        // POST: course_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ctype_code,cgroup_code,ctype_desc,id,rowversion,x_log,x_note,x_status")] course_type course_type)
        {
            if (id != course_type.ctype_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course_type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!course_typeExists(course_type.ctype_code))
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
            return View(course_type);
        }

        // GET: course_type/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_type = await _context.course_type.SingleOrDefaultAsync(m => m.ctype_code == id);
            if (course_type == null)
            {
                return NotFound();
            }

            return View(course_type);
        }

        // POST: course_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course_type = await _context.course_type.SingleOrDefaultAsync(m => m.ctype_code == id);
            _context.course_type.Remove(course_type);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool course_typeExists(string id)
        {
            return _context.course_type.Any(e => e.ctype_code == id);
        }
    }
}
