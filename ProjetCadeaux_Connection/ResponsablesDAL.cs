using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux_Connection
{
    public class ResponsablesDAL
    {

        ConnectionBase conn = new ConnectionBase();

        public bool devenirResponsable(int id_responsable, int id_personne_choisie, int id_liste)
        {
            String sql = "INSERT INTO \"responsables\" (id_personne_responsable,id_personne_choisie,id_liste) VALUES (" + id_responsable + "," + id_personne_choisie + "," + id_liste + "); ";

            try
            {
                conn.getVoidConnection(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Personne getInfosResponsableDe(int personneID, int id_liste)
        {
            String sql = "SELECT p.nom , p.prenom, p.id_personne from \"responsables\" r, \"personnes\" p WHERE r.id_liste = "+id_liste+" AND r.id_personne_choisie = "+personneID+" AND p.id_personne = r.id_personne_responsable;";
            
            try
            {
                DataTable dt = conn.getConnection(sql);

                Personne personne = new Personne();

                personne.nom = dt.Rows[0].ItemArray.GetValue(0).ToString();
                personne.prenom = dt.Rows[0].ItemArray.GetValue(1).ToString();
                personne.id_personne = int.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString());

                return personne;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Personne> getListeInfosResponsabilite(int personneID, int id_evenement)
        {
            String sql = "SELECT p.nom, p.prenom, p.id_personne, r.id_liste "
                +"FROM responsables r, \"listeCadeaux\" l, personnes p "
                +"WHERE r.id_personne_responsable  = "+personneID+" and r.id_liste = l.id_liste and l.id_evenement = "+id_evenement+" and l.id_personne = p.id_personne";
            
            try
            {
                DataTable dt = conn.getConnection(sql);

                List<Personne> listePersonne = new List<Personne>();

                //Construit la liste des participants
                foreach (DataRow row in dt.Rows)
                {
                    Personne personne = new Personne();
                    personne.nom = row.ItemArray.GetValue(0).ToString();
                    personne.prenom = row.ItemArray.GetValue(1).ToString();
                    personne.id_personne = (int.Parse(row.ItemArray.GetValue(2).ToString()));

                    listePersonne.Add(personne);
                }

                return listePersonne;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool nePlusEtreResponsableDe(int id_responsable, int id_personne_choisie, int id_liste)
        {
            String sql = "DELETE FROM \"responsables\" WHERE id_responsables = '" + id_responsable + "' AND id_personne_choisie = '" + id_personne_choisie + "' AND id_liste = "+id_liste+";";

            try
            {
                conn.getVoidConnection(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool estResponsable(int id_responsable, int id_personne_choisie, int id_liste)
        {
            String sql = "SELECT COUNT(id_personne_responsable) FROM \"responsables\" WHERE id_responsable = '" + id_responsable + "' AND id_personne_choisie = '"+id_personne_choisie+"' AND id_liste = "+id_liste+" ;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                int retour = int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());

                return retour > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Personne> getListePersonnesSansResponsables(int id_personneConnectee, int id_evenement)
        {
            String sql = "SELECT p.id_personne, p.nom, p.prenom FROM \"personnes\" p, \"participants\" pa "
                + "WHERE pa.id_personne_participant = p.id_personne AND pa.id_evt_participant = " + id_evenement + " AND pa.has_liste = 1 AND pa.id_personne_participant not in ("
                    + "SELECT r.id_personne_choisie FROM \"responsables\" r, \"listeCadeaux\" l WHERE l.id_liste = r.id_liste AND l.id_evenement = "+id_evenement
                + ") AND p.id_personne != " + id_personneConnectee + " ORDER BY p.nom ASC;";

            try
            {
                DataTable dt = conn.getConnection(sql);

                List<Personne> listePersonne = new List<Personne>();

                //Construit la liste des participants
                foreach (DataRow row in dt.Rows)
                {
                    Personne personne = new Personne();
                    personne.id_personne = int.Parse(row.ItemArray.GetValue(0).ToString());
                    personne.nom = row.ItemArray.GetValue(1).ToString().ToUpper();
                    personne.prenom = row.ItemArray.GetValue(2).ToString();

                    listePersonne.Add(personne);
                }

                return listePersonne;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
