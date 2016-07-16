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
    public class projectsController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public projectsController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        private void prepareViewBag()
        {
            ViewBag.x_status = ini_data.x_status;
        }

        public IActionResult Index()
        {
            ViewBag.countRecords = _context.project.Count();
            return View();
        }

        [HttpGet]
        public IActionResult DetailsAsTable()
        {
            var p = _context.project.OrderBy(m => m.project_code);
            return View(p.ToList());
        }

        // GET: projects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.project.SingleOrDefaultAsync(m => m.project_code == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: projects/Create
        public IActionResult Create()
        {
            prepareViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("project_code,active_member_join,budget,id,passed_member,project_approve_date,project_date,project_desc,project_manager,ref_doc,target_member_join,x_log,x_note,x_status")] project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: projects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.project.SingleOrDefaultAsync(m => m.id == new Guid(id));
            if (project == null)
            {
                return NotFound();
            }
            prepareViewBag();
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("project_code,active_member_join,budget,id,passed_member,project_approve_date,project_date,project_desc,project_manager,ref_doc,target_member_join,x_log,x_note,x_status")] project project)
        {
            if (new Guid(id) != project.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!projectExists(project.project_code))
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
            return View(project);
        }

        // GET: projects/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.project.SingleOrDefaultAsync(m => m.project_code == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project = await _context.project.SingleOrDefaultAsync(m => m.project_code == id);
            _context.project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool projectExists(string id)
        {
            return _context.project.Any(e => e.project_code == id);
        }
    }
}
