using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Wiki.Models;


using Wiki.Models.Biz;
using Wiki.Helpers;

namespace Wiki.Controllers
{
    public class UtilisateursController : Controller
    {
        // GET: Utilisateurs
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Profil()
        {
            string nom = User.Identity.Name;
            return View(Utilisateur.getUtilisateurByName(nom));
        }
        [HttpPost]
        public ActionResult Profil(Utilisateur g)
        {
            Utilisateur.modifier(g);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
       public  ActionResult Inscription()
        {
                   return View();
        }

        [HttpPost]
        public ActionResult Inscription(Utilisateur g)
        {

            Utilisateur.creer(g);

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public ActionResult Connexion(string ReturnUrl = "")
        {
            ViewBag.error = "";
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }


                [HttpPost]
       public ActionResult Connexion(string login, string passwd, string
        ReturnUrl = "")
                /* Pour traiter la demande de login */
                {
         ViewBag.error = "";
         ViewBag.ReturnUrl = ReturnUrl;
         if (!Utilisateur.Authentifie(login, passwd))
         {
         ViewBag.error = Views.Shared.SiteResource.message_erreur;
         return View();
            }
         else
         {
         FormsAuthentication.SetAuthCookie(login, false);




                //**************************************************

                /*
                string culture = "fr";

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
                */
                //**************************************************







                if (ReturnUrl == "")
         {
         return RedirectToAction("Index", "Home");
        }
         else
         {
         return Redirect(ReturnUrl);
         }
         }
         }




        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ModifierMdP()
        {
            string nom = User.Identity.Name;
            return View(Utilisateur.getUtilisateurByName(nom));
        }
        [HttpPost]
        public ActionResult ModifierMdP(Utilisateur g)
        {
            Utilisateur.modifierpass(g);
            return RedirectToAction("Index", "Home");
        }




    }
}