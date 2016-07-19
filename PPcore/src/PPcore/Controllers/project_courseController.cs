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

        //private void prepareViewBag()
        //{
        //    ViewBag.x_status = ini_data.x_status;
        //    ViewBag.cgroup_code = new SelectList(_context.course_group.OrderBy(cg => cg.cgroup_code), "cgroup_code", "cgroup_desc",1);
        //}

        [HttpGet]
        public IActionResult Index(string id)
        {
            var pj = _context.project.SingleOrDefault(p => p.id == new Guid(id));
            ViewBag.projectId = pj.id;
            ViewBag.projectCode = pj.project_code;
            ViewBag.countRecords = _context.project_course.Count();
            return View(new project_course());
        }

        public IActionResult DetailsAsTable(string id, string code)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project_course = _context.project_course.Where(m => m.project_code == code);
            if (project_course == null)
            {
                return NotFound();
            }

            return View(project_course.ToList());
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

        [HttpPost]
        public async Task<IActionResult> Create(string courseId, string projectCode)
        {
            var c = _context.course.SingleOrDefault(m => m.id == new Guid(courseId));
            project_course pc = new project_course();
            pc.project_code = projectCode;
            pc.active_member_join = c.active_member_join;
            pc.budget = c.budget;
            pc.cgroup_code = c.cgroup_code;
            pc.charge_head = c.charge_head;
            pc.course_approve_date = c.course_approve_date;
            pc.course_begin = c.course_begin;
            pc.course_code = c.course_code;
            pc.course_date = c.course_date;
            pc.course_desc = c.course_desc;
            pc.course_end = c.course_end;
            pc.ctype_code = c.ctype_code;
            pc.passed_member = c.passed_member;
            pc.project_manager = c.project_manager;
            pc.ref_doc = c.ref_doc;
            pc.support_head = c.support_head;
            pc.target_member_join = c.target_member_join;
            pc.x_status = c.x_status;

            try
            {
                _context.Add(pc);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Json(new { result = "fail" });
            }
            return Json(new { result = "success" });
        }

        public IActionResult EditAsTable()
        {
            var p = _context.course.OrderBy(m => m.course_code).ToList();
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
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

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var pc = await _context.project_course.SingleOrDefaultAsync(m => m.id == new Guid(id));
            _context.project_course.Remove(pc);
            await _context.SaveChangesAsync();
            return Json(new { result = "success" });
        }
    }
}
