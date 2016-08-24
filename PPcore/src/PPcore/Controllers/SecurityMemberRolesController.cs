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

        //[HttpPost]
        //public async Task<IActionResult> Suspend(string id, string status)
        //{
        //    var mr = _scontext.SecurityMemberRoles.SingleOrDefault(mrr => mrr.MemberId == new Guid(id));
        //    if (mr != null)
        //    {
        //        mr.x_status = status;
        //        mr.EditedDate = DateTime.Now;
        //        mr.EditedBy = new Guid("bab0a0a5-f0d4-4ba7-81d0-1c15fcd324c8"); //CHANGE THIS!!!!
        //        _scontext.Update(mr);
        //        await _scontext.SaveChangesAsync();
        //        return Json(new { result = "success" });
        //    }
        //    else
        //    {
        //        return Json(new { result = "fail" });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateUser([Bind("MemberId,RoleId,CreatedBy,CreatedDate,EditedBy,EditedDate,x_log,x_note,x_status")] SecurityMemberRoles securityMemberRoles)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        securityMemberRoles.MemberId = Guid.NewGuid();
        //        _scontext.Add(securityMemberRoles);
        //        await _scontext.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(securityMemberRoles);
        //}









        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var securityMemberRoles = await _scontext.SecurityMemberRoles.SingleOrDefaultAsync(m => m.MemberId == id);
        //    if (securityMemberRoles == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(securityMemberRoles);
        //}

        //// POST: SecurityMemberRoles/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("MemberId,RoleId,CreatedBy,CreatedDate,EditedBy,EditedDate,x_log,x_note,x_status")] SecurityMemberRoles securityMemberRoles)
        //{
        //    if (id != securityMemberRoles.MemberId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _scontext.Update(securityMemberRoles);
        //            await _scontext.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SecurityMemberRolesExists(securityMemberRoles.MemberId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(securityMemberRoles);
        //}

        //// GET: SecurityMemberRoles/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var securityMemberRoles = await _scontext.SecurityMemberRoles.SingleOrDefaultAsync(m => m.MemberId == id);
        //    if (securityMemberRoles == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(securityMemberRoles);
        //}

        //// POST: SecurityMemberRoles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var securityMemberRoles = await _scontext.SecurityMemberRoles.SingleOrDefaultAsync(m => m.MemberId == id);
        //    _scontext.SecurityMemberRoles.Remove(securityMemberRoles);
        //    await _scontext.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool SecurityMemberRolesExists(Guid id)
        //{
        //    return _scontext.SecurityMemberRoles.Any(e => e.MemberId == id);
        //}
    }
}
