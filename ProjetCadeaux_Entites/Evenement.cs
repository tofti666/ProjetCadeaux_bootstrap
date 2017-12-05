using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class Evenement
    {
        #region paramètres
        public int id_evenement { get; set;}
        public String libelle { get; set; }
        public DateTime dateEvenement { get; set;}
        public DateTime dateButoir{ get; set;}
        public int id_admin { get; set;}
        #endregion paramètres

         #region Constructeur
        public Evenement(int pid_evenement, String plibelle, DateTime pdateEvenement, DateTime pdateButoir, int pid_admin)
        {
            this.id_evenement = pid_evenement;
            this.libelle = plibelle;
            this.dateEvenement = pdateEvenement;
            this.dateButoir = pdateButoir;
            this.id_admin = pid_admin;
        }

        public Evenement() { }
        #endregion

        #region fonctions
        public String toString()
        {
            return "Evenement [ id = " + id_evenement
                + "; libelle = "+ libelle
            +"; dateEvenement = " + dateEvenement.ToShortDateString()
            + "; dateButoir = " + dateButoir.ToShortDateString()
            + "; id_admin = " + id_admin
            + "]";
        }
        #endregion fonctions
    }
}
