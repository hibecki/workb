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
    public class course_groupController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public course_groupController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: course_group
        public IActionResult Index()
        {
            var cg = _context.course_group.OrderByDescending(m => m.rowversion);
            ViewBag.countRecords = cg.Count();
            return View(cg.ToList());
        }

        // GET: course_group/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_group = await _context.course_group.SingleOrDefaultAsync(m => m.cgroup_code == id);
            if (course_group == null)
            {
                return NotFound();
            }

            return View(course_group);
        }

        // GET: course_group/Create
        public IActionResult Create()
        {
            ViewBag.x_status = new SelectList(new[] { new { Value = "N", Text = "เตรียมการ" }, new { Value = "Y", Text = "เปิด" } }, "Value", "Text");
            return View();
        }

        // POST: course_group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cgroup_code,cgroup_desc,id,rowversion,x_log,x_note,x_status")] course_group course_group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course_group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course_group);
        }

        // GET: course_group/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_group = await _context.course_group.SingleOrDefaultAsync(m => m.cgroup_code == id);
            if (course_group == null)
            {
                return NotFound();
            }
            return View(course_group);
        }

        // POST: course_group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("cgroup_code,cgroup_desc,id,rowversion,x_log,x_note,x_status")] course_group course_group)
        {
            if (id != course_group.cgroup_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course_group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!course_groupExists(course_group.cgroup_code))
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
            return View(course_group);
        }

        // GET: course_group/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course_group = await _context.course_group.SingleOrDefaultAsync(m => m.cgroup_code == id);
            if (course_group == null)
            {
                return NotFound();
            }

            return View(course_group);
        }

        // POST: course_group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course_group = await _context.course_group.SingleOrDefaultAsync(m => m.cgroup_code == id);
            _context.course_group.Remove(course_group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool course_groupExists(string id)
        {
            return _context.course_group.Any(e => e.cgroup_code == id);
        }
    }
}
