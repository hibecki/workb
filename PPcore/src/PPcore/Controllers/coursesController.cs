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
    public class coursesController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public coursesController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        private void prepareViewBag()
        {
            ViewBag.x_status = ini_data.x_status;
            ViewBag.cgroup_code = new SelectList(_context.course_group.OrderBy(cg => cg.cgroup_code), "cgroup_code", "cgroup_desc", 1);
        }

        public IActionResult Index()
        {
            ViewBag.countRecords = _context.course.Count();
            return View();
        }

        [HttpGet]
        public IActionResult DetailsAsTable()
        {
            var p = _context.course.OrderBy(m => m.course_code);
            return View(p.ToList());
        }

        // GET: courses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.course.SingleOrDefaultAsync(m => m.course_code == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public IActionResult Create()
        {
            prepareViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("course_code,active_member_join,budget,cgroup_code,charge_head,course_approve_date,course_begin,course_date,course_desc,course_end,ctype_code,id,passed_member,project_code,project_manager,ref_doc,support_head,target_member_join,x_log,x_note,x_status")] course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: courses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.course.SingleOrDefaultAsync(m => m.id == new Guid(id));
            if (course == null)
            {
                return NotFound();
            }
            prepareViewBag();
            ViewBag.ctype_code = course.ctype_code;
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("course_code,active_member_join,budget,cgroup_code,charge_head,course_approve_date,course_begin,course_date,course_desc,course_end,ctype_code,id,passed_member,project_code,project_manager,ref_doc,support_head,target_member_join,x_log,x_note,x_status")] course course)
        {
            if (new Guid(id) != course.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                        throw;

                }
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: courses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.course.SingleOrDefaultAsync(m => m.course_code == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course = await _context.course.SingleOrDefaultAsync(m => m.course_code == id);
            _context.course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool courseExists(string id)
        {
            return _context.course.Any(e => e.course_code == id);
        }
    }
}
