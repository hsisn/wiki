using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Wiki.Models;

using Wiki.Models.Biz;
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
                public ActionResult Connexion(string username, string password, string
        ReturnUrl = "")
                /* Pour traiter la demande de login */
                {
         ViewBag.error = "";
         ViewBag.ReturnUrl = ReturnUrl;
         if (!Utilisateur.Authentifie(username, password))
         {
         ViewBag.error = "Nom d'utilisateur ou mot de passe invalide!";
         return View();
            }
         else
         {
         FormsAuthentication.SetAuthCookie(username, false);
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