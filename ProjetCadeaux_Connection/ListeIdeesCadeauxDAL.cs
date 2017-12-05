using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetCadeaux_Entites;
using System.Data;

namespace ProjetCadeaux_Connection
{
    public class ListeIdeesCadeauxDAL
    {
        ConnectionBase connection;
        /// <summary>
        /// Créé une liste d'idées cadeaux à partir d'une personne, d'un évènement, et on ajoute l'info si la personne aura une liste.
        /// </summary>
        /// <param name="pId_personne"></param>
        /// <param name="pId_evt"></param>
        /// <param name="pIsListeActive"></param>
        /// <returns></returns>
        public ListeIdeesCadeaux creerListeIdeesCadeaux(int pId_personne, int pId_evt, Boolean pIsListeActive)
        {        
            try
            {
                connection = new ConnectionBase();
                String sql = "SELECT creerlistecadeaux(" + pId_personne + "," + pId_evt + "," + (pIsListeActive ? 1 : 0) + "); commit; ";
                    
                String sql2 = "SELECT l.id_liste, l.id_personne, l.date_creation_liste, l.id_evenement, l.liste_active "
                    +"FROM \"listeCadeaux\" l WHERE l.id_personne = " + pId_personne + " AND l.id_evenement = "+pId_evt +";";

                DataTable dt2 = connection.getConnection(sql);

                if (dt2.Rows[0].ItemArray.GetValue(0).ToString() != "OK")
                {
                    throw new Exception("La création de la liste de cadeaux ne s'est pas bien passée");
                }
                
                DataTable dt = connection.getConnection(sql2);

                ListeIdeesCadeaux listeIdeesCadeaux = new ListeIdeesCadeaux();


                PersonneDAL personneService = new PersonneDAL();
                Personne personneListe = new Personne();
                
                //On met l'id de la liste d'idées
                listeIdeesCadeaux.id_listeIdeesCadeaux = (int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString()));
                
                //On récupère les infos du propriétaire de la liste
                personneListe = personneService.getInfosPersonne(int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString()));
                listeIdeesCadeaux.listeIdeesCadeauxPour = personneListe;

                //On met la date de création de la liste
                listeIdeesCadeaux.dateCreationListe = (DateTime.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));

                DAL_Evenement evenementService = new DAL_Evenement();
                Evenement evtAssocie = new Evenement();

