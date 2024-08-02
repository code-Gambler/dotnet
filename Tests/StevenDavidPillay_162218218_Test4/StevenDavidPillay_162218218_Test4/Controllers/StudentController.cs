using StevenDavidPillay_162218218_Test4.Data;
using StevenDavidPillay_162218218_Test4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StevenDavidPillay_162218218_Test4.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Add()
        {
            var model = new StudentViewModel
            {
                Name = "John Doe",
                Address = "123 Main St",
                Id = 1
            };

            return View(model);
        }
    }
}