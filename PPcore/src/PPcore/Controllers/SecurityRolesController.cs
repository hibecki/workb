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
    public class SecurityRolesController : Controller
    {
        private readonly SecurityDBContext _context;

        public SecurityRolesController(SecurityDBContext context)
        {
            _context = context;    
        }

        public async Task<IActionResult> DetailsAsTable()
        {
            return View(await _context.SecurityRoles.ToListAsync());
        }

        // GET: SecurityRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.SecurityRoles.ToListAsync());
        }

        // GET: SecurityRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityRoles = await _context.SecurityRoles.SingleOrDefaultAsync(m => m.RoleId == id);
            if (securityRoles == null)
            {
                return NotFound();
            }

            return View(securityRoles);
        }

        // GET: SecurityRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SecurityRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,CreatedBy,CreatedDate,EditedBy,EditedDate,RoleName,x_log,x_note,x_status")] SecurityRoles securityRoles)
        {
            if (ModelState.IsValid)
            {
                securityRoles.RoleId = Guid.NewGuid();
                _context.Add(securityRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(securityRoles);
        }

        // GET: SecurityRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityRoles = await _context.SecurityRoles.SingleOrDefaultAsync(m => m.RoleId == id);
            if (securityRoles == null)
            {
                return NotFound();
            }
            return View(securityRoles);
        }

        // POST: SecurityRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoleId,CreatedBy,CreatedDate,EditedBy,EditedDate,RoleName,x_log,x_note,x_status")] SecurityRoles securityRoles)
        {
            if (id != securityRoles.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityRolesExists(securityRoles.RoleId))
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
            return View(securityRoles);
        }

        // GET: SecurityRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityRoles = await _context.SecurityRoles.SingleOrDefaultAsync(m => m.RoleId == id);
            if (securityRoles == null)
            {
                return NotFound();
            }

            return View(securityRoles);
        }

        // POST: SecurityRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityRoles = await _context.SecurityRoles.SingleOrDefaultAsync(m => m.RoleId == id);
            _context.SecurityRoles.Remove(securityRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SecurityRolesExists(Guid id)
        {
            return _context.SecurityRoles.Any(e => e.RoleId == id);
        }
    }
}
