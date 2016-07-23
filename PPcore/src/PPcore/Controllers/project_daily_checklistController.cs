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
    public class project_daily_checklistController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public project_daily_checklistController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        public IActionResult Index()
        {
            ViewBag.countRecords = _context.project_course.Where(m => m.x_status != "N").Count();
            return View();
        }

        [HttpGet]
        public IActionResult DetailsAsTableCourse()
        {
            var p = _context.project_course.Where(m => m.x_status != "N").OrderBy(m => m.course_code);
            return View(p.ToList());
        }

        public async Task<IActionResult> DetailsCourse(string id)
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
            ViewBag.ctype_code = project_course.ctype_code;
            return View(project_course);
        }

        [HttpGet]
        public IActionResult DetailsAsTableMember(string course_code)
        {
            var ps = _context.project_course_register.Where(pp => pp.course_code == course_code).OrderBy(pp => pp.member_code).ToList();
            List<PPcore.ViewModels.project_daily_checklist_member.project_daily_checklist_memberViewModel> pms = new List<PPcore.ViewModels.project_daily_checklist_member.project_daily_checklist_memberViewModel>();
            foreach (project_course_register p in ps)
            {
                var m = _context.member.SingleOrDefault(mm => mm.member_code == p.member_code);
                PPcore.ViewModels.project_daily_checklist_member.project_daily_checklist_memberViewModel pd = new PPcore.ViewModels.project_daily_checklist_member.project_daily_checklist_memberViewModel();
                pd.member = m;
                pd.attended = "Y";
                pd.course_day = 1;
                pms.Add(pd);
            }
            return View(pms);
        }

        // GET: project_daily_checklist/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_daily_checklist = await _context.project_daily_checklist.SingleOrDefaultAsync(m => m.course_date == id);
            if (project_daily_checklist == null)
            {
                return NotFound();
            }

            return View(project_daily_checklist);
        }

        // GET: project_daily_checklist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: project_daily_checklist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("course_date,course_code,member_code,id,x_log,x_note,x_status")] project_daily_checklist project_daily_checklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project_daily_checklist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project_daily_checklist);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string action, string id)
        {
            try
            {
                _context.Update(project_daily_checklist);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!project_daily_checklistExists(project_daily_checklist.course_date))
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

        // GET: project_daily_checklist/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_daily_checklist = await _context.project_daily_checklist.SingleOrDefaultAsync(m => m.course_date == id);
            if (project_daily_checklist == null)
            {
                return NotFound();
            }

            return View(project_daily_checklist);
        }

        // POST: project_daily_checklist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var project_daily_checklist = await _context.project_daily_checklist.SingleOrDefaultAsync(m => m.course_date == id);
            _context.project_daily_checklist.Remove(project_daily_checklist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool project_daily_checklistExists(DateTime id)
        {
            return _context.project_daily_checklist.Any(e => e.course_date == id);
        }
    }
}
