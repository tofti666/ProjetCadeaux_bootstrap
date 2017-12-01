using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using ProjetCadeaux_Connection;

namespace ProjetCadeauxBLL
{
    public class PersonnesBLL
    {

        /// <summary>
        /// Retourne une liste de Personnes à partir d'un nom et d'un prénom
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pPrenom"></param>
        /// <returns></returns>
        public List<Personne> recherchePersonne(string pNom, string pPrenom)
        {
            Compte cpt = new Compte();

            return cpt.rechercherPersonnes(pNom, pPrenom);
        }

    }
}
