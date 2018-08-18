using c_final_capstone_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;
using System.Configuration;

namespace c_final_capstone_v2.Controllers
{
    public class HomeController : StaffController
    {
        ICatSqlDao catDao;
        ISkillDao skillDao;
        string connectionString = ConfigurationManager.ConnectionStrings["CatStoneConnection"].ConnectionString;

        public HomeController()
        {
            catDao = new CatSqlDao(connectionString);
            skillDao = new SkillDao(connectionString);
        }

        public ActionResult Index()
        {
            Cat featuredCat = catDao.GetFeaturedCat();

            return View("Index", featuredCat);
        }

        public ActionResult ViewCat(int id)
        {
            Cat theCat = catDao.ViewCat(id);

            return View("ViewCat", theCat);
        }

        public ActionResult AddCat()//TODO move to admin controller
        {
            return View();
        }

        public ActionResult SubmitCat(Cat newCat)//TODO tmove to admin controller
        {
            catDao.AddCat(newCat);

            return RedirectToAction("CatList");/// Would love to return to viewcat for the added cat.
        }

        public ActionResult AddPhoto(Cat selectedCat)//put this is catDetails??
        {
            catDao.AddPhoto(selectedCat);

            return RedirectToAction("");//do not want to redirect need to hold onto the catid
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CatDetails()//FIX what is different about this versus Action Result ViewCat(int id) Above?
        {
            return View("ViewCat");//not created view yet. for sepceific cat. is viewcat now
        }

        public ActionResult CareTips()
        {
            return View("CareTips");
        }

        public ActionResult CatList()
        {
            List<Cat> cats = catDao.GetAllCats();
            return View(cats);
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Us()// cause we're adorable
        {
            return View("Us");
        }

        //TODO change skill 'Comendeering' to 'Commandeering'


        public ActionResult TempImage()//TODO destroy tempImage. most likely won't need
        {
            return View("TempImage");
        }

      
    }
}
