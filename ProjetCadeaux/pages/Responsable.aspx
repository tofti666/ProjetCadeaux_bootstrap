<%@ Page Title="Mes responsabilités" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Responsable.aspx.cs"
    Inherits="ProjetCadeaux.pages.Responsable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 100%;">
        <h2>
            Mes responsabilités
        </h2>
        <p>
            C'est ici qu'on peut prendre nos responsabilités, et ainsi voir les votes et les
            participations.
            <br />
            Il faut choisir un évènement, puis on aura accès aux listes dont on est responsable.
        </p>
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
        <br />
        <asp:DropDownList ID="ddl_listeEvenements" class="dropdown-perso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_listeEvenements_Change" />
        <br />
        <br />
        <asp:UpdatePanel ID="updatePanel_infosReponsabilite" runat="server" Visible="false">
            <ContentTemplate>
                <asp:Label ID="lbl_listePersonneSansResponsable" runat="server" Text="Voici les personnes qui n'ont pas de responsable pour leur liste :" />
                <br />
                <p>
                    <asp:GridView ID="gridview_personnesSansResponsables" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="id_personne" RowStyle-Height="60" HeaderStyle-Height="40" OnRowCommand="gridView_OnRowCommand" Width="95%">
                        <Columns>
                            <asp:BoundField DataField="id_personne" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="nom" HeaderText="Nom" />
                            <asp:BoundField DataField="prenom" HeaderText="Prenom" />
                            <asp:ButtonField HeaderText="Devenir responsable" Text="Devenir responsable" CommandName="responsable" />
                        </Columns>
                        <EmptyDataTemplate>
                            Toutes les listes ont un responsable pour cet évènement.
                        </EmptyDataTemplate>
                    </asp:GridView>
                </p>
                <hr />
                <h2>
                    Tableau de bord
                </h2>
                <p>
                    Choisissez la liste d'une personne pour consulter les votes et les participations
                    aux cadeaux de la liste.
                </p>
                <p>
                    <asp:DropDownList ID="listePersonnes" class="dropdown-perso" runat="server" Width="60%" AutoPostBack="true"
                        OnTextChanged="listePersonnes_onIndexChanged" />
                </p>
                <asp:UpdatePanel ID="updatePanel_gridViewPersonneChoisie" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="listePersonnes" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gridView_cadeauResponsable" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="id_ideecadeau" RowStyle-Height="60" HeaderStyle-Height="40" OnRowCommand="gridView_cadeauResponsable_OnRowCommand"
                            Width="95%">
                            <Columns>
                                <asp:BoundField DataField="id_ideecadeau" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="id_cadeau" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="intitule_cadeau" HeaderText="Titre" />
                                <asp:BoundField DataField="description" HeaderText="Description" />
                                <asp:ButtonField HeaderText="Action" Text="Voir les votes" CommandName="voir" />
                            </Columns>
                            <EmptyDataTemplate>
                                Il n'y a aucun cadeau proposé pour cette personne. Ajoutez-en dans l'évènement !
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <asp:UpdatePanel ID="updatePanel_totalParticipation" runat="server" UpdateMode="Conditional"
                    Visible="false">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="lbl_participation" runat="server" Text="La participation totale pour ce cadeau est de : " /><asp:Label
                                ID="lbl_participationTotale" runat="server" />
                        </div>
                        <br />
                        <asp:GridView ID="gridView_detailsParticipation" RowStyle-Height="60" HeaderStyle-Height="40" runat="server" AutoGenerateColumns="false"
                            Width="95%">
                            <Columns>
                                <asp:BoundField DataField="personne" HeaderText="Personne" />
                                <asp:BoundField DataField="participation" HeaderText="Participation" />
                            </Columns>
                            <EmptyDataTemplate>
                                Il n'y a aucune participation pour cette liste.
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <br />
                <br />
                <br />
                <asp:UpdatePanel ID="updatePanel_detailCadeau" runat="server" UpdateMode="Conditional"
                    Visible="false">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="gridView_cadeauResponsable" />
                    </Triggers>
                    <ContentTemplate>
                        <hr />
                        <h2>
                            <u>Cadeau sélectionné :</u></h2>
                        <br />
                        <div style="width: 50%; float: left; margin-bottom: 10%;">
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_titreCadeau" runat="server" Text="Titre : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <asp:Label ID="titreIdeeCadeau" runat="server" Text="TITRE" />
                                </div>
                            </p>
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_description" runat="server" Text="Description : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <asp:Label ID="descriptionIdeeCadeau" runat="server" Text="DESCRIPTION" />
                                </div>
                            </p>
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_ordreDePrix" runat="server" Text="Ordre de prix : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <asp:Label ID="ordreDePrixIdeeCadeau" runat="server" Text="PRIX" />
                                </div>
                            </p>
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_priorite" runat="server" Text="Priorité : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <asp:Label ID="prioriteCadeau" runat="server" Text="PRIORITE" />
                                </div>
                            </p>
                            <br />
                            <div>
                                <asp:Label ID="lbl_votes" runat="server" Text="Voici les résultats du vote : " />
                                <br />
                                <asp:Label ID="lbl_votesOui" runat="server" /><asp:Label ID="lbl_voteOuiReste" runat="server"
                                    Text=" votes 'Oui' ;" />
                                <br />
                                <asp:Label ID="lbl_votesPourquoiPas" runat="server" /><asp:Label ID="lbl_votesPourquoiPasReste"
                                    runat="server" Text=" votes 'Pourquoi pas' ;" />
                                <br />
                                <asp:Label ID="lbl_votesNon" runat="server" /><asp:Label ID="lbl_votesNonReste" runat="server"
                                    Text=" votes 'Non'." />
                                <br />
                            </div>
                        </div>
                        <div style="width: 48%; float: left;">
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_lien1" runat="server" Text="Lien 1 : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <a id="Lien1" runat="server" href="">
                                        <asp:Label ID="lien1_nom" runat="server" Text="" /></a>
                                </div>
                            </p>
                            <br />
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_lien2" runat="server" Text="Lien 2 : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <a id="Lien2" runat="server" href="">
                                        <asp:Label ID="lien2_nom" runat="server" Text="" /></a>
                                </div>
                            </p>
                            <br />
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_lien3" runat="server" Text="Lien 3 : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <a id="Lien3" runat="server" href="">
                                        <asp:Label ID="lien3_nom" runat="server" Text="" /></a>
                                </div>
                            </p>
                            <br />
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_lien4" runat="server" Text="Lien 4 : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <a id="Lien4" runat="server" href="">
                                        <asp:Label ID="lien4_nom" runat="server" Text="" /></a>
                                </div>
                            </p>
                            <br />
                            <p>
                                <div style="float: left; width: 20%">
                                    <asp:Label ID="lbl_lien5" runat="server" Text="Lien 5 : " Style="text-decoration: underline" />
                                </div>
                                <div style="width: 79%">
                                    <a id="Lien5" runat="server" href="">
                                        <asp:Label ID="lien5_nom" runat="server" Text="" /></a>
                                </div>
                            </p>
                        </div>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
</asp:Content>
