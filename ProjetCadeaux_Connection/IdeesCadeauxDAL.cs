using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;
using ProjetCadeaux_BLL;

namespace ProjetCadeaux_Connection
{
    public class IdeesCadeauxDAL
    {

        ConnectionBase conn = new ConnectionBase();

        public void ajouterIdeeCadeau(string titre, string description, string prix, string priorite, string personneID, string lien1, string lien2, string lien3, string lien4, string lien5)
        {
            String sql = "SELECT obtenirTousLesTrucs('" + titre.Replace("'", "''") + "','" + description.Replace("'", "''") + "','" + prix.Replace("'", "''") + "','" + priorite.Replace("'", "''") + "','" + personneID
                + "','" + lien1.Replace("'", "''") + "','" + lien2.Replace("'", "''") + "','" + lien3.Replace("'", "''") + "','" + lien4.Replace("'", "''") + "','" + lien5.Replace("'", "''") + "');";

            try
            {
                conn.getConnection(sql);
            }
            catch (Exception)
            {

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

        /// <summary>
        /// Fonction récupérant une ideeCadeau à partir de l'id de l'idée de cadeau
        /// </summary>
        /// <param name="pId_ideeCadeau"></param>
        /// <returns></returns>
        public IdeeCadeau getIdeeCadeauById(int pId_ideeCadeau)
        {
            String sql = "SELECT i.\"id_ideecadeau\", i.\"id_personne\", i.\"id_cadeau\", i.\"priorite\", i.\"propose_par\" "
                + "FROM \"ideesCadeaux\" i "
                + "WHERE i.\"id_ideecadeau\" = " + pId_ideeCadeau + ";";

            ConnectionBase connection = new ConnectionBase();

            IdeeCadeau ideeCadeauRetour = new IdeeCadeau();

            try
            {
                DataTable dt = connection.getConnection(sql);


                ideeCadeauRetour.id_ideeCadeau = pId_ideeCadeau;

                //Récupération des infos de la personne pour laquelle il s'agit du cadeau
                PersonneDAL personneService = new PersonneDAL();
                ideeCadeauRetour.ideeCadeauPour = personneService.getInfosPersonne(int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString()));

                CadeauDAL cadeauService = new CadeauDAL();
                ideeCadeauRetour.cadeau = cadeauService.getInfosCadeau(int.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));

                ideeCadeauRetour.priorite = dt.Rows[0].ItemArray.GetValue(3).ToString();

                //Ce paramètre sera vide dans le cas où il ne s'agit pas d'une suggestion
                if(StringUtils.estNonNullNiVide(dt.Rows[0].ItemArray.GetValue(4).ToString()))
                    ideeCadeauRetour.proposePar = personneService.getInfosPersonne(int.Parse(dt.Rows[0].ItemArray.GetValue(4).ToString()));

                return ideeCadeauRetour;
            }
            catch (Exception)
            {
                throw new Exception("Erreur lors de la récupération de la liste de cadeaux");
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
