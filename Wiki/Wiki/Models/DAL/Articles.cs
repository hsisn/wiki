using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL
{
    public class Articles
    {
        // Auteurs:
        public int Add(Article a)
        {
            string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "AddArticle";                   // Stored procedures
                SqlCommand commande = new SqlCommand(requete, cnx);
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.Add(new SqlParameter("titre", a.Titre));
                commande.Parameters.Add(new SqlParameter("Contenu", a.Contenu));
                commande.Parameters.Add(new SqlParameter("IdContributeur", a.IdContributeur));
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = commande.ExecuteReader();
                    Article t = new Article();

                    while (dataReader.Read())
                    {
                        t.Titre = (string)dataReader["Titre"];
                        t.Contenu = (string)dataReader["Contenu"];
                        t.DateModification = (DateTime)dataReader["DateModification"];
                        t.IdContributeur = (int)dataReader["IdContributeur"];
                        t.Revision = (int)dataReader["Revision"];

                    }
                    dataReader.Close();

                    return 0;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }

        // Auteurs:
        public Article Find(string titre)
        {
            string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "ChercherArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("titre", titre));
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    Article t = new Article();

                    while (dataReader.Read())
                    {
                        t.Titre = (string)dataReader["Titre"];
                        t.Contenu = (string)dataReader["Contenu"];
                        t.DateModification = (DateTime)dataReader["DateModification"];
                        t.IdContributeur = (int)dataReader["IdContributeur"];
                        t.Revision = (int)dataReader["Revision"];

                    }
                    dataReader.Close();

                    return t;
                }
                finally
                {
                    cnx.Close();
                }
            }

        }


        // Auteurs: Vincent Simard, Phan Ngoc Long Denis, Floyd Ducharme, Pierre-Olivier Morin
        public IList<string> GetTitres()
        {
            string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "GetTitresArticles";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    IList<string> ListeTitre = new List<string>();
                    while (dataReader.Read())
                    {
                        string t = (string)dataReader["Titre"];
                        ListeTitre.Add(t);
                    }
                    dataReader.Close();

                    return ListeTitre;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }

        // Auteurs: Alexandre, Vincent, William et Nicolas
        public IList<Article> GetArticles()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCommand("GetArticles", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    var dataReader = cmd.ExecuteReader();
                    var articles = new List<Article>();

                    while (dataReader.Read())
                    {
                        var article = new Article();

                        article.Titre = (string)dataReader["Titre"];
                        article.Contenu = (string)dataReader["Contenu"];
                        article.Revision = (int)dataReader["Revision"];
                        article.IdContributeur = (int)dataReader["IdContributeur"];
                        article.DateModification = (DateTime)dataReader["DateModification"];

                        articles.Add(article);
                    }

                    return articles;
                }
                catch
                {
                    return null;
                }
            }
        }


        // Auteurs:
        public int Update(Article a)
        {


            string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "UpdateArticle";                   // Stored procedures
                SqlCommand commande = new SqlCommand(requete, cnx);
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.Add(new SqlParameter("titre", a.Titre));
                commande.Parameters.Add(new SqlParameter("Contenu", a.Contenu));
                commande.Parameters.Add(new SqlParameter("IdContributeur", a.IdContributeur));
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = commande.ExecuteReader();
                    Article t = new Article();

                    while (dataReader.Read())
                    {
                        t.Titre = (string)dataReader["Titre"];
                        t.Contenu = (string)dataReader["Contenu"];
                        t.DateModification = (DateTime)dataReader["DateModification"];
                        t.IdContributeur = (int)dataReader["IdContributeur"];
                        t.Revision = (int)dataReader["Revision"];

                    }
                    dataReader.Close();

                    return 0;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }



        // Auteurs:
        public int Delete(string titre)
        {
            string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "DeleteArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("titre", titre));
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    Article t = new Article();

                    while (dataReader.Read())
                    {
                        t.Titre = (string)dataReader["Titre"];
                        t.Contenu = (string)dataReader["Contenu"];
                        t.DateModification = (DateTime)dataReader["DateModification"];
                        t.IdContributeur = (int)dataReader["IdContributeur"];
                        t.Revision = (int)dataReader["Revision"];

                    }
                    dataReader.Close();

                    return 1;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }



        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}