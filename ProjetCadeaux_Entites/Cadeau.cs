using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class Cadeau
    {

        #region paramètres
        public String intitule_cadeau { get; set; }
        public String description { get; set; }
        public String prix { get; set; }
        public int id_cadeau { get; set; }
        #endregion paramètres

        #region constructeurs
        public Cadeau()
        {
        }

        public Cadeau(int pId_cadeau, String pIntitule_cadeau, String pDescription, String pPrix)
        {
            this.id_cadeau = pId_cadeau;
            this.intitule_cadeau = pIntitule_cadeau;
            this.description = pDescription;
            this.prix = pPrix;
        }
        #endregion constructeurs

    }
}
