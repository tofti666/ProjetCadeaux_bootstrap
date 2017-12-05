using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCadeaux_Entites
{
    /// <summary>
    /// Correspond à la table en base "listeCadeaux_cadeaux"
    /// </summary>
    public class IdeeCadeauPourListe : IdeeCadeau
    {
        public int idIdeeCadeauPourListe { get; set; }
        public DateTime dateAjoutIdeeCadeau { get; set; }

        public IdeeCadeauPourListe()
        {
        }

        public IdeeCadeauPourListe(DateTime pDateAjoutIdeeCadeau)
        {
            this.dateAjoutIdeeCadeau = pDateAjoutIdeeCadeau;
        }

        #region fonctions

        public string intitule_cadeau
        {
            get
            {
                return this.cadeau.intitule_cadeau;
            }
            set
            {
               
                this.cadeau.intitule_cadeau = value;
                
            }
        }

        public string prix
        {
            get
            {
                return this.cadeau.prix;
            }
            set
            {

                this.cadeau.prix = value;

            }
        }

        public string description
        {
            get
            {
                return this.cadeau.description;
            }
            set
            {

                this.cadeau.description = value;

            }
        }

        public int id_cadeau
        {
            get
            {
                return this.cadeau.id_cadeau;
            }
            set
            {

                this.cadeau.id_cadeau = value;

            }
        }

        #endregion fonctions


    }
}
