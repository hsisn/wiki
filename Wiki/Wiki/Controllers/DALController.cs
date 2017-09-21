using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.DAL;
using Wiki.Models.Biz;
using Wiki.Helpers;
namespace Wiki.Controllers
{
    public class DALController : Controller
    {

        //unchecked petit commentaire
        Articles repo = new Articles();
        public ActionResult Create()
        {
            return PartialView("Create");
        }
       
        // test 1
        [ValidateInput(false)]
        
        public ActionResult Index(String operation, Article a)
        {
            switch (operation)
            {
                case "Find":
                    if (a.Titre == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    a = repo.Find(a.Titre);
                    if (a == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Article = a;
                    break;
                   
                case "Update":
                    if (ModelState.IsValid && User.Identity.IsAuthenticated)
                    {
                        a.IdContributeur = 1;
                        repo.Update(a);
                    }
                    break;
                case "Add":
                    if (ModelState.IsValid && User.Identity.IsAuthenticated)
                    {
                        a.IdContributeur = 1;
                        repo.Add(a);
                    }
                    break;
                case "Delete":
                    repo.Delete(a.Titre);
                    break;
            }

            return View(repo.GetArticles());
        }


       
        public ActionResult Update()
        {
            return View();

        }
        [HttpPost]
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult Update(string Titre)
        //{
        //    return View(repo.Find(Titre));

        //}

        //[HttpPost]
        //public ActionResult Update(Article g)
        //{
        //    repo.Update(g);

        //    return RedirectToAction("Index", "Home");
        //}



        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Delete(Article g)
        //{
        //    repo.Delete(g);
        //    return RedirectToAction("Auteur");
        //}

    }
}
