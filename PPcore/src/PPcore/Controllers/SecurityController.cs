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
        public IActionResult ManageMembers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageRoles()
        {
            return View();
        }
    }
}