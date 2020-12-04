using Assignment4_N01390572_A_KunalSailor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment4_N01390572_A_KunalSailor.Controllers
{
    public class TeacherController : Controller
    {

        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teacher = controller.ListTeachers(SearchKey);
            return View(Teacher);
        }

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string employeenumber)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(employeenumber);

            Teacher AddTeacher = new Teacher();
            AddTeacher.TeacherFname = TeacherFname;
            AddTeacher.TeacherLname = TeacherLname;
            AddTeacher.employeenumber = employeenumber;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(AddTeacher);

            return RedirectToAction("List");
        }

    }
}