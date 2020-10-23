using Microsoft.Ajax.Utilities;
using MVCtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCtest.Controllers
{
    public class TestController : Controller
    {
        static List<Person> data = new List<Person>()
            {
                new Person() { Id = 1, Name = "Will", Age = 18 },
                new Person() { Id = 2, Name = "Tom", Age = 28 },
                new Person() { Id = 3, Name = "Mary", Age = 38 },
                new Person() { Id = 4, Name = "John", Age = 48 },
            };

        // GET: Test
        public ActionResult Index()
        {


            return View(data);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                data.Add(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public ActionResult Edit(int id)
        {
            return View(data.FirstOrDefault(p => p.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Person person)
        {
            if (ModelState.IsValid)
            {
                var one = data.FirstOrDefault(p =>p.Id == id);
                one.Name = person.Name;
                one.Age = person.Age;
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public ActionResult Delete(int id)
        {
            data.FirstOrDefault(p =>p.Id == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,FormCollection from)
        {
            if (ModelState.IsValid)
            {
                data.Remove(data.First(p =>p.Id == id));
                return RedirectToAction("Index");
            }

            return View(id);
        }

        public ActionResult Details(int id)
        {

            return View(data.FirstOrDefault(p =>p.Id == id));
        }
    }
}