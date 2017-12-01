<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="ProjetCadeaux.pages.Events.Event" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" language="javascript">
    function refresh(idevt, idp) {
        $.ajax({
            url: "contentEvent.aspx?evenementId=" + idevt + "&idpersonne=" + idp, // Ton fichier ou se trouve ton chat
            success:
                function (retour) {
                    $('#contentListe').html(retour); // rafraichi toute ta DIV "bien sur il lui faut un id "
                }
        });
    }
</script>

    <div style="height: 100%;">
        <h2>
            <div class="row">
                <div style="padding-bottom:10px;" class="col-xs-12 col-md-4">
                    <asp:Label ID="TitreEvenementLabel" runat="server" Text="Evenement" />
                </div>
                <div style="padding-bottom:10px;" class="col-xs-12 col-md-4">
                    <asp:HyperLink style="color:White;" CssClass="btn btn-success" ID="linkAdministrerEvenement" runat="server" Text="Administrer L'évènement"
                    NavigateUrl="~/pages/Events/gestionEvenement.aspx?evenementId=" Visible="false" />
                </div>
            </div>
        </h2>
        <br />
        <p>
            <asp:UpdatePanel ID="updatePanelNotifications" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span><span class="successNotification">
                        <asp:Literal ID="SuccessText" runat="server"></asp:Literal>
                    </span>
                </ContentTemplate>
            </asp:UpdatePanel>
        </p>
        <div style="width:100%;" class="row">
            <div class="col-xs-12 col-md-2 col-lg-2" >
                <div style="height: 100%; overflow-y:scroll; position:fixed;">
                    <%
                        foreach (ProjetCadeaux_Entites.Participant part in listeParticipantAyantListeCadeau)
                        { %>
                            <a style="margin-bottom:0px; cursor:pointer; " onclick="refresh(<%= part.id_evenement %>,<%= part.id_personne%>);">
                                <div class="panel panel-default" style="margin-bottom:5px;" >
                                    <div class="panel-heading">
                                        <%= part.nom_participant %>
                                    </div>
                                </div>
                            </a>
                    <% } %>
                </div>
            </div>
            <div class="hidden-xs hidden-md col-lg-1 " style=" margin-right:-60px;">
                <img src="../../images/coin_gris_fleche_reversed_white.png" style="width:20px;" />
            </div>
            <div id="contentListe" class="col-xs-12 col-md-9 col-lg-9" style="background-color: White; padding-left:35px; padding-right:20px;">


            </div>
        </div>
        
    </div>


</asp:Content>
