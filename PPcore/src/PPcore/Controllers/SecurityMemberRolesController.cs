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
    public class SecurityMemberRolesController : Controller
    {
        private readonly SecurityDBContext _scontext;
        private readonly PalangPanyaDBContext _context;

        public SecurityMemberRolesController(SecurityDBContext scontext, PalangPanyaDBContext context)
        {
            _scontext = scontext;
            _context = context;
        }

        [HttpGet]
        public IActionResult DetailsAsTable(int utype)
        {
            Guid rid = Guid.Empty;
            var srs = _scontext.SecurityRoles.Where(sr => sr.x_status != "N").OrderBy(sr => sr.CreatedDate).ToList();
            var c = 0;
            foreach (SecurityRoles sero in srs)
            {
                c++;
                if (c == utype) { rid = sero.RoleId; }
            }

            var smrs = _scontext.SecurityMemberRoles.Where(smr => (smr.x_status != "N") && (smr.RoleId == rid)).ToList();
            foreach (SecurityMemberRoles seme in smrs)
            {

            }
            var cg = _context.course_group.OrderBy(m => m.cgroup_code);
            return View(cg.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("UserId,RoleId,CreatedBy,CreatedDate,EditedBy,EditedDate,x_log,x_note,x_status")] SecurityMemberRoles securityMemberRoles)
        {
            if (ModelState.IsValid)
            {
                securityMemberRoles.UserId = Guid.NewGuid();
                _scontext.Add(securityMemberRoles);
                await _scontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(securityMemberRoles);
        }









        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityMemberRoles = await _scontext.SecurityMemberRoles.SingleOrDefaultAsync(m => m.UserId == id);
            if (securityMemberRoles == null)
            {
                return NotFound();
            }
            return View(securityMemberRoles);
        }

        // POST: SecurityMemberRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,RoleId,CreatedBy,CreatedDate,EditedBy,EditedDate,x_log,x_note,x_status")] SecurityMemberRoles securityMemberRoles)
        {
            if (id != securityMemberRoles.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _scontext.Update(securityMemberRoles);
                    await _scontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityMemberRolesExists(securityMemberRoles.UserId))
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
            return View(securityMemberRoles);
        }

        // GET: SecurityMemberRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityMemberRoles = await _scontext.SecurityMemberRoles.SingleOrDefaultAsync(m => m.UserId == id);
            if (securityMemberRoles == null)
            {
                return NotFound();
            }

            return View(securityMemberRoles);
        }

        // POST: SecurityMemberRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityMemberRoles = await _scontext.SecurityMemberRoles.SingleOrDefaultAsync(m => m.UserId == id);
            _scontext.SecurityMemberRoles.Remove(securityMemberRoles);
            await _scontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SecurityMemberRolesExists(Guid id)
        {
            return _scontext.SecurityMemberRoles.Any(e => e.UserId == id);
        }
    }
}
