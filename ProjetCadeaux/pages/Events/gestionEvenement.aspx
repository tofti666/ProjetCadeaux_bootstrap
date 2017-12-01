<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gestionEvenement.aspx.cs" Inherits="ProjetCadeaux.pages.Events.gestionEvenement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <h2>
            Administration de : <asp:Label ID="TitreEvenementLabel" runat="server" Text="Evenement" />
        </h2>
        <br />
        <h3>
            C'est ici qu'il est possible d'administrer l'évènement <asp:Label ID="TitreEvenementLabelh3" runat="server" Text="Evenement" />.
        </h3>
        <p>
            <asp:Label ID="ListeDesParticipantsLabel" runat="server" Text="Liste des participants :" /><br />
            <asp:UpdatePanel ID="updatePanelParticipants" runat="server" UpdateMode="Always" >
                <ContentTemplate>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <span class="successNotification">
                        <asp:Literal ID="SuccessText" runat="server"></asp:Literal>
                    </span>
                    <asp:GridView ID="gridViewParticipants" RowStyle-Height="60" HeaderStyle-Height="40" runat="server" Width="95%" 
                        AutoGenerateColumns="false" DataKeyNames="id_participant">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblCbChoix" runat="server" Text="Sélection" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbChoix" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="id_participant" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="nom_participant" ItemStyle-HorizontalAlign="Center" HeaderText="Participant"/>
                            <asp:BoundField DataField="dateAjout" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date d'ajout" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHasListe" runat="server" Text="A une liste ?" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align:center;"><asp:CheckBox ID="cbHasListe" runat="server" checked='<%# Eval("hasListe") %>' /></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <div style="width:95%;">
                        <div style="float:right;">
                            <asp:Button ID="btnModifierParticipants" class="btn btn-default" runat="server" OnClick="btnModifierParticipant_Click" Text="Enregistrer les modifications" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnSupprimerParticipant" class="btn btn-default" runat="server" OnClientClick="confirmSupprimer();" OnClick="btnSupprimerParticipant_Click" Text="Supprimer les participants sélectionnés" />
                
                    <asp:HiddenField ID="supprimerParticipants" runat="server" Value="" />

                    <script type="text/javascript">
                        function confirmSupprimer() {
                            var result = confirm("Voulez-vous vraiment supprimer ces participants ?");

                            document.getElementById("MainContent_supprimerParticipants").value = result ? "Y" : "N";
                        }
                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </p>
        <br />
        <p>
            <div style="border-style:double; border-width:thin; width:95%;">
                <asp:UpdatePanel ID="AjoutParticipantPanel" runat="server" >
                    <ContentTemplate>
                    <div class="row">
                    <div class="col-xs-12">
                        <h2>Ajouter un participant</h2>
                        </div>
                        </div>
                        <div class="row" style="margin-top:30px;">
                        <div class="col-xs-12">
                        <asp:Label ID="rechercheLabel" runat="server" Text="Rechercher une personne :" />
                        </div>
                        </div>
                        <div class="row" style="margin-top:30px;">
                        <div class="col-xs-12 col-md-6">
                        <asp:Label ID="rechercheNomParticipantLabel" runat="server" Text="Nom :" AssociatedControlID="rechercheNomParticipantTb" /><br />
                        <asp:TextBox ID="rechercheNomParticipantTb" class="form-control" runat="server" /><br />
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-xs-12 col-md-6">
                        <asp:Label ID="recherchePrenomParticipantLabel" runat="server" Text="Prénom :" AssociatedControlID="recherchePrenomParticipantTb" /><br />
                        <asp:TextBox ID="recherchePrenomParticipantTb" class="form-control" runat="server" /> <br />
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-xs-12 col-md-6">

                        <asp:Button ID="btnRechercheParticipant" class="btn btn-default" runat="server" Text="Rechercher la personne" OnClick="btnRecherchePersonne_Click" /><br />
                        </div>
                        </div>
                        <div class="row" style="margin-top:30px; margin-left:5px;">
                        <div class="col-xs-12">
                        <asp:UpdatePanel ID="updatePanelGridViewPersonnes" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gridViewPersonnesRecherche" RowStyle-Height="60" HeaderStyle-Height="40" runat="server" Width="95%" 
                                    AutoGenerateColumns="false" DataKeyNames="id_personne">
                                    <Columns>
                                        <asp:BoundField DataField="id_personne" HeaderText="ID" Visible="false" />
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCbChoixPersonne" runat="server" Text="Sélection" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbChoixPersonne" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblChoixListe" runat="server" Text="Aura une liste ?" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="text-align:center;"><asp:CheckBox ID="cbChoixListe" runat="server" /></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nom" ItemStyle-HorizontalAlign="Center" HeaderText="Nom"/>
                                        <asp:BoundField DataField="prenom" ItemStyle-HorizontalAlign="Center" HeaderText="Prénom" />
                                        <asp:BoundField DataField="email" ItemStyle-HorizontalAlign="Center" HeaderText="E-mail" />
                                    </Columns>
                                </asp:GridView>

                                <div class="row" style="margin-top:30px; margin-bottom:30px;">
                                <div class="col-xs-3 col-md-offset-9">
                                <asp:Button ID="btnAjouterParticipant" class="btn btn-default" runat="server" Text="Ajouter l'utilisateur" 
                                    OnClick="btnAjouterParticipant_Click" 
                                    OnClientClick="confirmAjouter();" Visible="false" />
                                    </div>
                                    </div>
                                <asp:HiddenField ID="hiddenFieldAjouterParticipant" runat="server" Value="" />

                                <script type="text/javascript">
                                    function confirmAjouter() {
                                        var result = confirm("Voulez-vous vraiment ajouter ces participants ?");

                                        document.getElementById("MainContent_hiddenFieldAjouterParticipant").value = result ? "Y" : "N";
                                    }
                                </script>    
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </p>
        <p>
            <asp:LinkButton id="retourEvenement" runat="server" PostBackUrl="~/pages/Events/evenement.aspx" Text="Retours aux évènements" />
        </p>
    </div>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
