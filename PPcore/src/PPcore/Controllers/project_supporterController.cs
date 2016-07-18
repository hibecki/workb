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
    public class project_supporterController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public project_supporterController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        public IActionResult Index(string id)
        {
            var pj = _context.project.SingleOrDefault(p => p.id == new Guid(id));
            ViewBag.projectId = pj.id;
            ViewBag.projectCode = pj.project_code;
            ViewBag.countRecords = _context.project_supporter.Count();
            return View();
        }

        // GET: project_supporter/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_supporter = await _context.project_supporter.SingleOrDefaultAsync(m => m.spon_code == id);
            if (project_supporter == null)
            {
                return NotFound();
            }

            return View(project_supporter);
        }

        public IActionResult DetailsAsTable(string id, string code)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project_supporter = _context.project_supporter.Where(m => m.project_code == code);
            if (project_supporter == null)
            {
                return NotFound();
            }

            return View(project_supporter.ToList());
        }

        // GET: project_supporter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: project_supporter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("spon_code,project_code,contactor,contactor_detail,id,ref_doc,support_budget,x_log,x_note,x_status")] project_supporter project_supporter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project_supporter);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project_supporter);
        }

        public IActionResult EditAsTable()
        {
            var p = _context.project_sponsor.OrderBy(ps => ps.spon_code).ToList();
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("spon_code,project_code,contactor,contactor_detail,id,ref_doc,support_budget,x_log,x_note,x_status")] project_supporter project_supporter)
        {
            if (id != project_supporter.spon_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project_supporter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!project_supporterExists(project_supporter.spon_code))
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
            return View(project_supporter);
        }

        // GET: project_supporter/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project_supporter = await _context.project_supporter.SingleOrDefaultAsync(m => m.spon_code == id);
            if (project_supporter == null)
            {
                return NotFound();
            }

            return View(project_supporter);
        }

        // POST: project_supporter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project_supporter = await _context.project_supporter.SingleOrDefaultAsync(m => m.spon_code == id);
            _context.project_supporter.Remove(project_supporter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool project_supporterExists(string id)
        {
            return _context.project_supporter.Any(e => e.spon_code == id);
        }
    }
}
