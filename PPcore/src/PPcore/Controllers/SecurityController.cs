using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;
using PPcore.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using PPcore.Helpers;

namespace PPcore.Controllers
{
    public class SecurityController : Controller
    {
        private readonly PalangPanyaDBContext _context;
        private readonly SecurityDBContext _scontext;
        private readonly IEmailSender _emailSender;
        private IConfiguration _configuration;
        private IHostingEnvironment _env;
        private readonly ILogger _logger;

        public SecurityController(PalangPanyaDBContext context,SecurityDBContext scontext, IEmailSender emailSender, IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _context = context;
            _scontext = scontext;
            _emailSender = emailSender;
            _configuration = configuration;
            _env = env;
            _logger = loggerFactory.CreateLogger<SecurityController>();
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            var memberId = HttpContext.Session.GetString("memberId");
            if (memberId != null)
            {
                var smr = _scontext.SecurityMemberRoles.SingleOrDefault(smrr => smrr.MemberId == new Guid(memberId));
                smr.LoggedOutDate = DateTime.Now;
                _scontext.Update(smr);
                await _scontext.SaveChangesAsync();

                HttpContext.Session.Clear();

            }

            _logger.LogInformation(4, "User: " + memberId + " logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult ManageMembers(string roleId)
        {
            if (roleId != null && roleId != "")
            {
                ViewBag.RoleId = roleId;
            }
            else
            {
                ViewBag.RoleId = "";
            }
            return View();
        }

        [HttpGet]
        public IActionResult ManageRoles()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Settings()
        {
            var memberId = HttpContext.Session.GetString("memberId");
            var roleId = HttpContext.Session.GetString("roleId");
            ViewBag.UserName = HttpContext.Session.GetString("displayname");

            if (roleId != "c5a644a2-97b0-40e5-aa4d-e2afe4cdf428") //Not Administrators
            {
                if (roleId != "9a1a4601-f5ee-4087-b97d-d69e7f9bfd7e") //Not Operators
                {
                    if (roleId != "17822a90-1029-454a-b4c7-f631c9ca6c7d") //Not Members
                    {
                        ViewBag.Color = "panel-dashboard-yellow";
                    }
                    else { ViewBag.Color = "panel-dashboard-green"; } //Members
                }
                else { ViewBag.Color = "panel-primary"; } //Operators
            }
            else { ViewBag.Color = "panel-dashboard-black"; } //Administrators

            var m = _context.member.SingleOrDefault(mm => mm.id == new Guid(memberId));
            ViewBag.LoginName = m.mem_username;

            return View();
        }
    }
}