﻿using System;
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
            ViewBag.x_status = ini_data.x_status;
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

        public IActionResult ViewInput()
        {
            prepareViewBag();
            return View(new course_type());
        }

        public IActionResult DetailsAsTable(string cgroup_code)
        {
            var ct = _context.course_type.Where(c => (c.cgroup_code == cgroup_code)).OrderBy(c => c.cgroup_code).ToList();
            return View(ct);
        }



        [HttpPost]
        public async Task<IActionResult> Create([Bind("ctype_code,cgroup_code,ctype_desc,id,rowversion,x_log,x_note,x_status")] course_type course_type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course_type);
                await _context.SaveChangesAsync();
            }
            return Json(new { result = "success" });
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return Json(new { result = "fail" });
            }

            var course_type = await _context.course_type.SingleOrDefaultAsync(m => m.id == new Guid(id));
            if (course_type == null)
            {
                return Json(new { result = "fail" });
            }
            return Json(new { result = "success", ctype_code = course_type.ctype_code, ctype_desc = course_type.ctype_desc, x_status = course_type.x_status, id = course_type.id, rowversion = course_type.rowversion });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("ctype_code,cgroup_code,ctype_desc,id,rowversion,x_log,x_note,x_status")] course_type course_type)
        {
            if (course_type.id != new Guid(id))
            {
                return Json(new { result = "fail" });
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
                        return Json(new { result = "fail" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { result = "success" });
            }
            return Json(new { result = "fail" });
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
