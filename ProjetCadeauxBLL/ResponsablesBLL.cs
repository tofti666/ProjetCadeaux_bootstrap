using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using ProjetCadeaux_Connection;

namespace ProjetCadeauxBLL
{
    public class ResponsablesBLL
    {
        ResponsablesDAL respoDAL = new ResponsablesDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personne"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public Personne getInfosResponsableDe(Personne personne, Evenement evt)
        {
            ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();

            ListeIdeesCadeaux listeRetour = listeBLL.getListeIdeesCadeaux(personne,evt);

            return respoDAL.getInfosResponsableDe(personne.id_personne, listeRetour.id_listeIdeesCadeaux);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responsable"></param>
        /// <param name="responsableDe"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public bool devenirResponsable(Personne responsable, Personne responsableDe, Evenement evt)
        {
            ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();

            ListeIdeesCadeaux listeRetour = listeBLL.getListeIdeesCadeaux(responsableDe, evt);

            return respoDAL.devenirResponsable(responsable.id_personne, responsableDe.id_personne, listeRetour.id_listeIdeesCadeaux);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personneConnectee"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public List<Personne> getListePersonnesSansResponsables(Personne personneConnectee, Evenement evt)
        {
            return respoDAL.getListePersonnesSansResponsables(personneConnectee.id_personne, evt.id_evenement);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personneConnectee"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public List<Personne> getListeInfosResponsabilite(Personne personneConnectee, Evenement evt)
        {
            return respoDAL.getListeInfosResponsabilite(personneConnectee.id_personne, evt.id_evenement);
        }

    }
}
