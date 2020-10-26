using MVCtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCtest.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Departments
        ContosoUniversityEntities db = new ContosoUniversityEntities();
        public ActionResult Index()
        {
            return View(db.Department);
        }
    }
}