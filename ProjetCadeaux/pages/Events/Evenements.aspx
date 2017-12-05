<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Evenements.aspx.cs" Inherits="ProjetCadeaux.pages.Events.Evenements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>
            Gestion des évènements
        </h2>
        <h3>
            Cette page permet de visualiser les évènements auxquels on participe, en créer de nouveaux, et administrer ceux que l'on dirige.
        </h3>
        <span class="failureNotification">
            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <span class="successNotification">
            <asp:Literal ID="SuccessText" runat="server"></asp:Literal>
        </span>

        <div style="padding-top:20px;">
            <%
            
                ProjetCadeauxBLL.EvenementBLL evtBLL = new ProjetCadeauxBLL.EvenementBLL();

                string id_personne = Session["personneID"].ToString();

                List<ProjetCadeaux_Entites.Evenement> listeRetour = evtBLL.getAllEvenementByIdPersonne(id_personne);

                foreach (ProjetCadeaux_Entites.Evenement evt in listeRetour)
                {
                 %>
                <div  class=" col-xs-12 col-md-4" >
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <%= evt.libelle %> - <%= evt.dateEvenement.ToShortDateString() %>
                        </div>
                        <div class="panel-body">
                        
                        <p style="color:Gray; height:170px;">
                        Participants : <br />
                        
                            <%
                                ProjetCadeauxBLL.ParticipantBLL ptcpBLL = new ProjetCadeauxBLL.ParticipantBLL();

                                List<ProjetCadeaux_Entites.Participant> listeParticipants = ptcpBLL.getAllParticipantByEvenement(evt);


                                for (int i = 0; ((i < listeParticipants.Count) && (i < 16)); i++)
                                {
                                    if (i < 15)
                                    { %>
                                        <%= listeParticipants[i].nom_participant %> <% if (i != listeParticipants.Count-1){ %>- <% } %>
                            
                            <% }
                                    else
                                    {
                                        if (listeParticipants.Count - i == 1)
                                        { %>

                                            <%= listeParticipants[i].nom_participant%>
                                        <%
                                        }
                                        else
                                        {%> 
                                            [<%= listeParticipants.Count - i%> autres participants...]
                                        <%} %>
                                        <br />
                                        <br />
                                 <% }
                                } %>
                                </p>
                            <div style="text-align:right;">
                                <a style="color:White;" href="voirEvenement.aspx?evenementId=<%= evt.id_evenement %>" class="btn btn-success" >Aller à l'événement</a>
                            </div>
                        </div>
                    </div>
                </div>

                 <%
                }
                 %>

                <div class=" col-xs-12 col-md-4" >
                    <div class="panel panel-default">
                        <div class="panel-heading">
                           Nouvel événement
                        </div>
                        <div style="text-align:center;" class="panel-body">
                            
                            Créer un nouvel événement <br />
                            <br />

                            <div >
                                <a href="creerEvenement.aspx"  ><img style="height:150px;" alt="bouton créer nouvel événement" src="../../images/square-add_icon-icons.com_72277.png" /></a>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </div>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>

</asp:Content>
