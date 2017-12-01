using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux_Connection
{
    public class Compte
    {

        ConnectionBase conn = new ConnectionBase();

        /// <summary>
        /// Crée un nouveau compte en base de données.
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="email"></param>
        /// <param name="login"></param>
        /// <param name="motdepasse"></param>
        public void creerCompte(string nom, string prenom, string email, string login, string motdepasse)
        {
            String sql = "INSERT INTO personnes (nom, prenom, email, login, password) VALUES "
                + "('" + nom.Replace("'", "''") + "','" + prenom.Replace("'", "''") + "','" 
                + email.Replace("'", "''") + "','" + login.Replace("'", "''") + "','" 
                + motdepasse.Replace("'", "''") + "'); commit;";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Récupère le mot de passe d'un utilisateur en fonction de son login.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Boolean getPassword(string login, string password)
        {
            String sql = "SELECT password FROM personnes "
                +"WHERE login = '" + login.Replace("'", "''") + "';";

            try
            {
                DataTable dt = conn.getConnection(sql);

                String retour = dt.Rows[0].ItemArray.GetValue(0).ToString();

                return password.Equals(retour);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Retourne l'ensemble des données d'un utilisateur en fonction de son login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public DataTable getInformationsPersonne(string login)
        {
            String sql = "SELECT * FROM personnes "
                +"WHERE login = '" + login.Replace("'", "''") + "';";

            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retourne les informations d'un utilisateur en fonction de son id.
        /// </summary>
        /// <param name="personneID"></param>
        /// <returns></returns>
        public DataTable getPersonnesInscrites(string personneID)
        {
            String sql = "SELECT id_personne, nom, prenom FROM personnes "
                +"WHERE id_personne != '" + personneID + "';";

            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retourne l'ensemble des résultats d'un utilisateur en fonction de son adresse e-mail.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataTable getInfosPersonne(string email)
        {
            String sql = "SELECT nom, prenom, login FROM personnes "
                +"WHERE email = '" + email.Replace("'", "''") + "';";

            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Réinitialise le mot de passe de l'utilisateur lorsqu'il saisit son e-mail
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public String reinitialiserMotDePasse(string Email)
        {
            String motDePasse = ProjetCadeaux_Utils.RandomString.genererString();

            string hashMd5Mdp = ProjetCadeaux_Utils.HashMd5.getMd5Hash(motDePasse);

            String sql = "UPDATE \"personnes\" SET password = '" + hashMd5Mdp.Replace("'", "''") 
                + "' WHERE email = '" + Email.Replace("'", "''") + "';";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }

            return motDePasse;
        }

        /// <summary>
        /// Modifie les informations personnelles de l'utilisateur en fonction de son numéro d'Id
        /// </summary>
        /// <param name="idPersonne">id de l'utilisateur à qui on modifie les informations personnelles</param>
        /// <param name="pNom">Nouveau nom de l'utilisateur</param>
        /// <param name="pPrenom">Nouveau prénom de l'utilisateur</param>
        /// <param name="pEmail">Nouvel e-mail de l'utilisateur</param>
        /// <returns>Booléen représentant si l'opération a réussi</returns>
        public Boolean modifierInformations(string idPersonne, string pNom, string pPrenom,
            string pEmail)
        {
            
            String sql = "UPDATE \"personnes\" SET nom= '"+ pNom + "', prenom ='" + pPrenom + "', email = '"+ pEmail
                    + "' WHERE id_personne = " + idPersonne  + "; commit;";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Méthode modifiant le mot de passe de l'utilisateur en fonction de son numéro d'id.
        /// </summary>
        /// <param name="idPersonne">Id de l'utilisateur à qui on modifie le mot de passe</param>
        /// <param name="pNewMotDePasse">Nouveau mot de passe de l'utilisateur</param>
        /// <returns>Un booléen qui indique si l'opération a réussi</returns>
        public Boolean modifierMotDePasse(string idPersonne, string pNewMotDePasse)
        {

            string hashMd5Mdp = ProjetCadeaux_Utils.HashMd5.getMd5Hash(pNewMotDePasse);

            String sql = "UPDATE \"personnes\" SET password = '" + hashMd5Mdp.Replace("'", "''")
                    + "' WHERE id_personne = " + idPersonne + "; commit;";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lance la requête qui récupère la liste des personnes en fonction d'un nom et d'une prénom
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <returns></returns>
        public List<Personne> rechercherPersonnes(string nom, string prenom)
        {
            String sql = "SELECT id_personne, nom, prenom, email FROM personnes "
                + "WHERE nom like '%" + nom.Replace("'", "''") + "%' AND prenom like '%"+ prenom.Replace("'","''") +"%';";

            try
            {
                DataTable dt = conn.getConnection(sql);
                List<Personne> listePersonne = new List<Personne>();

                //Construit la liste des participants
                foreach (DataRow row in dt.Rows)
                {
                    Personne personne = new Personne();
                    personne.id_personne = (int.Parse(row.ItemArray.GetValue(0).ToString()));
                    personne.nom = row.ItemArray.GetValue(1).ToString();
                    personne.prenom = row.ItemArray.GetValue(2).ToString();
                    personne.email = row.ItemArray.GetValue(3).ToString();
                    
                    listePersonne.Add(personne);
                }

                return listePersonne;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
