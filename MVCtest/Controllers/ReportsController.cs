using MVCtest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCtest.Controllers
{
    public class ReportsController : Controller
    {
        ContosoUniversityEntities db = new ContosoUniversityEntities();

        StringBuilder sb = new StringBuilder();

        public ReportsController()
        {
            db.Database.Log = (msg) =>
            {
                sb.AppendLine(msg);
                sb.AppendLine("------------------------");
            };
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseReport1()
        {
            var data = (from c in db.Course
                       select new CourseReport1VM()
                       {
                           CourseID = c.CourseID,
                           CourseName = c.Title,
                           StudentCount = c.Enrollments.Count(),
                           TeacherCount = c.Teachers.Count(),
                           AvgGrade = c.Enrollments.Where(e =>e.Grade.HasValue).Average(e => e.Grade.Value)
                       }).ToList();

            ViewBag.SQL = sb.ToString();
            return View(data);
        }

        public ActionResult CourseReport2()
        {
            var data = db.Database.SqlQuery<CourseReport1VM>(@"
            SELECT
                Course.CourseID, 
                Course.Title AS CourseName,
	            (SELECT COUNT(CourseID) FROM CourseInstructor WHERE (CourseID = Course.CourseID)) AS TeacherCount,
	            (SELECT COUNT(CourseID) FROM Enrollment WHERE (Course.CourseID = Enrollment.CourseID)) AS StudentCount,
	            (SELECT AVG(Cast(Grade as Float)) FROM Enrollment WHERE (Course.CourseID = Enrollment.CourseID)) AS AvgGrade
            FROM   Course
            WHERE Course.CourseID=@CourseID
            ");
            return View("CourseReport1",data);
        }

        public ActionResult CourseReport3(int id)
        {
            var data = db.Database.SqlQuery<CourseReport1VM>(@"
            SELECT
                Course.CourseID, 
                Course.Title AS CourseName,
	            (SELECT COUNT(CourseID) FROM CourseInstructor WHERE (CourseID = Course.CourseID)) AS TeacherCount,
	            (SELECT COUNT(CourseID) FROM Enrollment WHERE (Course.CourseID = Enrollment.CourseID)) AS StudentCount,
	            (SELECT AVG(Cast(Grade as Float)) FROM Enrollment WHERE (Course.CourseID = Enrollment.CourseID)) AS AvgGrade
            FROM   Course
            WHERE Course.CourseID=@p0
            ",id);
            return View("CourseReport1", data);
        }

        public ActionResult CourseReport4(int id)
        {
            var data = db.GetCourseReport(id).First();

            ViewBag.SQL = sb.ToString();

            return View( data);
        }

        public ActionResult CourseReport5(int id)
        {
            var data = db.Database.SqlQuery<GetCourseReport_Result>("EXEC GetCourseReport @p0",id).ToList();

            ViewBag.SQL = sb.ToString();

            return View("CourseReport1", data);
        }


    }
}