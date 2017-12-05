using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class ListeIdeesCadeaux
    {

        #region paramètres
        public int id_listeIdeesCadeaux { get; set; }
        public Personne listeIdeesCadeauxPour { get; set; }
        public DateTime dateCreationListe { get; set; }
        public Evenement listePourEvenement { get; set; }
        public Boolean isListeActive { get; set; }
        public List<IdeeCadeauPourListe> listeDeCadeaux { get; set; }
        #endregion paramètres


        #region constructeurs
        public ListeIdeesCadeaux(int pId_listeIdeesCadeaux, Personne pListeIdeesCadeauxPour, DateTime pDateCreationListe,
            Evenement pListePourEvenement, Boolean pIsListeActive, List<IdeeCadeauPourListe> pListeDeCadeaux)
        {
            this.id_listeIdeesCadeaux = pId_listeIdeesCadeaux;
            this.listeIdeesCadeauxPour = pListeIdeesCadeauxPour;
            this.dateCreationListe = pDateCreationListe;
            this.listePourEvenement = pListePourEvenement;
            this.isListeActive = pIsListeActive;
            this.listeDeCadeaux = pListeDeCadeaux;
        }

        public ListeIdeesCadeaux()
        {
        }
        #endregion constructeurs

    }
}
