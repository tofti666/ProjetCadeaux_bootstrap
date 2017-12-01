using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProjetCadeaux_Connection
{
    /// <summary>
    /// Ne plus utiliser cette classe, mais son équivalente objet IdeesCadeauxDAL
    /// </summary>
    public class IdeesCadeaux
    {

        ConnectionBase conn = new ConnectionBase();

        public String ajouterIdeeCadeau(string titre, string description, string prix, string priorite, string personneID, string lien1, string lien2, string lien3, string lien4, string lien5)
        {
            String sql = "SELECT obtenirTousLesTrucs('" + titre.Replace("'", "''") + "','" + description.Replace("'", "''") + "','" + prix.Replace("'", "''") + "','" + priorite.Replace("'", "''") + "','" + personneID
                + "','" + lien1.Replace("'", "''") + "','" + lien2.Replace("'", "''") + "','" + lien3.Replace("'", "''") + "','" + lien4.Replace("'", "''") + "','" + lien5.Replace("'", "''") + "');";

            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt.Rows[0].ItemArray.GetValue(0).ToString();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getIdeesCadeauxByPersonneEvenementAndPersonneConnectee(int personneID, int id_evt, int personneConnectee)
        {

            String sql = "SELECT SUM(v2.participation) as participation_totale, v.participation as participation, c.id_cadeau, c.intitule_cadeau, c.description, c.prix, i.priorite, i.id_ideecadeau "
                    + " FROM \"cadeaux\" c, \"ideesCadeaux\" i "
                    + " LEFT OUTER JOIN \"votes\" v"
                    + " ON v.id_ideecadeau = i.id_ideecadeau AND v.id_personne = '" + personneConnectee + "'"
                    + " LEFT OUTER JOIN \"votes\" v2"
                    + " ON v2.id_ideecadeau = i.id_ideecadeau "
                    + " LEFT OUTER JOIN \"listeCadeaux\" l ON l.id_evenement = "+id_evt+" AND l.id_personne = "+personneID+" "
                    + " LEFT OUTER JOIN \"listeCadeaux_Cadeaux\" g ON l.id_liste = g.\"id_listeCadeaux\" "
                    + " WHERE i.id_personne = '" + personneID + "'"
                    + " AND g.id_cadeaux = i.id_ideecadeau "
                    + " AND c.id_cadeau = i.id_cadeau AND propose_par is null "
                    + " GROUP BY c.id_cadeau, i.priorite, i.id_ideecadeau, v.participation "
                    + " ORDER BY i.priorite ASC;";
            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable getIdeesCadeauxByPersonneAndPersonneConnectee(string personneID, string personneConnectee)
        {

            String sql = "SELECT SUM(v2.participation) as participation_totale, v.participation as participation, c.id_cadeau, c.intitule_cadeau, c.description, c.prix, i.priorite, i.id_ideecadeau "
                    +" FROM \"cadeaux\" c, \"ideesCadeaux\" i"  
                    +" LEFT OUTER JOIN \"votes\" v"
                    +" ON v.id_ideecadeau = i.id_ideecadeau AND v.id_personne = '"+personneConnectee+"'"
                    + " LEFT OUTER JOIN \"votes\" v2"
                    + " ON v2.id_ideecadeau = i.id_ideecadeau "
                    +" WHERE i.id_personne = '"+personneID+"' AND c.id_cadeau = i.id_cadeau AND propose_par is null "
                    +" GROUP BY c.id_cadeau, i.priorite, i.id_ideecadeau, v.participation "
                    +" ORDER BY i.priorite ASC;";
            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getIdeesCadeauxByPersonne(string personneID)
        {

            String sql = "SELECT c.id_cadeau, c.intitule_cadeau, c.description, c.prix, i.priorite, i.id_ideecadeau "
                +"from \"cadeaux\" c, \"ideesCadeaux\" i "
                +"WHERE i.id_personne = '"+personneID+"' AND c.id_cadeau = i.id_cadeau AND propose_par is null GROUP BY c.id_cadeau, i.priorite, i.id_ideecadeau ORDER BY i.priorite ASC;";
            
            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getIdeesCadeauxEtParticipationByPersonne(string personneConnecte)
        {
            String sql = "SELECT SUM(v.participation)  as participation, c.id_cadeau, c.intitule_cadeau, c.description, c.prix, i.priorite, i.id_ideecadeau "
                + "from \"cadeaux\" c, \"ideesCadeaux\" i "
                + "LEFT OUTER JOIN \"votes\" v "
                + "ON i.id_ideecadeau = v.id_ideecadeau "
                + "WHERE i.id_personne = '" + personneConnecte + "' AND c.id_cadeau = i.id_cadeau AND propose_par is null GROUP BY c.id_cadeau, i.priorite, i.id_ideecadeau ORDER BY i.priorite ASC;";
            try
            {
                DataTable dt = conn.getConnection(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void deleteIdeeCadeau(string id_ideecadeau)
        {
            String sql = "SELECT supprimerideecadeau("+id_ideecadeau+");";

            try
            {
                conn.getConnection(sql);
            }
            catch (Exception)
            {

            }
        }

        public DataTable getIdeeCadeauById(string id_ideeCadeau)
        {
            String sql = "SELECT i.priorite, c.intitule_cadeau, c.description, c.prix FROM \"ideesCadeaux\" i, \"cadeaux\" c WHERE i.id_ideecadeau = '" + id_ideeCadeau + "' AND i.id_cadeau = c.id_cadeau ;";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable getAllInfosIdeeCadeauById(string id_ideeCadeau)
        {

            String sql = "SELECT i.priorite, c.intitule_cadeau, c.description, c.prix "
                +"FROM \"ideesCadeaux\" i, \"cadeaux\" c, \"liens\" l WHERE i.id_ideecadeau = '" + id_ideeCadeau +"' ;";

            try
            {
                return conn.getConnection(sql);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public void modifierIdeeCadeau(string cleIdeeCadeau, string titre, string description, string prix, string priorite, string lien1, string lien2, string lien3, string lien4, string lien5)
        {
            String sql = "UPDATE \"ideesCadeaux\" SET priorite = '" + priorite.Replace("'", "''") + "' WHERE id_ideecadeau = '" + cleIdeeCadeau + "'; "
                + "UPDATE \"cadeaux\" SET intitule_cadeau = '" + titre.Replace("'", "''") + "', description ='" + description.Replace("'", "''") + "',prix = '" + prix.Replace("'", "''") + "' WHERE id_cadeau = (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + cleIdeeCadeau + "'); "
                + "DELETE FROM \"liens\" WHERE id_cadeau = (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + cleIdeeCadeau + "'); "
                + "INSERT INTO \"liens\" (lien , id_cadeau) VALUES ( '" + lien1.Replace("'", "''") + "', (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + cleIdeeCadeau + "')); "
                + "INSERT INTO \"liens\" (lien , id_cadeau) VALUES ( '" + lien2.Replace("'", "''") + "', (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + cleIdeeCadeau + "')); "
                + "INSERT INTO \"liens\" (lien , id_cadeau) VALUES ( '" + lien3.Replace("'", "''") + "', (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + cleIdeeCadeau + "')); "
                + "INSERT INTO \"liens\" (lien , id_cadeau) VALUES ( '" + lien4.Replace("'", "''") + "', (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + cleIdeeCadeau + "')); "
                + "INSERT INTO \"liens\" (lien , id_cadeau) VALUES ( '" + lien5.Replace("'", "''") + "', (SELECT id_cadeau FROM \"ideesCadeaux\" WHERE id_ideecadeau ='" + cleIdeeCadeau + "')); ";

            try
            {
                conn.getVoidConnection(sql);
            }
            catch (Exception)
            {
            }
        }

    }
}
