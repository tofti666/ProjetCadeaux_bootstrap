using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProjetCadeaux_Connection
{
    public class Commentaires
    {

        ConnectionBase conn = new ConnectionBase();

        public void ajouterCommentaire(int id_auteur, int evenement_id, int id_personnecible, string commentaire)
        {
            String sql = "INSERT INTO \"commentaires\" (id_auteur, id_personneliste, commentaire, id_evenement) VALUES ('"
                + id_auteur + "','" + id_personnecible + "','" + commentaire.Replace("'", "''") + "', "+evenement_id+");";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {

            }
        }

        public void supprimerCommentaire(string id_commentaire)
        {
            String sql = "UPDATE \"commentaires\" SET supprime = 'true', date_modification = current_date" +
                            " WHERE id_commentaire = " + id_commentaire + ");"; // id_auteur + "','" + id_personnecible + "','" + commentaire.Replace("'", "''") + "', " + evenement_id + ");";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {

            }
        }

        public void modifierCommentaire(string id_commentaire, string commentaire)
        {
            String sql = "UPDATE \"commentaires\" SET supprime = 'true', date_modification = current_date" +
                " WHERE id_commentaire = " + id_commentaire + ");"; // id_auteur + "','" + id_personnecible + "','" + commentaire.Replace("'", "''") + "', " + evenement_id + ");";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {

            }
        }


        public DataTable getCommentairesByPersonneListe(int id_personneliste,int evenement_id)
        {
            String sql = "SELECT c.commentaire, c.id_auteur, p.prenom||' '||p.nom as qui, c.date_creation, c.date_modification, c.supprime"
                +" FROM \"commentaires\" c, \"personnes\" p WHERE c.id_personneliste = '" + id_personneliste + "' "
                +" AND c.id_evenement = "+ evenement_id+" AND p.id_personne = c.id_auteur ORDER BY id_commentaire;";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getCommentairesByPersonneListe(int id_personneliste)
        {
            String sql = "SELECT c.commentaire, c.id_auteur, p.prenom||' '||p.nom as qui "
                +" FROM \"commentaires\" c, \"personnes\" p WHERE c.id_personneliste = '" + id_personneliste + "' "
                +" AND p.id_personne = c.id_auteur ORDER BY id_commentaire;";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getNbCommentairesByListe(int id_personneListe)
        {
            String sql = "SELECT COUNT(id_commentaire) FROM \"commentaires\" WHERE id_personneliste = '" + id_personneListe + "';";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getNbCommentairesByListe(int id_personneListe, int id_evenement)
        {
            String sql = "SELECT COUNT(id_commentaire) FROM \"commentaires\" WHERE id_personneliste = '" + id_personneListe + "' AND id_evenement = "+id_evenement+";";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
