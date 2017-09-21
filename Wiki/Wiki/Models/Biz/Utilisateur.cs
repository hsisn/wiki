using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Wiki.Views.Shared;
namespace Wiki.Models.Biz
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(SiteResource), Name = "LastName")]
        public string Nom { get; set; }
        [Required]
        [Display(ResourceType = typeof(SiteResource), Name = "FirstName")]
        public string Prenom { get; set; }

        [Required]
        [Display(ResourceType = typeof(SiteResource), Name = "Language")]
        public string langue { get; set; }



        [Display(ResourceType = typeof(SiteResource), Name = "EmailAddress")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "doit etre un email valid")]
        [Required]
        public string Courriel { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(SiteResource), Name = "Password")]
        [StringLength(255, ErrorMessage = "doit etre  entre 6 et  255 caracters", MinimumLength = 6)]
        [Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "doit etre  entre 6 et  255 caracters", MinimumLength = 6)]
        [Display(ResourceType = typeof(SiteResource), Name = "ConfirmationofPassword")]
        [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation ne orrespondent pas.")]
        public string PasswordRepete { get; set; }

        private static string hahsString;

        public static bool creer(Utilisateur u)
        {
            bool TEST = true;
            byte[] hashPassword = new UTF8Encoding().GetBytes(u.Password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);
            string chConnexion = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            string requete = "INSERT INTO Utilisateurs ( Courriel,MDP,NomFamille,Prenom,Langue) VALUES ('" + u.Courriel + "', '" + hashString + "', '" + u.Nom + "', '"+u.Prenom+ "', '"+u.langue+ "')";
            SqlConnection connexion = new SqlConnection(chConnexion);
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try
            {
                connexion.Open();
                commande.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string msg = e.Message;
                TEST = false;
            }
            finally
            {
                connexion.Close();
            }
            return TEST;
        }


        public static bool Authentifie(string login, string passwd)
        {
            string cStr =   ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "SELECT * FROM Utilisateurs WHERE Courriel = '" +
               login + "'";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        dataReader.Close();
                       
                     return false;
                    }
                    dataReader.Read();
                    var encodedPasswordOnServer = (string)dataReader["MDP"];
                    byte[] encodedPassword = new UTF8Encoding().GetBytes(passwd);
                    byte[] hash =
                   ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                    string encodedPasswordSentToForm =
                   BitConverter.ToString(hash);
                    dataReader.Close();
                    return encodedPasswordSentToForm ==
                    encodedPasswordOnServer.Trim();
                }
                finally
                {
                    cnx.Close();
                }
            }
        }
    

        ///////////

        public static bool modifier(Utilisateur g)
        {
            
            string chConnexion = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            string requete = "UPDATE Utilisateurs SET  NomFamille='" + g.Nom + "', Prenom='" + g.Prenom + "', Langue='"+g.langue+ "' WHERE Id=" + g.Id;
            SqlConnection connexion = new SqlConnection(chConnexion);
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            bool TEST = true;

            try
            {
                connexion.Open();
                commande.ExecuteNonQuery();
                return TEST;
            }
            catch (Exception e)
            {
                string Msg = e.Message;
                TEST = false;
            }
            finally
            {
                connexion.Close();
            }
            return TEST;
        }
        ///
        public static bool modifierpass(Utilisateur g)
        {
            byte[] hashPassword = new UTF8Encoding().GetBytes(g.Password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);
            string chConnexion = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            string requete = "UPDATE Utilisateurs SET MDP='" + hashString + "' WHERE Id=" + g.Id;
            SqlConnection connexion = new SqlConnection(chConnexion);
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            bool TEST = true;

            try
            {
                connexion.Open();
                commande.ExecuteNonQuery();
                return TEST;
            }
            catch (Exception e)
            {
                string Msg = e.Message;
                TEST = false;
            }
            finally
            {
                connexion.Close();
            }
            return TEST;
        }

        ///////////////
        public static Utilisateur getUtilisateurByName(string name)
        {
            string chConnexion = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            string requete = "SELECT * FROM Utilisateurs WHERE Courriel='" + name + "'";
            SqlConnection connexion = new SqlConnection(chConnexion);
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            Utilisateur monUtilisateur = new Utilisateur();

            try
            {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                dr.Read();
                monUtilisateur = new Utilisateur
                {
                    Id = (int)dr["Id"],                    
                    Courriel = name,
                    Password = (string)dr["MDP"],
                    Nom = (string)dr["NomFamille"],
                    Prenom = (string)dr["Prenom"],
                    langue = (string)dr["Langue"],
                    //PasswordRepete = (string)dr["Password"]
                };
                dr.Close();

                return monUtilisateur;
            }
            catch (Exception e)
            {
                string Msg = e.Message;
            }
            finally
            {
                connexion.Close();
            }
            return monUtilisateur;
        }

}
}