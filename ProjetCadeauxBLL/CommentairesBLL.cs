using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Connection;
using ProjetCadeaux_Entites;
using System.Data;
using ProjetCadeaux_BLL;

namespace ProjetCadeauxBLL
{
    public class CommentairesBLL
    {

        /// <summary>
        /// Récupère la liste des commentaires sur la liste d'une personne pour un événement donné
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="personneListe"></param>
        /// <returns></returns>
        public List<Commentaire> chargerCommentaires(Evenement evt, Personne personneListe)
        {
            Commentaires comm = new Commentaires();

            List<Commentaire> listeRetour = new List<Commentaire>();

            DataTable comments = new DataTable();

            comments = comm.getCommentairesByPersonneListe(personneListe.id_personne, evt.id_evenement);

            if (comments != null)
            {
                for (int i = 0; i < comments.Rows.Count; i++)
                {
                    Commentaire unComm = new Commentaire();
                    unComm.commentaire = StringUtils.replaceSautDeLignePourHTML(comments.Rows[i].ItemArray.GetValue(0).ToString());
                    unComm.id_auteur = int.Parse(comments.Rows[i].ItemArray.GetValue(1).ToString());
                    unComm.ecrit_par = comments.Rows[i].ItemArray.GetValue(2).ToString();
                    unComm.date_creation = DateTime.Parse(comments.Rows[i].ItemArray.GetValue(3).ToString());
                    if (comments.Rows[i].ItemArray.GetValue(4) != null && "" !=comments.Rows[i].ItemArray.GetValue(4).ToString())
                    {
                        unComm.date_modification = DateTime.Parse(comments.Rows[i].ItemArray.GetValue(4).ToString());
                    }
                    unComm.deleted = Boolean.Parse(comments.Rows[i].ItemArray.GetValue(5).ToString()); ;

                    listeRetour.Add(unComm);
                }
            }

            return listeRetour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="personneListe"></param>
        /// <param name="commentaire"></param>
        public void enregistrerCommentaire(Evenement evt, Personne personneListe, Personne personneConnectee, Commentaire commentaire)
        {

            Commentaires commentaireDAL = new Commentaires();

            commentaireDAL.ajouterCommentaire(personneConnectee.id_personne, evt.id_evenement, personneListe.id_personne, commentaire.commentaire);

        }
    }
}
