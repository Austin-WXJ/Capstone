using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;

namespace c_final_capstone_v2.Controllers
{
    public class StaffController : Controller
    {
        private const string userNameKey = "Staff_Username";
        private const string isAdminKey = "Is_Admin";
        private readonly IUserDao userDao;

        //public StaffController(IUserDao userDao)
        //{
        //    this.userDao = userDao;
        //}

        public string CurrentUser
        {
            get
            {
                string username = string.Empty;

                //Check to see if user session exists, if not create it
                if (Session[userNameKey] != null)
                {
                    username = (string)Session[userNameKey];
                }

                return username;
            }
        }
        public bool IsAuthenticated
        {
            get
            {
                return Session[userNameKey] != null;
            }
        }
        public void LogUserIn(string username, bool isAdmin)
        {
            Session[userNameKey] = username;
            if (isAdmin)
            {
                Session[isAdminKey] = isAdmin;
            }
        }

    }
}