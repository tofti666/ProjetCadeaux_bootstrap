using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class Personne
    {
        #region paramètres
        public int id_personne { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        #endregion

        #region Constructeurs
        public Personne()
        {
        }

        public Personne(int pIdPersonne, string pNom, string pPrenom, string pLogin, string pPassword, string pEmail)
        {
        }
        #endregion

        #region Fonctions propres à la classe
        public override string ToString()
        {
            return this.prenom + " " + this.nom.ToUpper();
        }
        #endregion

    }
}
