using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;
using PPcore.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace PPcore.Controllers
{
    public class AccountController : Controller
    {
        private readonly PalangPanyaDBContext _context;
        private readonly IEmailSender _emailSender;

        public AccountController(PalangPanyaDBContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public void SendEmail(string email, string username, string password)
        {
            var title = "username and password";
            var body = "Username: " + username + "\nPassword: " + password;
            _emailSender.SendEmailAsync(email, title, body);
        }

        [HttpPost]
        public IActionResult Create(string birthdate, string cid_card, string email, string fname, string lname, string mobile)
        {
            DateTime bd = Convert.ToDateTime(birthdate);
            //birthdate = (bd.Year).ToString() + bd.Month.ToString() + bd.Day.ToString();
            birthdate = (bd.Year).ToString() + bd.ToString("MMdd");
            string password = cid_card.Substring(cid_card.Length - 4);
            try
            {
                _context.Database.ExecuteSqlCommand("INSERT INTO member (member_code,cid_card,birthdate,fname,lname,mobile,email,x_status,mem_password) VALUES ('" + cid_card + "','" + cid_card + "','" + birthdate + "',N'" + fname + "',N'" + lname + "','" + mobile + "','" + email + "','Y','" + password + "')");
                SendEmail(email, cid_card, password);
            }
            catch (SqlException ex)
            {
                var errno = ex.Number; var msg = "";
                if (errno == 2627) //Violation of primary key. Handle Exception
                {
                    msg = "duplicate";
                }
                return Json(new { result = "fail", error_code = errno, error_message = msg });
            }
            catch (Exception ex)
            {
                var errno = ex.HResult; var msg = "";
                if (ex.InnerException.Message.IndexOf("PRIMARY KEY") != -1)
                {
                    msg = "duplicate";
                }
                return Json(new { result = "fail", error_code = errno, error_message = msg });
            }

            return Json(new { result = "success" });
        }

        [HttpPost]
        public IActionResult Login(string uname, string upwd)
        {
            var mb = _context.member.FromSql("select cid_card from member where cid_card = '" + uname + "' and mem_password = '" + upwd + "' and x_status = 'Y'");
            if (mb.Count() == 0)
            {
                return Json(new { result = "fail" });
            }
            else
            {
                return Json(new { result = "success" });
            }
        }

        [HttpPost]
        public IActionResult ForgetPwd(string uname, string upwd)
        {
            member mb = _context.member.SingleOrDefault(m => (m.cid_card == uname) && (m.x_status == "Y"));
            if (mb != null)
            {
                SendEmail(mb.email, uname, mb.mem_password);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "fail" });
            }
        }
    }
}