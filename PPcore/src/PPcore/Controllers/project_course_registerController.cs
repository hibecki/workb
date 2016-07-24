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
    public class project_course_registerController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public project_course_registerController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        private void prepareViewBag()
        {
            ViewBag.cgroup_code = new SelectList(_context.course_group.OrderBy(cg => cg.cgroup_code), "cgroup_code", "cgroup_desc", 1);
        }

        public IActionResult Index()
        {
            prepareViewBag();
            ViewBag.countRecords = _context.project_course.Count();
            return View();
        }

        [HttpGet]
        public IActionResult DetailsAsTableCourse(string cgroup_code, string ctype_code)
        {
            //var p = _context.course.OrderBy(m => m.course_code);
            var p = _context.project_course.Where(m => (m.cgroup_code == cgroup_code && m.ctype_code == ctype_code)).OrderBy(m => m.course_code);
            return View(p.ToList());
        }

        [HttpGet]
        public IActionResult DetailsAsTableMember(string course_code)
        {
            var ps = _context.project_course_register.Where(pp => pp.course_code == course_code).OrderBy(pp => pp.member_code).ToList();

            List<PPcore.ViewModels.project_course_register_member.project_course_register_memberViewModel> rs = new List<PPcore.ViewModels.project_course_register_member.project_course_register_memberViewModel>();
            foreach (project_course_register p in ps)
            {
                var m = _context.member.SingleOrDefault(mm => mm.member_code == p.member_code);
                var r = new PPcore.ViewModels.project_course_register_member.project_course_register_memberViewModel();
                r.member = m;
                r.course_grade = p.course_grade;
                rs.Add(r);
            }
            return View(rs);
        }

        [HttpGet]
        public IActionResult DetailsCourse(string id)
        {
            var c = _context.project_course.SingleOrDefault(m => m.id == new Guid(id));
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Register(string cid, string courseId)
        {
            project_course c = _context.project_course.SingleOrDefault(cc => cc.id == new Guid(courseId));
            member m = _context.member.SingleOrDefault(mm => mm.cid_card == cid);
            try
            {
                project_course_register pcr = new project_course_register();
                pcr.course_code = c.course_code;
                pcr.member_code = m.member_code;
                pcr.x_status = "Y";
                _context.Add(pcr);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Json(new { result = "fail", cid = cid });
            }
            return Json(new { result = "success", cid = cid });
        }

        [HttpPost]
        public async Task<IActionResult> EditGrade(string member_code, string course_code, int grade)
        {
            project_course_register r = _context.project_course_register.SingleOrDefault(rr => (rr.member_code == member_code) && (rr.course_code == course_code));
            r.course_grade = grade;
            _context.project_course_register.Update(r);
            await _context.SaveChangesAsync();
            return Json(new { result = "success" });
        }
    }
}
