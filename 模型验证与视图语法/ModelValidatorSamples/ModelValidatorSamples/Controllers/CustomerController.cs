using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelValidatorSamples.Models;

namespace ModelValidatorSamples.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return Content("OK");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        [AcceptVerbs("Get","Post")]
        public IActionResult VerifyEmail(string email)
        {
            string[] emails = new string[] { "admin@xcode.me", "abc@xcode.me" };

            if (emails.Contains(email))
            {
                return Json("该电子邮件已经存在");
            }

            return Json(true);
        }
    }
}