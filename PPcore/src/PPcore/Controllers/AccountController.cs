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
                _emailSender.SendEmailAsync(email, "username and password", "Username: " + cid_card + "\nPassword: " + password);
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
    }
}