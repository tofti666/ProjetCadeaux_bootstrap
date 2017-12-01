using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class Commentaire
    {

         #region paramètres
        public int id_commentaire { get; set; }
        public int id_auteur { get; set; }
        public int id_personneliste { get; set; }
        public String commentaire { get; set; }
        public int id_evenement { get; set; }
        public DateTime date_creation { get; set; }
        public DateTime date_modification { get; set; }
        public Boolean deleted { get; set; }
        public String ecrit_par { get; set; }
        #endregion paramètres

        #region constructeurs
        public Commentaire()
        {
        }

        public Commentaire(int pIdCommentaire, int pIdAuteur, int pIdPersonneListe, String pCommentaire,
            int pIdEvenement, DateTime pDateCreation, DateTime pDateModification, Boolean pDeleted, String pEcrit_par)
        {
            this.id_commentaire = pIdCommentaire;
            this.id_auteur = pIdAuteur;
            this.id_personneliste = pIdPersonneListe;
            this.commentaire = pCommentaire;
            this.id_evenement = pIdEvenement;
            this.date_creation = pDateCreation;
            this.date_modification = pDateModification;
            this.deleted = pDeleted;
            this.ecrit_par = pEcrit_par;
        }
        #endregion constructeurs

    }
}
