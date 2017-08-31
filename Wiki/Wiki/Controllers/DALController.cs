using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

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
       
        //test master
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
                    if (ModelState.IsValid)
                    {
                        a.IdContributeur = 1;
                        repo.Update(a);
                    }
                    break;
                case "Add":
                    if (ModelState.IsValid)
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


        [HttpGet]
        public ActionResult Details(string Titre)
        {
                 return View(repo.Find(Titre));
        }









        [HttpGet]
        public ActionResult Update(string Titre)
        {
            return View(repo.Find(Titre));

        }


        [HttpPost]
        public ActionResult Update(Article g)
        {
            repo.Update(g);

            return RedirectToAction("Index", "Home");
        }



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
