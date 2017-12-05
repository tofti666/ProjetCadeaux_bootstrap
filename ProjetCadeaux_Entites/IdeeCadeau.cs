using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    public class IdeeCadeau
    {

        #region paramètres
        public int id_ideeCadeau { get; set; }
        public Personne ideeCadeauPour { get; set; }
        public Cadeau cadeau { get; set; }
        public String priorite { get; set; }
        public Personne proposePar { get; set; }
        public List<Lien> listeDeLiens { get; set; }
        public List<Vote> listeDeVotes { get; set; }
        #endregion paramètres

        #region constructeurs
        public IdeeCadeau()
        {
            Personne pIdeeCadeauPour = new Personne();
            Cadeau pCadeau = new Cadeau();
            Personne pProposePar = new Personne();
            List<Lien> pListeDeLiens = new List<Lien>();
            List<Vote> pListeVotes = new List<Vote>();

            this.ideeCadeauPour = pIdeeCadeauPour;
            this.cadeau = pCadeau;
            this.proposePar = pProposePar;
            this.listeDeLiens = pListeDeLiens;
            this.listeDeVotes = pListeVotes;

        }

        public IdeeCadeau(int pId_ideeCadeau, Personne pIdeeCadeauPour, Cadeau pCadeau, String pPriorite, Personne pPropose_par)
        {
            this.id_ideeCadeau = pId_ideeCadeau;
            this.ideeCadeauPour = pIdeeCadeauPour;
            this.cadeau = pCadeau;
            this.priorite = pPriorite;
            this.proposePar = pPropose_par;
        }
        #endregion constructeurs

        #region méthodes
        public string intitule_cadeau(){ 
            return this.cadeau.intitule_cadeau;
        }
        public string description()
        {
            return this.cadeau.description;
        }
        public int id_cadeau()
        {
            return this.cadeau.id_cadeau;
        }
        public string prix()
        {
            return this.cadeau.prix;
        }

        #endregion

    }
}
