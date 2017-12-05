<%@ Page Title="Les listes de cadeaux" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="VoirListes.aspx.cs" Inherits="ProjetCadeaux.VoirListes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Liste de cadeaux
    </h2>
        <div style="float:left; width:100%;">
            <div style="width:70%; float:left;">
                    Choisis une personne ci-dessous pour voir sa liste de demandes :
                    <br />
                    <asp:DropDownList ID="listePersonnes" runat="server" Width="80%" AutoPostBack="true" OnTextChanged="listePersonnes_onIndexChanged"/>
                </div>
                <div style="width:25%; float:left;">
                    <asp:Image ID="img_liste" runat="server" ImageUrl="~/images/Liste_complete.gif" width="50%" />
                </div>
        </div>
                
            <br />
            <span style="color:Green;">
                <b><asp:Label ID="lbl_validation" runat="server" /></b>
            </span>
            <br />
            <asp:Label id="lbl_responsable" runat="server" />
            <br />
            <p>
                <asp:UpdatePanel id="updatePanelListePropositions" runat="server" UpdateMode="Conditional" Visible="false">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="gridView_cadeaux" />
                        <asp:PostBackTrigger ControlID="button_modificationParticipation" />
                        <asp:PostBackTrigger ControlID="button_sauvegardeParticipation" />
                        <asp:PostBackTrigger ControlID="btn_retourListe" />
                    </Triggers>
                    <ContentTemplate>
                        <div style="text-align:left;">
                            Voir les <asp:Label ID="lbl_nbCommentaires" runat="server" Text="commentaires" /><asp:ImageButton ID="blabla_image" runat="server" OnClick="blabla_image_click" ImageUrl="~/images/bulle2.png" Width="75px" Height="48px" />
                        </div>
                        <p>
                            <asp:Label ID="lbl_gridViewCadeaux" runat="server" Text="Voici la liste de souhaits" /><asp:Label ID="lbl_nomSelectionne" runat="server" />
                            <br />
                            <asp:GridView ID="gridView_cadeaux" runat="server" Width="100%" 
                                AutoGenerateColumns="false" OnRowCommand="RowCommand_click" DataKeyNames="id_ideecadeau">
                                <Columns>
                                    <asp:BoundField DataField="id_ideecadeau" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="id_cadeau" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="participation" HeaderText="Ma participation" />
                                    <asp:BoundField DataField="participation_totale" HeaderText="Participation totale" />
                                    <asp:BoundField DataField="intitule_cadeau" HeaderText="Titre" />
                                    <asp:BoundField DataField="description" HeaderText="Description" />
                                    <asp:BoundField DataField="prix" HeaderText="Prix" />
                                    <asp:BoundField DataField="priorite" HeaderText="Priorité" />
                                    <asp:ButtonField HeaderText="Participation" Text="Voter et indiquer sa participation" CommandName="participer" />
                                </Columns>
                                <EmptyDataTemplate>
                                    La liste de souhaits n'a pas été créée pour l'instant.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </p>
                        <p>
                            <asp:Label ID="lbl_gridViewSuggestions" runat="server" Text="Voici les suggestions de cadeaux" /><asp:Label ID="lbl_nomSelectionneSuggestion" runat="server" />
                            <br />
                            <asp:GridView ID="gridView_Suggestions" runat="server" Width="100%" 
                                AutoGenerateColumns="false" OnRowCommand="RowCommandSuggestions_click" DataKeyNames="id_ideecadeau">
                                <Columns>
                                    <asp:BoundField DataField="id_ideecadeau" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="id_cadeau" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="participation" HeaderText="Ma participation" />
                                    <asp:BoundField DataField="participation_totale" HeaderText="Participation totale" />
                                    <asp:BoundField DataField="propose_par" HeaderText="Proposé par" />
                                    <asp:BoundField DataField="intitule_cadeau" HeaderText="Titre" />
                                    <asp:BoundField DataField="description" HeaderText="Description" />
                                    <asp:BoundField DataField="prix" HeaderText="Prix" />
                                    <asp:ButtonField HeaderText="Participation" Text="Voter et indiquer sa participation" CommandName="participer" />
                                </Columns>
                                <EmptyDataTemplate>
                                    Il n'y a pas encore de suggestions.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </p>
            <p>
                <asp:UpdatePanel ID="updatePanel_participationCadeau" runat="server" UpdateMode="Conditional" Visible="false">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="gridView_cadeaux" />
                        <asp:PostBackTrigger ControlID="gridView_Suggestions" />
                    </Triggers>
                    <ContentTemplate>
                    <hr />

                        <h2><u>Cadeau sélectionné :</u></h2>
                        <p>
                            <div style="float:left; width:20%">
                                <asp:Label id="lbl_titreCadeau" runat="server" Text="Titre : " style="text-decoration:underline" />
                            </div>
                            <div style="width:79%">
                                <asp:Label ID="titreIdeeCadeau" runat="server" Text="TITRE" />
                            </div>
                        </p>
                        <p>
                            <div style="float:left; width:20%">
                                <asp:Label id="lbl_description" runat="server" Text="Description : " style="text-decoration:underline" />
                            </div>
                            <div style="width:79%">
                                <asp:Label ID="descriptionIdeeCadeau" runat="server" Text="DESCRIPTION" />
                            </div>
                        </p>
                        <p>
                            <div style="float:left; width:20%">
                                <asp:Label id="lbl_ordreDePrix" runat="server" Text="Ordre de prix : " style="text-decoration:underline" />
                            </div>
                            <div style="width:79%">
                                <asp:Label ID="ordreDePrixIdeeCadeau" runat="server" Text="PRIX" />
                            </div>
                        </p>
                        <div>
                                <p>
                                    <div style="float:left; width:10%">
                                        <asp:Label id="lbl_lien1" runat="server" Text="Lien 1 : " style="text-decoration:underline" />
                                    </div>
                                    <div style="width:90%">
                                        <a id="Lien1" runat="server" href="" ><asp:Label ID="lien1_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <p>
                                    <div style="float:left; width:10%">
                                        <asp:Label id="lbl_lien2" runat="server" Text="Lien 2 : " style="text-decoration:underline" />
                                    </div>
                                    <div style="width:90%">
                                        <a id="Lien2" runat="server" href="" ><asp:Label ID="lien2_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <p>
                                    <div style="float:left; width:10%">
                                        <asp:Label id="lbl_lien3" runat="server" Text="Lien 3 : " style="text-decoration:underline" />
                                    </div>
                                    <div style="width:90%">
                                        <a id="Lien3" runat="server" href="" ><asp:Label ID="lien3_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <p>
                                    <div style="float:left; width:10%">
                                        <asp:Label id="lbl_lien4" runat="server" Text="Lien 4 : " style="text-decoration:underline" />
                                    </div>
                                    <div style="width:90%">
                                        <a id="Lien4" runat="server" href="" ><asp:Label ID="lien4_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <p>
                                    <div style="float:left; width:10%">
                                        <asp:Label id="lbl_lien5" runat="server" Text="Lien 5 : " style="text-decoration:underline" />
                                    </div>
                                    <div style="width:90%">
                                        <a id="Lien5" runat="server" href="" ><asp:Label ID="lien5_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                            </div>

                        <div style="float:left;">
                            <div style="width:100%;">
                                <h2><u>Indiquer son avis sur le cadeau :</u></h2>
                                <br />
                                <div style="width:100%">
                                    <div style="float:left; width:10%;">
                                        Je vote :
                                    </div>
                                    <div style="float:left; width:30%">
                                        <asp:RadioButtonList ID="radioButtonList_vote" runat="server" RepeatDirection="Vertical" Width="100%">
                                            <asp:ListItem Text="oui" Value="1" />
                                            <asp:ListItem Text="pourquoi pas" Value="2" />
                                            <asp:ListItem Text="non" Value="3" />
                                        </asp:RadioButtonList>
                                        <span style="color:Red">
                                            <asp:requiredfieldvalidator id="RequiredFieldValidator1"
                                              controltovalidate="radioButtonList_vote"
                                              validationgroup="participation"
                                              errormessage="Il faut renseigner un vote"
                                              runat="Server">
                                            </asp:requiredfieldvalidator>
                                        </span>
                                    </div>
                                    <div >
                                         pour le cadeau.
                                    </div>
                                </div>
                                <div style="clear:left; width:100%">
                                    <br />
                                    Ma participation à ce cadeau sera de : <asp:TextBox ID="Tb_participation" runat="server" Text="PARTICIPATION" /> €.
                                    <span style="color:Red">
                                        <asp:requiredfieldvalidator id="RequiredFieldValidator2"
                                              controltovalidate="Tb_participation"
                                              validationgroup="participation"
                                              errormessage="Il faut renseigner la participation."
                                              runat="Server">
                                        </asp:requiredfieldvalidator>
                                    </span>
                                    <br />
                                    (Pour info, la participation actuelle est de : <asp:Label ID="participationActuelle" runat="server" Text="TOTAL" />€ )
                                    <br />
                                </div>

                                <p style=" width:50%; text-align:center;">
                                    <asp:Button ID="button_sauvegardeParticipation" runat="server" Text="Participer" OnClick="btn_sauverParticipation_Click" ValidationGroup="participation" />
                                    <asp:Button ID="button_modificationParticipation" runat="server" Text="Modifier la participation" OnClick="btn_modificationParticipation_Click" ValidationGroup="participation" Visible="false"/>                        
                                </p>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </p>
            <p>
                <asp:UpdatePanel id="updatePanelCommentaires" runat="server" UpdateMode="Conditional" Visible="false">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="blabla_image" />
                        <asp:PostBackTrigger ControlID="btn_retourListe" />
                    </Triggers>          
                    <ContentTemplate>
                        
                        <h3>La place publique raconte :</h3>

                        <asp:GridView ID="gridView_Commentaires" runat="server" Width="100%" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="qui" HeaderText="Auteur"/>
                                <asp:BoundField DataField="commentaire" HeaderText="Commentaire"/>
                            </Columns>
                            <EmptyDataTemplate>
                                Il n'y a pas de commentaire sur cette liste de cadeaux.
                            </EmptyDataTemplate>
                        </asp:GridView>

                        <br />
                        <p>
                            <div style="display: inline; ">
                                <asp:Label ID="lbl_addcomment" runat="server" Text="Ajouter un commentaire : " />
                                <asp:TextBox ID="tb_commment" runat="server" TextMode="MultiLine" Width="80%" />
                            </div>
                            <br />
                            <div style="text-align:center;">
                                <asp:Button ID="btn_ajoutComment" runat="server" Text="Ajouter commentaire" OnClick="btn_ajoutComment_Click" />
                            </div>
                        </p>
                        <br />
                        <p>
                            <asp:Button ID="btn_retourListe" runat="server" OnClick="btn_retourListe_Click" Text="Retour à la liste de cadeaux" />
                        </p>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </p>
            <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
</asp:Content>
