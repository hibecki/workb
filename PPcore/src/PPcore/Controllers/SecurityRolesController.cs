using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPcore.Models;
using System.Data.SqlClient;
using PPcore.ViewModels.SecurityRoles;

namespace PPcore.Controllers
{
    public class SecurityRolesController : Controller
    {
        private readonly SecurityDBContext _scontext;
        private readonly PalangPanyaDBContext _context;

        public SecurityRolesController(PalangPanyaDBContext context, SecurityDBContext scontext)
        {
            _context = context;
            _scontext = scontext;    
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([Bind("RoleName")] SecurityRoles securityRoles)
        {
            if (ModelState.IsValid)
            {
                securityRoles.RoleId = Guid.NewGuid();
                securityRoles.CreatedBy = Guid.NewGuid();
                securityRoles.CreatedDate = DateTime.Now;
                securityRoles.EditedBy = Guid.NewGuid();
                securityRoles.x_status = "Y";
                securityRoles.RoleName = securityRoles.RoleName.Trim();

                _scontext.Add(securityRoles);
                try
                {
                    await _scontext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    //"Violation of UNIQUE KEY constraint 'UK_SecurityRoles'. Cannot insert duplicate key in object 'dbo.SecurityRoles'. The duplicate key value is (??????).\r\nThe statement has been terminated."
                    if (e.InnerException.Message.Contains("UNIQUE"))
                    {
                        return Json(new { result = "dup", RoleName = securityRoles.RoleName });
                    }
                    else
                    {
                        return Json(new { result = "fail" });
                    }
                }
                return Json(new { result = "success" });
            }
            return Json(new { result = "fail" });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRoleName(string roleId, string rolename)
        {
            var r = await _scontext.SecurityRoles.SingleOrDefaultAsync(rr => rr.RoleId == new Guid(roleId));
            if (r != null)
            {
                r.RoleName = rolename.Trim();
                _scontext.Update(r);
                await _scontext.SaveChangesAsync();
                return Json(new { result = "success", rolename = r.RoleName });
            }
            else
            {
                return Json(new { result = "fail" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var r = await _scontext.SecurityRoles.SingleOrDefaultAsync(rr => rr.RoleId == new Guid(roleId));
            if (r != null)
            {
                var memberCount = _context.member.Where(mm => mm.mem_role_id == r.RoleId).Count();
                if (memberCount != 0)
                {
                    return Json(new { result = "fail" });
                }
                else
                {
                    _scontext.Remove(r);
                    await _scontext.SaveChangesAsync();
                    return Json(new { result = "success" });
                }
            }
            else
            {
                return Json(new { result = "fail" });
            }
        }

        public async Task<IActionResult> DetailsAsBlock()
        {
            List<memberViewModel> ms = new List<memberViewModel>();
            var srs = await _scontext.SecurityRoles.Where(srr => srr.x_status != "N").OrderBy(srr => srr.CreatedDate).ToListAsync();
            foreach (SecurityRoles sr in srs)
            {
                memberViewModel m = new memberViewModel();
                m.SecurityRoles = sr;
                
                m.memberCount = _context.member.Where(mm => mm.mem_role_id == sr.RoleId).Count();
                ms.Add(m);
            }
            return View(ms);
        }

        public async Task<IActionResult> DetailsAsRoleMenu()
        {
            return View(await _scontext.SecurityRoles.Where(sr => sr.x_status != "N").OrderBy(sr => sr.CreatedDate).ToListAsync());
        }

        public async Task<IActionResult> Manage(string roleId)
        {
            var r = await _scontext.SecurityRoles.SingleOrDefaultAsync(rr => rr.RoleId == new Guid(roleId));
            if (r != null)
            {
                ViewBag.RoleName = r.RoleName;
                ViewBag.RoleId = r.RoleId.ToString();
                if (roleId != "c5a644a2-97b0-40e5-aa4d-e2afe4cdf428") //Not Administrators
                {
                    if (roleId != "9a1a4601-f5ee-4087-b97d-d69e7f9bfd7e") //Not Operators
                    {
                        if (roleId != "17822a90-1029-454a-b4c7-f631c9ca6c7d") //Not Members
                        {
                            ViewBag.Color = "panel-dashboard-yellow";
                        } else { ViewBag.Color = "panel-dashboard-green"; } //Members
                    } else { ViewBag.Color = "panel-primary"; } //Operators
                } else { ViewBag.Color = "panel-dashboard-black"; } //Administrators

                ViewBag.CountMembers = _context.member.Where(mm => mm.mem_role_id == r.RoleId).Count();

                string menuHtmlCB = ""; int leftgap = 30;
                var menus = _scontext.SecurityMenus.OrderBy(me => me.MenuId).ToList();
                foreach (SecurityMenus menu in menus)
                {
                    leftgap = menu.Level * 30;
                    menuHtmlCB += "<div id='menu-"+menu.MenuId+"' class='rolemanage-cb-uncheck' style='margin-left:"+leftgap+"px;' onclick='checkMenu("+menu.MenuId+")'><i id='menucb-"+menu.MenuId+"' class='fa fa-square-o' style='font-size:18px;'></i>&nbsp;&nbsp;" + menu.MenuName+"</div>";
                }
                ViewBag.RoleCheckBox = menuHtmlCB;
            }
            return View();
        }
    }
}
