using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.Models;
using c_final_capstone_v2.DAL;
using System.Configuration;

namespace c_final_capstone_v2.Controllers
{
    public class UserController : StaffController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CatStoneConnection"].ConnectionString;
        private IUserDao userDao;

        public UserController(IUserDao userDao)
        {
            this.userDao = new UserDao(connectionString);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (IsAuthenticated)
            {
                return RedirectToAction("CatList", "Home", new { username = CurrentUser});
            }
            LoginModel model = new LoginModel();
            return View("Login", model);
        }

        //[HttpPost]
        //public ActionResult Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Staff user = userDao.GetUser(model.Username)
        //    }
        //}

        [HttpPost]
        public ActionResult UserHome(LoginModel login)
        {
            
            //We need to have parameters where session is NOT NULL and checks and redirects to the right view
            //session variable that indicates a user is logged in and variable to determine if admin

            //Needs to check if null and build a staff member if session is null and they logged in
            Staff staff = userDao.Login(login.Username, login.Password);
            /*fork if returns null try log in again
             */

            //determine is staff object from DAO ahve a value or not
            //if staff obj exists 

            /*keep info of whos logged in and if theyre admin or not in two session variables
             * 
             * 
             */
            Session["User"] = staff.IsAdmin;
            //If session is null and user login is valid and not an admin
            if (!(bool)Session["User"])
            {
                return View("StaffView");
            }
            //Returns admin view if session is null and builds it
            else
            {
                return View("AdminView");
            }
            //We also need to check if Model state is vaild and if it is not then redirect to login with error message
        }
        public ActionResult NewStaffView()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");

            }
            else if ((bool)Session["User"])
            {
                return View();
            }
            else return RedirectToAction("StaffView");
        }

        [HttpPost]
        public ActionResult SubmitStaff(Staff newStaff)//TODO tmove to admin controller
        {
            //Fix Issue where refresh adds new staff entry over and over
            AdminDao admin = new AdminDao(connectionString);
            admin.AddStaff(newStaff);
            return View("AdminView");
        }
    }
}