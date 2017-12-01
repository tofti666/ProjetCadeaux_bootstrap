using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Connection;
using ProjetCadeaux_BLL;
using System.Data;

namespace ProjetCadeauxBLL
{
    public class MonCompteBLL
    {

        /// <summary>
        /// Méthode qui vérifie les bonnes règles concernant les mots de passe,
        /// et qui appelle la classe modifiant le mot de passe en base de données
        /// </summary>
        /// <param name="pIdPersonne"></param>
        /// <param name="pOldMotDePasse"></param>
        /// <param name="pNewMotDePasse1"></param>
        /// <param name="pNewMotDePasse2"></param>
        /// <returns>Un booléen qui dit si l'opération a fonctionné, ou une exception avec l'erreur rencontrée.</returns>
        public Boolean modifierMotDePasse(string pIdPersonne, string pOldMotDePasse, string pNewMotDePasse1, string pNewMotDePasse2)
        {
            //Vérifier que les mots de passes ne coïncident pas
            if (!pNewMotDePasse1.Equals(pNewMotDePasse2))
            {
                throw new Exception("Les deux mots de passe ne coïncident pas !");
            }
            //Vérifie que l'ancien mot de passe n'est pas vide
            else if (!StringUtils.estNonNullNiVide(pOldMotDePasse))
            {
                throw new Exception("L'ancien mot de passe est vide.");
            }
            else
            {
                Compte compteDao = new Compte();

                //Appel à la méthode qui modifie le mot de passe en base
                Boolean retour = compteDao.modifierMotDePasse(pIdPersonne, pNewMotDePasse1);

                return retour;
            }
        }       

        /// <summary>
        /// Modifie les informations personnelles de l'utilisateur,
        /// en vérifiant notamment que sa nouvelle adresse e-mail (si différente d'avant),
        /// n'est pas déjà utilisée
        /// </summary>
        /// <param name="pIdPersonne"></param>
        /// <param name="pNom"></param>
        /// <param name="pPrenom"></param>
        /// <param name="pEmail"></param>
        /// <param name="pOldEmail"></param>
        /// <returns></returns>
        public Boolean modifierInfos(string pIdPersonne, string pNom, string pPrenom, string pEmail, string pOldEmail)
        {
            Boolean retour;

            Compte compte = new Compte();

            //L'utilisateur a modifié son e-mail
            if (!pEmail.Equals(pOldEmail))
            {
                //On cherche l'ensemble des utilisateurs avec le nouvel e-mail
                DataTable dt = compte.getInfosPersonne(pEmail);

                //Il n'y a pas d'utilisateur avec le nouvel e-mail
                if (dt.Rows.Count == 0)
                {
                    //modifier les infos personnelles
                    retour = compte.modifierInformations(pIdPersonne, pNom, pPrenom, pEmail);

                    return retour;
                }
                //Il existe déjà des comptes avec cette adresse e-mail, on envoie une exception
                else
                {
                    throw new Exception("Un compte existe déjà avec cet e-mail. Veuillez renseigner une adresse valide.");
                }
            }
            else
            {
                //modifier les infos personnelles
                retour = compte.modifierInformations(pIdPersonne, pNom, pPrenom, pEmail);

                return retour;
            }
        }

    }
}