                //On récupère l'évènement associé à la liste - on part du principe de base que chaque liste n'appartient qu'à un évènement
                evtAssocie = evenementService.getEvenementById(int.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString()));
                listeIdeesCadeaux.listePourEvenement = evtAssocie;

                listeIdeesCadeaux.isListeActive = pIsListeActive;

                return listeIdeesCadeaux;
            }
            catch (Exception)
            {
                throw new Exception("La liste ne peut être créée.");
            }
        }
        
        /// <summary>
        /// Fonction permettant de retourner les infos d'une liste d'idées de cadeaux en fonction d'un évènement et d'une personne donnée
        /// </summary>
        /// <param name="pId_personne"></param>
        /// <param name="pId_evt"></param>
        /// <returns></returns>
        public ListeIdeesCadeaux getListeIdeesCadeaux(int pId_personne, int pId_evt)
        {
            ListeIdeesCadeaux listeIdeesCadeauxRetour = new ListeIdeesCadeaux();

            String sql = "SELECT l.id_liste "
                    + "FROM \"listeCadeaux\" l "
                    + "WHERE l.id_personne = " + pId_personne + " AND l.id_evenement = " + pId_evt + ";";

            try
            {
                connection = new ConnectionBase();
                DataTable dt = connection.getConnection(sql);

                int id_liste = int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());

                return getListeIdeesCadeaux(id_liste);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Fonction permettant de retourner les infos d'une liste d'idées de cadeaux en fonction d'un évènement et d'une personne donnée
        /// </summary>
        /// <param name="pId_personne"></param>
        /// <param name="pId_evt"></param>
        /// <returns></returns>
        public List<IdeeCadeauPourListe> getListeIdeesCadeauxByPersonneEtEvenement(int pId_personne, int pId_evt)
        {
            List<IdeeCadeauPourListe> listeIdeesCadeauxRetour = new List<IdeeCadeauPourListe>();

            String sql = "SELECT idees.id_cadeau as id_cadeau, date_ajout as date_creation, cadeau.intitule_cadeau as titre, "
                        +" cadeau.description as desc, cadeau.prix as prix, li.id_personne as liste_pour, idees.priorite as priorite, "
                        +" idees.propose_par as propose_par, pe.nom as nom_propose_par, pe.prenom as prenom_propose_par  "
                        +" FROM \"listeCadeaux_Cadeaux\" licaca"
                        +" INNER JOIN \"cadeaux\" cadeau ON (licaca.id_cadeaux = cadeau.id_cadeau)"
                        +" INNER JOIN \"ideesCadeaux\" idees ON (licaca.id_cadeaux = idees.id_cadeau)"
                        +" INNER JOIN \"listeCadeaux\" li ON (licaca.\"id_listeCadeaux\" = li.\"id_liste\" ) "
                        + "LEFT OUTER JOIN \"personnes\" pe ON ( idees.\"propose_par\" = pe.\"id_personne\"  ) "
                        +" where li.\"id_personne\"="+ pId_personne + " and li.\"id_evenement\" = " + pId_evt + " and li.\"liste_active\" = 1";

            try
            {
                connection = new ConnectionBase();
                DataTable dt = connection.getConnection(sql);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IdeeCadeauPourListe idee = new IdeeCadeauPourListe();
                        idee.cadeau.id_cadeau = int.Parse(dt.Rows[i]["id_cadeau"].ToString());
                        idee.dateAjoutIdeeCadeau = DateTime.Parse(dt.Rows[i]["date_creation"].ToString());
                        idee.cadeau.intitule_cadeau = dt.Rows[i]["titre"].ToString();
                        idee.cadeau.description = dt.Rows[i]["desc"].ToString();
                        idee.cadeau.prix = dt.Rows[i]["prix"].ToString();
                        idee.priorite = dt.Rows[i]["priorite"].ToString();
                        idee.ideeCadeauPour.id_personne = int.Parse(dt.Rows[i]["liste_pour"].ToString());
                        if (dt.Rows[i]["propose_par"].ToString() != "" 
                            && dt.Rows[i]["nom_propose_par"].ToString() != "" 
                            && dt.Rows[i]["prenom_propose_par"].ToString() != "")
                        {
                            idee.proposePar.id_personne = int.Parse(dt.Rows[i]["propose_par"].ToString());
                            idee.proposePar.nom = dt.Rows[i]["nom_propose_par"].ToString();
                            idee.proposePar.prenom = dt.Rows[i]["prenom_propose_par"].ToString();
                        }

                        listeIdeesCadeauxRetour.Add(idee);
                    }
                }

                return listeIdeesCadeauxRetour;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ListeIdeesCadeaux getListeIdeesCadeauxPasSuggere(int pId_personne, int pId_evt)
        {
            ListeIdeesCadeaux listeIdeesCadeauxRetour = new ListeIdeesCadeaux();

            String sql = "SELECT l.id_liste "
                    + "FROM \"listeCadeaux\" l "
                    + "WHERE l.id_personne = " + pId_personne + " AND l.id_evenement = " + pId_evt + ";";

            try
            {
                connection = new ConnectionBase();
                DataTable dt = connection.getConnection(sql);

                int id_liste = int.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());

                return getListeIdeesCadeauxPasSuggere(id_liste);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public ListeIdeesCadeaux getListeIdeesCadeauxPasSuggere(int pId_listeIdeesCadeaux)
        {
            String sql = "SELECT l.id_liste, l.id_personne, l.date_creation_liste, l.id_evenement, l.liste_active "
                    +"FROM \"listeCadeaux\" l "
                    +"WHERE l.id_liste = " + pId_listeIdeesCadeaux +";";
            
            try{
                connection = new ConnectionBase();
                DataTable dt = connection.getConnection(sql);

                ListeIdeesCadeaux listeIdeesCadeaux = new ListeIdeesCadeaux();

                PersonneDAL personneService = new PersonneDAL();
                Personne personneListe = new Personne();
                
                //On met l'id de la liste d'idées
                listeIdeesCadeaux.id_listeIdeesCadeaux = pId_listeIdeesCadeaux;
                
                //On récupère les infos du propriétaire de la liste
                personneListe = personneService.getInfosPersonne(int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString()));
                listeIdeesCadeaux.listeIdeesCadeauxPour = personneListe;

                //On met la date de création de la liste
                listeIdeesCadeaux.dateCreationListe = (DateTime.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));

                DAL_Evenement evenementService = new DAL_Evenement();
                Evenement evtAssocie = new Evenement();

                //On récupère l'évènement associé à la liste - on part du principe de base que chaque liste n'appartient qu'à un évènement
                evtAssocie = evenementService.getEvenementById(int.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString()));
                listeIdeesCadeaux.listePourEvenement = evtAssocie;

                listeIdeesCadeaux.isListeActive = "1".Equals(dt.Rows[0].ItemArray.GetValue(4).ToString());

                //On récupère maintenant l'ensemble des cadeaux associés à cette liste
                listeIdeesCadeaux.listeDeCadeaux = getListeIdeesCadeauxPasSuggerePourListe(listeIdeesCadeaux.id_listeIdeesCadeaux);


                return listeIdeesCadeaux;
            }
            catch (Exception)
            {
                throw new Exception("La liste ne peut être créée.");
            }
        }

        /// <summary>
        /// Récupère la liste des idées de cadeaux complétée par les cadeaux, les votes, les liens
        /// </summary>
        /// <param name="pId_listeIdeesCadeaux"></param>
        /// <returns>Un objet complet ListeIdeesCadeaux</returns>
        public ListeIdeesCadeaux getListeIdeesCadeaux(int pId_listeIdeesCadeaux)
        {
            String sql = "SELECT l.id_liste, l.id_personne, l.date_creation_liste, l.id_evenement, l.liste_active "
                    +"FROM \"listeCadeaux\" l "
                    +"WHERE l.id_liste = " + pId_listeIdeesCadeaux +";";
            
            try{
                ConnectionBase connection2 = new ConnectionBase();
                DataTable dt = connection2.getConnection(sql);

                ListeIdeesCadeaux listeIdeesCadeaux = new ListeIdeesCadeaux();

                PersonneDAL personneService = new PersonneDAL();
                Personne personneListe = new Personne();
                
                //On met l'id de la liste d'idées
                listeIdeesCadeaux.id_listeIdeesCadeaux = pId_listeIdeesCadeaux;
                
                //On récupère les infos du propriétaire de la liste
                personneListe = personneService.getInfosPersonne(int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString()));
                listeIdeesCadeaux.listeIdeesCadeauxPour = personneListe;

                //On met la date de création de la liste
                listeIdeesCadeaux.dateCreationListe = (DateTime.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));

                DAL_Evenement evenementService = new DAL_Evenement();
                Evenement evtAssocie = new Evenement();

                //On récupère l'évènement associé à la liste - on part du principe de base que chaque liste n'appartient qu'à un évènement
                evtAssocie = evenementService.getEvenementById(int.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString()));
                listeIdeesCadeaux.listePourEvenement = evtAssocie;

                listeIdeesCadeaux.isListeActive = "1".Equals(dt.Rows[0].ItemArray.GetValue(4).ToString());

                //On récupère maintenant l'ensemble des cadeaux associés à cette liste
                listeIdeesCadeaux.listeDeCadeaux = getListeIdeesCadeauxPourListe(listeIdeesCadeaux.id_listeIdeesCadeaux);


                return listeIdeesCadeaux;
            }
            catch (Exception)
            {
                throw new Exception("La liste ne peut être créée.");
            }
        }

        /// <summary>
        /// Récupère la liste des idées de cadeaux complétée par les cadeaux, les votes, les liens
        /// </summary>
        /// <param name="pId_listeIdeesCadeaux"></param>
        /// <returns>Un objet complet ListeIdeesCadeaux</returns>
        public ListeIdeesCadeaux getListeIdeesCadeauxLight(int pId_listeIdeesCadeaux)
        {
            String sql = "SELECT l.id_liste, l.id_personne, l.date_creation_liste, l.id_evenement, l.liste_active "
                    + "FROM \"listeCadeaux\" l "
                    + "WHERE l.id_liste = " + pId_listeIdeesCadeaux + ";";

            try
            {
                connection = new ConnectionBase();
                DataTable dt = connection.getConnection(sql);

                ListeIdeesCadeaux listeIdeesCadeaux = new ListeIdeesCadeaux();

                PersonneDAL personneService = new PersonneDAL();
                Personne personneListe = new Personne();

                //On met l'id de la liste d'idées
                listeIdeesCadeaux.id_listeIdeesCadeaux = pId_listeIdeesCadeaux;

                //On récupère les infos du propriétaire de la liste
                personneListe = personneService.getInfosPersonne(int.Parse(dt.Rows[0].ItemArray.GetValue(1).ToString()));
                listeIdeesCadeaux.listeIdeesCadeauxPour = personneListe;

                DAL_Evenement evenementService = new DAL_Evenement();
                Evenement evtAssocie = new Evenement();

                evtAssocie.id_evenement = int.Parse(dt.Rows[0].ItemArray.GetValue(3).ToString());
                listeIdeesCadeaux.listePourEvenement = evtAssocie;

                listeIdeesCadeaux.isListeActive = "1".Equals(dt.Rows[0].ItemArray.GetValue(4).ToString());

                return listeIdeesCadeaux;
            }
            catch (Exception)
            {
                throw new Exception("La liste ne peut être créée.");
            }
        }

        /// <summary>
        /// Récupère les idées de cadeaux complètes associées à la liste d'idées de cadeaux
        /// </summary>
        /// <param name="pId_listeCadeaux"></param>
        /// <returns>Une liste d'idées de cadeaux</returns>
        public List<IdeeCadeauPourListe> getListeIdeesCadeauxPourListe(int pId_listeCadeaux)
        {

            String sql = "SELECT \"id_listeCadeaux_cadeaux\", \"id_cadeaux\", \"date_ajout\" "
                    + "FROM \"listeCadeaux_Cadeaux\" "
                    + "WHERE \"id_listeCadeaux\" = " + pId_listeCadeaux + ";";

            List<IdeeCadeauPourListe> listeCadeauxPourListeRetour = new List<IdeeCadeauPourListe>();

            try
            {
                connection = new ConnectionBase();
                DataTable dt = connection.getConnection(sql);

                foreach (DataRow row in dt.Rows)
                {
                    IdeeCadeauPourListe ideeCadeauPourListe = new IdeeCadeauPourListe();
                    ideeCadeauPourListe.idIdeeCadeauPourListe = int.Parse(row.ItemArray.GetValue(0).ToString());
                    ideeCadeauPourListe.id_ideeCadeau = int.Parse(row.ItemArray.GetValue(1).ToString());
                    ideeCadeauPourListe.dateAjoutIdeeCadeau = (DateTime.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));
                    
                    //On récupère les infos sur l'idée de cadeau
                    IdeesCadeauxDAL ideeCadeauService = new IdeesCadeauxDAL();
                    IdeeCadeau ideeCadeauTmp = ideeCadeauService.getIdeeCadeauById(ideeCadeauPourListe.id_ideeCadeau);
                    ideeCadeauPourListe.cadeau = ideeCadeauTmp.cadeau;
                    ideeCadeauPourListe.priorite = ideeCadeauTmp.priorite;              

                    //On récupère l'ensemble des votes
                    VotesDAL voteService = new VotesDAL();
                    ideeCadeauPourListe.listeDeVotes = voteService.getListeVoteByIdeeCadeau(ideeCadeauPourListe.id_ideeCadeau);

                    //On récupère l'ensemble des liens
                    LiensDAL lienService = new LiensDAL();
                    ideeCadeauPourListe.listeDeLiens = lienService.getInfosLiensByIdeeCadeau(ideeCadeauPourListe.id_ideeCadeau);

                    listeCadeauxPourListeRetour.Add(ideeCadeauPourListe);
                }

                return listeCadeauxPourListeRetour;
            }
            catch (Exception)
            {
                throw new Exception("Erreur lors de la récupération de la liste de cadeaux");
            }
        }

        public List<IdeeCadeauPourListe> getListeIdeesCadeauxPasSuggerePourListe(int pId_listeCadeaux)
        {

            String sql = "SELECT \"id_listeCadeaux_cadeaux\", \"id_cadeaux\", \"date_ajout\" "
                    + "FROM \"listeCadeaux_Cadeaux\" "
                    + "WHERE \"id_listeCadeaux\" = " + pId_listeCadeaux + ";";

            List<IdeeCadeauPourListe> listeCadeauxPourListeRetour = new List<IdeeCadeauPourListe>();

            try
            {
                connection = new ConnectionBase();
                DataTable dt = connection.getConnection(sql);

                foreach (DataRow row in dt.Rows)
                {
                    IdeeCadeauPourListe ideeCadeauPourListe = new IdeeCadeauPourListe();
                    ideeCadeauPourListe.idIdeeCadeauPourListe = int.Parse(row.ItemArray.GetValue(0).ToString());
                    ideeCadeauPourListe.id_ideeCadeau = int.Parse(row.ItemArray.GetValue(1).ToString());
                    ideeCadeauPourListe.dateAjoutIdeeCadeau = (DateTime.Parse(dt.Rows[0].ItemArray.GetValue(2).ToString()));
                    
                    //On récupère les infos sur l'idée de cadeau
                    IdeesCadeauxDAL ideeCadeauService = new IdeesCadeauxDAL();
                    IdeeCadeau ideeCadeauTmp = ideeCadeauService.getIdeeCadeauById(ideeCadeauPourListe.id_ideeCadeau);
                    ideeCadeauPourListe.cadeau = ideeCadeauTmp.cadeau;
                    ideeCadeauPourListe.priorite = ideeCadeauTmp.priorite;              

                    //On récupère l'ensemble des votes
                    VotesDAL voteService = new VotesDAL();
                    ideeCadeauPourListe.listeDeVotes = voteService.getListeVoteByIdeeCadeau(ideeCadeauPourListe.id_ideeCadeau);

                    //On récupère l'ensemble des liens
                    LiensDAL lienService = new LiensDAL();
                    ideeCadeauPourListe.listeDeLiens = lienService.getInfosLiensByIdeeCadeau(ideeCadeauPourListe.id_ideeCadeau);
                    if(ideeCadeauTmp.proposePar == null)
                        listeCadeauxPourListeRetour.Add(ideeCadeauPourListe);

                }

                return listeCadeauxPourListeRetour;
            }
            catch (Exception)
            {
                throw new Exception("Erreur lors de la récupération de la liste de cadeaux");
            }
        }
        

        /// <summary>
        /// modifie les infos de la liste pour un evt et une personne donnée
        /// </summary>
        /// <param name="pId_personne"></param>
        /// <param name="pId_evt"></param>
        /// <param name="pIsListeActive"></param>
        /// <returns></returns>
        public Boolean updateActiveListe(int pId_personne, int pId_evt, Boolean pIsListeActive)
        {
            try
            {
                String sql = "UPDATE \"listeCadeaux\" SET liste_active = "+ (pIsListeActive ? 1 : 0) 
                    + " WHERE id_personne = " + pId_personne + " AND id_evenement = " + pId_evt + ";";

                connection = new ConnectionBase();
                connection.getVoidConnection(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// ajout d'une idée de cadeau à une liste d'idées de cadeau passé en paramètre
        /// </summary>
        /// <param name="pId_liste"></param>
        /// <param name="pId_ideeCadeau"></param>
        /// <returns></returns>
        public bool ajouterCadeauToListe(int pId_liste, int pId_ideeCadeau)
        {
            try
            {
                String sql = "INSERT INTO \"listeCadeaux_Cadeaux\" (\"id_listeCadeaux\", \"id_cadeaux\") VALUES("+pId_liste+","+pId_ideeCadeau+");";

                connection = new ConnectionBase();
                connection.getVoidConnection(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Supprime le lien existant entre un cadeau et une liste
        /// </summary>
        /// <param name="id_ideeCadeau"></param>
        /// <returns></returns>
        public bool supprimerCadeauFromListe(int id_ideeCadeau)
        {
            try
            {
                String sql = "DELETE FROM \"listeCadeaux_Cadeaux\" WHERE id_cadeaux = "+id_ideeCadeau+";";

                connection = new ConnectionBase();
                connection.getVoidConnection(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Récupère l'ensemble des Listes d'idées cadeaux pour un évènement donné
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<ListeIdeesCadeaux> getAllListeIdeesCadeaux(int pId_evt)
        {
            List<ListeIdeesCadeaux> listeIdeesCadeauxRetour = new List<ListeIdeesCadeaux>();

            String sql = "SELECT l.id_liste "
                    + "FROM \"listeCadeaux\" l "
                    + "WHERE l.id_evenement = " + pId_evt + " AND l.liste_active = 1;";

            try
            {
                connection = new ConnectionBase();
                DataTable dtAllListe = connection.getConnection(sql);

                foreach (DataRow row in dtAllListe.Rows)
                {
                    ListeIdeesCadeaux liste = new ListeIdeesCadeaux();
                    int id_liste = int.Parse(row.ItemArray.GetValue(0).ToString());
                    liste = getListeIdeesCadeauxLight(id_liste);
                    listeIdeesCadeauxRetour.Add(liste);
                }

                return listeIdeesCadeauxRetour;
            }
            catch (Exception)
            {
                return new List<ListeIdeesCadeaux>();
            }
        }
    }
}
