﻿using MVCtest.Models;
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

        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Department.Add(department);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName");
            return View(department);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var item = db.Department.Find(id.Value);

            //ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName");
            var dept = db.Department.Find(id);
            ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName", dept.InstructorID);

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(int id,Department department)
        {
            if (ModelState.IsValid)
            {
                var item = db.Department.Find(id);

                item.Budget = department.Budget;
                item.Name = department.Name;
                item.StartDate = department.StartDate;
                item.InstructorID = department.InstructorID;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            //ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName");






            return View(department);
        }
        public ActionResult Details(int? id)
        {
            var data = db.Department.Find(id.Value);
            return View(data);
        }
        public ActionResult Delete(int? id)
        {
            var item = db.Department.Find(id.Value);
            return View(item);
        }
        [HttpPost]
        public ActionResult Delete(int id,Department department)
        {
            if (ModelState.IsValid)
            {
                department = db.Department.Find(id);
                return RedirectToAction("Index");
            }

            return View(department);
        }
    }
}