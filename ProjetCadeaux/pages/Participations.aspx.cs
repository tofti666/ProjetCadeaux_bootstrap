using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProjetCadeaux_Connection;
using ProjetCadeauxBLL;
using ProjetCadeaux_Entites;

namespace ProjetCadeaux.pages
{
    public partial class Participations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {
                if (!IsPostBack)
                {
                    chargerListeEvenement();
                }
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        /// <summary>
        /// Evenement appelé quand on change d'évènement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_listeEvenements_Change(object sender, EventArgs e)
        {
            FailureText.Text = "";
            SuccessText.Text = "";

            ViewState["evenementId"] = ddl_listeEvenements.SelectedValue;
            ViewState["listePersonnesChargee"] = "false";

            updatePanel_recapitulatif.Visible = true;

            chargerTableauParticipations(ddl_listeEvenements.SelectedValue);
        }

        /// <summary>
        /// Charge l'ensemble des participations dans la datagrid
        /// </summary>
        /// <param name="p"></param>
        public void chargerTableauParticipations(string evenementId)
        {
            List<ListeIdeesCadeaux> listeIdeesCadeaux = null;
            List<Participation> listeParticipation = null;

            try
            {
                //Récupérer l'ensemble des listes actives pour cet évènement
                Evenement evt = new Evenement();
                evt.id_evenement = int.Parse(evenementId);
                ListeIdeesCadeauxBLL listeBLL = new ListeIdeesCadeauxBLL();

                listeIdeesCadeaux = listeBLL.getAllListeIdeesCadeaux(evt);
                
                //Récupérer l'ensemble des participations de la personne connectée pour cet évènement
                Participation parti = new Participation();
                parti.id_personne = int.Parse(Session["personneID"].ToString());

                ParticipationsBLL partService = new ParticipationsBLL();

                listeParticipation = partService.getAllParticipations(parti, evt);

                //Merger les deux infos dans une DataTable pour s'en servir de DataSource
                DataTable dtMerge = new DataTable();
                dtMerge = preparerTable(dtMerge);

                //Représente la liste des responsables avec l'argent que la personne connectée doit
                Dictionary<int, long> mapDette = new Dictionary<int, long>();
                //Représente la liste des sous que doivent les personnes pour les listes où l'utilisateur connecté est responsable
                Dictionary<int, long> mapCredit = new Dictionary<int, long>();
                //Représente la liste des noms par id, pour éviter de refaire une requête
                Dictionary<int, string> mapNoms = new Dictionary<int, string>();
                HashSet<int> setCle = new HashSet<int>();
                
                //On parcourt les listes actives
                foreach (ListeIdeesCadeaux liste in listeIdeesCadeaux)
                {
                    ResponsablesBLL respoService = new ResponsablesBLL();

                    //On cherche le responsable de la liste
                    Personne responsable = respoService.getInfosResponsableDe(liste.listeIdeesCadeauxPour, liste.listePourEvenement);

                    if (responsable != null)
                    {
                        //Si on est responsable de cette liste, on va ajouter les participations des autres
                        if(responsable.id_personne == int.Parse(Session["personneID"].ToString()))
                        {
                            List<Participation> listeParticipationAListe = new List<Participation>();
                            listeParticipationAListe = partService.getListeParticipation(liste);

                            foreach (Participation particAListe in listeParticipationAListe)
                            {
                                //S'il ne s'agit pas de notre propre participation
                                if (particAListe.id_personne != responsable.id_personne)
                                {
                                    //Si la personne devant de l'argent n'existait pas encore, on la créé avec la participation
                                    if (!mapCredit.ContainsKey(particAListe.id_personne))
                                    {
                                        mapCredit.Add(particAListe.id_personne, particAListe.participation);
                                        setCle.Add(particAListe.id_personne);
                                        if (!mapNoms.ContainsKey(particAListe.id_personne))
                                            mapNoms.Add(particAListe.id_personne, particAListe.personne.prenom + " " + particAListe.personne.nom.ToUpper());
                                    }
                                    //si on devait de l'argent au responsable, on ajoute la nouvelle participation
                                    else
                                    {
                                        mapCredit[particAListe.id_personne] = mapCredit[particAListe.id_personne] + particAListe.participation;
                                    }
                                }
                            }

                        }
                            //Si on n'est pas responsable, on ajoute ce qu'on doit au responsable
                        else{
                            foreach (Participation part in listeParticipation)
                            {
                                if (part.id_liste == liste.id_listeIdeesCadeaux)
                                {
                                    //Si le responsable n'existait pas encore, on le créé avec la participation
                                    if (!mapDette.ContainsKey(responsable.id_personne))
                                    {
                                        mapDette.Add(responsable.id_personne, part.participation);
                                        setCle.Add(responsable.id_personne);
                                        if(!mapNoms.ContainsKey(responsable.id_personne))
                                            mapNoms.Add(responsable.id_personne, responsable.prenom + " " + responsable.nom.ToUpper());
                                    }
                                        //si on devait de l'argent au responsable, on ajoute la nouvelle participation
                                    else
                                    {
                                        mapDette[responsable.id_personne] = mapDette[responsable.id_personne] + part.participation;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (int key in setCle)
                {
                    DataRow row = dtMerge.NewRow();

                    row["id"] = key;
                    row["personne"] = mapNoms[key];
                    row["jedois"] = mapDette.ContainsKey(key) ? mapDette[key] : 0;
                    row["onmedoit"] = mapCredit.ContainsKey(key) ? mapCredit[key] : 0;

                    dtMerge.Rows.Add(row);
                }

                //ATTENTION : Penser qu'on doit l'argent au responsable de la liste, et pas à celui pour qui est la liste !
               

                gridView_listeDettes.DataSource = dtMerge;
                gridView_listeDettes.DataBind();
            }
            catch (Exception e)
            {
                FailureText.Text = "Erreur dans la récupération du tableau";
                SuccessText.Text = "";
            }

        }

        private DataTable preparerTable(DataTable dtMerge)
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Merge");

            // Add three column objects to the table.
            DataColumn idColumn = new DataColumn();
            idColumn.DataType = System.Type.GetType("System.Int32");
            idColumn.ColumnName = "id";
            namesTable.Columns.Add(idColumn);

            DataColumn fNameColumn = new DataColumn();
            fNameColumn.DataType = System.Type.GetType("System.String");
            fNameColumn.ColumnName = "personne";
            fNameColumn.DefaultValue = "";
            namesTable.Columns.Add(fNameColumn);

            DataColumn lJeDoisColumn = new DataColumn();
            lJeDoisColumn.DataType = System.Type.GetType("System.String");
            lJeDoisColumn.ColumnName = "jedois";
            namesTable.Columns.Add(lJeDoisColumn);

            DataColumn lOnMeDoitColumn = new DataColumn();
            lOnMeDoitColumn.DataType = System.Type.GetType("System.String");
            lOnMeDoitColumn.ColumnName = "onmedoit";
            namesTable.Columns.Add(lOnMeDoitColumn);

            // Return the new DataTable.
            return namesTable;
        }

        private void chargerListeEvenement()
        {
            if (Session["personneId"] != null)
            {
                EvenementBLL evtBLL = new EvenementBLL();

                List<Evenement> listeEvt = evtBLL.getAllEvenementByIdPersonne(Session["personneId"].ToString());

                DataTable retour = new DataTable();

                retour.Columns.Add("ID");
                retour.Columns.Add("nom");

                DataRow dr2 = retour.NewRow();
                dr2["ID"] = "0";
                dr2["nom"] = "--";
                retour.Rows.Add(dr2);

                foreach (Evenement evt in listeEvt)
                {
                    String id = evt.id_evenement.ToString();
                    String libelle = evt.libelle;

                    DataRow dr = retour.NewRow();
                    dr["ID"] = id;
                    dr["nom"] = libelle;

                    retour.Rows.Add(dr);
                }

                ddl_listeEvenements.DataValueField = "ID";
                ddl_listeEvenements.DataTextField = "nom";

                ddl_listeEvenements.DataSource = retour;

                ddl_listeEvenements.DataBind();

            }
        }
    }
}