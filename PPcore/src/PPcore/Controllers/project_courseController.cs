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
    public class project_courseController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public project_courseController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        private void prepareViewBag()
        {
            ViewBag.x_status = ini_data.x_status;
            ViewBag.cgroup_code = new SelectList(_context.course_group.OrderBy(cg => cg.cgroup_code), "cgroup_code", "cgroup_desc",1);
        }

        public IActionResult Index()
        {
            ViewBag.countRecords = _context.project_course.Count();
            return View();
        }

        [HttpGet]
        public IActionResult DetailsAsTable()
        {
            var p = _context.project_course.OrderBy(m => m.course_code);
            return View(p.ToList());
        }

        // GET: project_course/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_course = await _context.project_course.SingleOrDefaultAsync(m => m.course_code == id);
            if (project_course == null)
            {
                return NotFound();
            }

            return View(project_course);
        }

        public IActionResult Create()
        {
            prepareViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("course_code,active_member_join,budget,cgroup_code,charge_head,course_approve_date,course_begin,course_date,course_desc,course_end,ctype_code,id,passed_member,project_code,project_manager,ref_doc,support_head,target_member_join,x_log,x_note,x_status")] project_course project_course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project_course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project_course);
        }

        // GET: project_course/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_course = await _context.project_course.SingleOrDefaultAsync(m => m.id == new Guid(id));
            if (project_course == null)
            {
                return NotFound();
            }
            prepareViewBag();
            ViewBag.ctype_code = project_course.ctype_code;
            return View(project_course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("course_code,active_member_join,budget,cgroup_code,charge_head,course_approve_date,course_begin,course_date,course_desc,course_end,ctype_code,id,passed_member,project_code,project_manager,ref_doc,support_head,target_member_join,x_log,x_note,x_status")] project_course project_course)
        {
            if (new Guid(id) != project_course.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project_course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction("Index");
            }
            return View(project_course);
        }

        // GET: project_course/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_course = await _context.project_course.SingleOrDefaultAsync(m => m.course_code == id);
            if (project_course == null)
            {
                return NotFound();
            }

            return View(project_course);
        }

        // POST: project_course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project_course = await _context.project_course.SingleOrDefaultAsync(m => m.course_code == id);
            _context.project_course.Remove(project_course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool project_courseExists(string id)
        {
            return _context.project_course.Any(e => e.course_code == id);
        }
    }
}
