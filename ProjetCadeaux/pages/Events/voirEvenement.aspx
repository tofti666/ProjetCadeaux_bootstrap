<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="voirEvenement.aspx.cs" Inherits="ProjetCadeaux.pages.Events.voirEvenement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 100%;">
        <h2>
            <asp:Label ID="TitreEvenementLabel" runat="server" Text="Evenement" />
        </h2>
        <br />
        <h3>
            Voici les informations au sujet de l'évènement
            <asp:Label ID="TitreEvenementLabelh3" runat="server" Text="Evenement" />.
        </h3>
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
        <asp:UpdatePanel ID="updatePanelInformations" runat="server">
            <ContentTemplate>
                <asp:Label ID="dateEvenementLabel" runat="server" Text="Date de l'évènement : " CssClass="bold" />
                <asp:Label ID="dateEvenement" runat="server" />
                <br />
                <asp:Label ID="dateButoirLabel" runat="server" Text="Date butoir : " CssClass="bold" />
                <asp:Label ID="dateButoir" runat="server" />
                <br />
                <br />
                <asp:Label ID="dataGridParticipantsLabel" runat="server" Text="Liste des participants"
                    CssClass="bold" /><br />
                <br />
                <asp:GridView ID="gridViewParticipants" RowStyle-Height="60" HeaderStyle-Height="40" runat="server" Width="100%" AutoGenerateColumns="false"
                    DataKeyNames="id_participant">
                    <Columns>
                        <asp:BoundField DataField="id_participant" HeaderText="ID" Visible="false" />
                        <asp:BoundField DataField="nom_participant" ItemStyle-HorizontalAlign="Center" HeaderText="Participant" HeaderStyle-CssClass="headerColonneTableau" />
                        <asp:BoundField DataField="dateAjout" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date d'ajout"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"  HeaderStyle-CssClass="headerColonneTableau"/>
                        <asp:CheckBoxField DataField="hasListe" ItemStyle-HorizontalAlign="Center" HeaderText="A une liste" HeaderStyle-CssClass="headerColonneTableau"/>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="text-align:right;padding-top:10px;">
            <asp:HyperLink ID="linkAdministrerEvenement" runat="server" Text="Administrer L'évènement"
                NavigateUrl="~/pages/Events/gestionEvenement.aspx?evenementId=" Visible="false" />
        </div>
        <br />
        <br />
        <asp:UpdatePanel ID="updatePanelListeCadeaux" runat="server">
            <ContentTemplate>
                <div style="padding-top:10px;" class="row">
                    <div class="col-xs-12 col-md-6" style="padding-top:10px;">
                        Choisissez, ci-dessous, un des participants (ayant une liste) pour voir sa liste de demandes :
                    </div>
                    <div class="col-xs-12 col-md-6" style="padding-top:10px;">
                        <asp:DropDownList ID="listeParticipantAyantListe" class="dropdown-perso" runat="server" AutoPostBack="true"
                            OnTextChanged="listePersonnes_onIndexChanged" />
                    </div>
                </div>
                <br />
                <asp:Label ID="lbl_responsable" runat="server" />
                <br />
                <p>
                    <asp:UpdatePanel ID="updatePanelListePropositions" runat="server" UpdateMode="Always"
                        Visible="false">
                        <ContentTemplate>
                            <div style="text-align: left;">
                                Voir les
                                <asp:Label ID="lbl_nbCommentaires" runat="server" Text="commentaires" /><asp:ImageButton
                                    ID="blabla_image" runat="server" OnClick="blabla_image_click" ImageUrl="~/images/bulle2.png"
                                    Width="37px" Height="24px" />
                            </div>
                            <br />
                            <p>
                                <asp:UpdatePanel ID="updatePanelParticipation" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblValeurParticipationListe" runat="server" Text="Votre participation à cette liste est de : 0 €" />
                                        <asp:Button ID="btnModifierParticipation" class="btn btn-default" runat="server" Text="Modifier" OnClick="btnModifierParticipation_Click" />
                                        
                                        <asp:Label ID="lblParticipationFinanciere" runat="server" Visible="false">Ma participation :</asp:Label>
                                        <asp:TextBox ID="tbParticipation" runat="server" CssClass="textEntry" ValidationGroup="participationGroup" Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTbParticipation" runat="server"
                                            ControlToValidate="tbParticipation" CssClass="failureNotification" ErrorMessage="Vous devez saisir votre participation"
                                            ToolTip="Vous devez saisir votre participation." ValidationGroup="participationGroup">Obligatoire</asp:RequiredFieldValidator>
                                        
                                        <asp:Button ID="btnAjouterParticipation" class="btn btn-default" runat="server" Text="Enregistrer" OnClick="btnAjouterParticipation_Click"  Visible="false"
                                            ValidationGroup="participationGroup" />
                                        <asp:Button ID="btnAnnulerAjoutParticipation" class="btn btn-default" runat="server" Text="Annuler modification"
                                            OnClick="btnAnnulerAjoutParticipation_Click"  Visible="false"/>
                                        <br />
                                        <span class="failureNotification">
                                            <asp:Literal ID="lbPbParticipation" runat="server"></asp:Literal>
                                        </span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </p>
                            <p>
                                <asp:Label ID="lbl_gridViewCadeaux" runat="server" Text="Voici la liste de souhaits pour cet évènement" /><asp:Label
                                    ID="lbl_nomSelectionne" runat="server" />
                                <br />
                                <asp:GridView ID="gridView_cadeaux" runat="server" Width="95%" AutoGenerateColumns="false"
                                    OnRowCommand="RowCommand_click" RowStyle-Height="60" HeaderStyle-Height="40" DataKeyNames="id_ideecadeau">
                                    <Columns>
                                        <asp:BoundField DataField="id_ideecadeau" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="id_cadeau" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="intitule_cadeau" HeaderText="Titre" HeaderStyle-Width="25%" />
                                        <asp:BoundField DataField="description" HeaderText="Description" HeaderStyle-Width="35%" />
                                        <asp:BoundField DataField="prix" HeaderText="Prix" HeaderStyle-Width="35%" />
                                        <asp:BoundField DataField="priorite" HeaderText="Priorité" HeaderStyle-Width="15%" />
                                        <asp:ButtonField HeaderText="Participation" Text="Voter" HeaderStyle-Width="10%"
                                            CommandName="participer" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        La liste de souhaits n'a pas été créée pour l'instant.
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </p>
                            <p>
                                <asp:Label ID="lbl_gridViewSuggestions" runat="server" Text="Voici les suggestions de cadeaux pour cet évènement" /><asp:Label
                                    ID="lbl_nomSelectionneSuggestion" runat="server" />
                                <br />
                                <asp:GridView ID="gridView_Suggestions" runat="server" Width="95%" AutoGenerateColumns="false"
                                    OnRowCommand="RowCommandSuggestions_click" RowStyle-Height="60" HeaderStyle-Height="40" DataKeyNames="id_ideecadeau">
                                    <Columns>
                                        <asp:BoundField DataField="id_ideecadeau" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="id_cadeau" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="propose_par" HeaderText="Proposé par" HeaderStyle-Width="15%" />
                                        <asp:BoundField DataField="intitule_cadeau" HeaderText="Titre" HeaderStyle-Width="25%" />
                                        <asp:BoundField DataField="description" HeaderText="Description" HeaderStyle-Width="35%" />
                                        <asp:BoundField DataField="prix" HeaderText="Prix" HeaderStyle-Width="10%" />
                                        <asp:ButtonField HeaderText="Participation" Text="Voter" CommandName="participer"
                                            HeaderStyle-Width="10%" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        Il n'y a pas encore de suggestions.
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <div style="text-align: right; padding-right: 5%; padding-top: 1%;">
                                    <asp:Button ID="btnAjoutSuggestionListe" class="btn btn-default" runat="server" Text="Ajouter des suggestions" />
                                    <asp:ModalPopupExtender ID="PopupAjoutSuggestion" runat="server" TargetControlID="btnAjoutSuggestionListe"
                                        PopupControlID="panelAjoutSuggestion" BackgroundCssClass="ModalPopupBG" DropShadow="true">
                                    </asp:ModalPopupExtender>
                                </div>
                                <asp:Panel ID="panelAjoutSuggestion" runat="server" CssClass="HellowWorldPopup">
                                    <div style="width: 90%; padding-left: 5%; padding-top: 1%; padding-bottom: 1%;">
                                        <div style="border-style: solid; border-width: 1px;">
                                            <asp:UpdatePanel ID="updatePanelAjoutCadeaux" runat="server">
                                                <ContentTemplate>
                                                    <p>
                                                        <legend>Ajouter un cadeau :</legend>
                                                        <p>
                                                            <asp:Label ID="TitreCadeauLabel" runat="server">Titre cadeau :</asp:Label>
                                                            <asp:TextBox ID="TitreCadeau" runat="server" CssClass="textEntry"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="TitreCadeauRequired" runat="server" ControlToValidate="TitreCadeau"
                                                                CssClass="failureNotification" ErrorMessage="Il faut donner un titre au cadeau."
                                                                ToolTip="Un titre cadeau est requis." ValidationGroup="LoginUserValidationGroup">Obligatoire</asp:RequiredFieldValidator>
                                                        </p>
                                                        <p>
                                                            <asp:Label ID="DescriptionLabel" runat="server">Description :</asp:Label>
                                                            <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Columns="60" Rows="8"
                                                                CssClass="textEntry"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                                                                CssClass="failureNotification" ErrorMessage="Une description est requise." ToolTip="Une description est requise."
                                                                ValidationGroup="LoginUserValidationGroup">Obligatoire</asp:RequiredFieldValidator>
                                                        </p>
                                                        <p>
                                                            <asp:Label ID="PrixLabel" runat="server">Ordre d'idée du prix :</asp:Label>
                                                            <asp:TextBox ID="Prix" runat="server" CssClass="textEntry"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PrixRequired" runat="server" ControlToValidate="Prix"
                                                                CssClass="failureNotification" ErrorMessage="Un ordre d'idée du prix est requis."
                                                                ToolTip="Un ordre d'idée du prix est requis." ValidationGroup="LoginUserValidationGroup">Obligatoire</asp:RequiredFieldValidator>
                                                        </p>
                                                        <p>
                                                            <asp:Label ID="PrioriteLabel" runat="server">Priorité du cadeau :</asp:Label>
                                                            <asp:TextBox ID="Priorite" runat="server" CssClass="textEntry"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Priorite"
                                                                CssClass="failureNotification" ErrorMessage="Une priorité est requise." ToolTip="Une priorité est requise."
                                                                ValidationGroup="LoginUserValidationGroup">Obligatoire</asp:RequiredFieldValidator>
                                                        </p>
                                                        <p>
                                                            <h3>
                                                                Ajouter des liens</h3>
                                                            <div style="margin-left: 5%;">
                                                                <p>
                                                                    <asp:Label ID="LienLabel1" runat="server">Lien 1 :</asp:Label>
                                                                    <asp:TextBox ID="tb_lien1" runat="server" CssClass="textEntry"></asp:TextBox>
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="LienLabel2" runat="server">Lien 2 :</asp:Label>
                                                                    <asp:TextBox ID="tb_lien2" runat="server" CssClass="textEntry"></asp:TextBox>
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="LienLabel3" runat="server">Lien 3 :</asp:Label>
                                                                    <asp:TextBox ID="tb_lien3" runat="server" CssClass="textEntry"></asp:TextBox>
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="LienLabel4" runat="server">Lien 4 :</asp:Label>
                                                                    <asp:TextBox ID="tb_lien4" runat="server" CssClass="textEntry"></asp:TextBox>
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="LienLabel5" runat="server">Lien 5 :</asp:Label>
                                                                    <asp:TextBox ID="tb_lien5" runat="server" CssClass="textEntry"></asp:TextBox>
                                                                </p>
                                                        </p>
                                                        <p>
                                                            <asp:Button ID="btn_AjouterCadeau" class="btn btn-default" runat="server" Text="Ajouter l'idée" OnClick="btnAjouterIdee_Click"
                                                                ValidationGroup="LoginUserValidationGroup" />
                                                            <br />
                                                            <asp:Button ID="btn_CacherIdees" class="btn btn-default" runat="server" Text="Retour à ma liste" />
                                                        </p>
                                                    </p>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="updatePanel_participationCadeau" runat="server" Visible="false">
                        <ContentTemplate>
                            <hr />
                            <h2>
                                <u>Cadeau sélectionné :</u></h2>
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
                            <div>
                                <p>
                                    <div style="float: left; width: 10%">
                                        <asp:Label ID="lbl_lien1" runat="server" Text="Lien 1 : " Style="text-decoration: underline" />
                                    </div>
                                    <div style="width: 90%">
                                        <a id="Lien1" runat="server" href="">
                                            <asp:Label ID="lien1_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <br />
                                <p>
                                    <div style="float: left; width: 10%">
                                        <asp:Label ID="lbl_lien2" runat="server" Text="Lien 2 : " Style="text-decoration: underline" />
                                    </div>
                                    <div style="width: 90%">
                                        <a id="Lien2" runat="server" href="">
                                            <asp:Label ID="lien2_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <br />
                                <p>
                                    <div style="float: left; width: 10%">
                                        <asp:Label ID="lbl_lien3" runat="server" Text="Lien 3 : " Style="text-decoration: underline" />
                                    </div>
                                    <div style="width: 90%">
                                        <a id="Lien3" runat="server" href="">
                                            <asp:Label ID="lien3_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <br />
                                <p>
                                    <div style="float: left; width: 10%">
                                        <asp:Label ID="lbl_lien4" runat="server" Text="Lien 4 : " Style="text-decoration: underline" />
                                    </div>
                                    <div style="width: 90%">
                                        <a id="Lien4" runat="server" href="">
                                            <asp:Label ID="lien4_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                                <br />
                                <p>
                                    <div style="float: left; width: 10%">
                                        <asp:Label ID="lbl_lien5" runat="server" Text="Lien 5 : " Style="text-decoration: underline" />
                                    </div>
                                    <div style="width: 90%">
                                        <a id="Lien5" runat="server" href="">
                                            <asp:Label ID="lien5_nom" runat="server" Text="" /></a>
                                    </div>
                                </p>
                            </div>
                            <br />
                            <div style="float: left;">
                                <div style="width: 100%;">
                                    <h2>
                                        <u>Indiquer son avis sur le cadeau :</u></h2>
                                    <br />
                                    <div style="width: 100%">
                                        <div style="float: left; width: 20%;">
                                            Je vote :
                                        </div>
                                        <div style="float: left; width: 40%">
                                            <asp:RadioButtonList ID="radioButtonList_vote" runat="server" RepeatDirection="Vertical"
                                                Width="100%">
                                                <asp:ListItem Text="oui" Value="1" />
                                                <asp:ListItem Text="pourquoi pas" Value="2" />
                                                <asp:ListItem Text="non" Value="3" />
                                            </asp:RadioButtonList>
                                            <span style="color: Red">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="radioButtonList_vote"
                                                    ValidationGroup="participation" ErrorMessage="Il faut renseigner un vote" runat="Server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div style="float: left; width: 40%">
                                            pour le cadeau.
                                        </div>
                                    </div>
                                    <p style="width: 50%; text-align: center;">
                                        <asp:Button ID="button_sauvegardeParticipation" class="btn btn-default" runat="server" Text="Voter"
                                            OnClick="btn_sauverParticipation_Click" ValidationGroup="participation" />
                                        <asp:Button ID="button_modificationParticipation" class="btn btn-default" runat="server" Text="Modifier mon vote"
                                            OnClick="btn_modificationParticipation_Click" ValidationGroup="participation"
                                            Visible="false" />
                                    </p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </p>
                <p>
                    <asp:UpdatePanel ID="updatePanelCommentaires" runat="server" UpdateMode="Always"
                        Visible="false">
                        <ContentTemplate>
                            <h3>
                                La place publique raconte :</h3>
                            <asp:GridView ID="gridView_Commentaires" runat="server" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="qui" HeaderText="Auteur" HeaderStyle-Width="25%" />
                                    <asp:BoundField DataField="commentaire" HeaderText="Commentaire" />
                                </Columns>
                                <EmptyDataTemplate>
                                    Il n'y a pas de commentaire sur cette liste de cadeaux.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <br />
                            <p>
                                <div style="display: inline;">
                                    <asp:Label ID="lbl_addcomment" runat="server" Text="Ajouter un commentaire : " />
                                    <asp:TextBox ID="tb_commment" runat="server" TextMode="MultiLine" Width="80%" />
                                </div>
                                <br />
                                <div style="text-align: center;">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div style="width: 100%; clear: left;">
            <asp:LinkButton ID="retourEvenement" runat="server" PostBackUrl="~/pages/Events/evenement.aspx"
                Text="Retours aux évènements" />
        </div>
        <br />
        <br />
    </div>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
