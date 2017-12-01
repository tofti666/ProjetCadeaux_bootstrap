<%@ Page Title="Ma liste de cadeaux" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaListe.aspx.cs" Inherits="ProjetCadeaux.pages.MaListe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>    
        <h2>
            Mes listes de cadeaux
        </h2>
        <h3>
            On peut remplir les listes de cadeaux des évènements auxquels on participe. Cette liste sera visible par les autres participants à l'évènement.
        </h3>

                <span class="failureNotification" style="color:Red;">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <span class="SuccessNotification" style="color:Green;">
                    <asp:Literal ID="SuccesText" runat="server"></asp:Literal>
                </span>


        <asp:UpdatePanel ID="updatePanelIdeesCadeaux" runat="server" UpdateMode="Conditional" Visible="true">
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_CacherIdees" />
                <asp:PostBackTrigger ControlID="gridViewMesCadeaux" />
            </Triggers>
            <ContentTemplate>
                <p>
                <br />
                    <asp:Label ID="lbl_choixEvenement" runat="server" Text="Choisissez un évènement :" />
                    <asp:DropDownList ID="ddl_choixEvenement" class="dropdown-perso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="choixEvenement_SelectIndexChanged" />
                    <br />
                    <br />
                    <asp:Label ID="lbl_maListe" runat="server" Text="Voici ma liste de cadeaux pour l'évènement sélectionné : " />
                    <br />
                    <div align="center">
                        <asp:Button ID="bouton_refresh" class="btn btn-default" runat="server" Text="Rafraîchir la liste" />                     
                    </div>
                    <br />
                    <asp:GridView ID="gridViewMesCadeaux" runat="server" Width="100%" 
                        AutoGenerateColumns="false"  RowStyle-Height="60" HeaderStyle-Height="40"  OnRowCommand="RowCommand_click" DataKeyNames="id_ideecadeau">
                        <Columns>
                            <asp:BoundField DataField="id_ideecadeau" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="id_cadeau" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="intitule_cadeau" HeaderText="Titre" HeaderStyle-Width="10%"/>
                            <asp:BoundField DataField="description" HeaderText="Description" />
                            <asp:BoundField DataField="prix" HeaderText="Prix" HeaderStyle-Width="7%"/>
                            <asp:BoundField DataField="priorite" HeaderText="Priorité" HeaderStyle-Width="10%" />
                            <asp:ButtonField HeaderText="Supprimer" Text="Supprimer cadeau" CommandName="supprimer" />
                            <asp:ButtonField HeaderText="Modifier" Text="Modifier cadeau" CommandName="modifier" />
                        </Columns>

                    </asp:GridView>
               
                    <br />
                    <asp:Label ID="lbl_ajoutIdee" runat="server" Text="Pour ajouter un souhait de cadeau, clique sur le bouton :" />
                    <asp:Button ID="btn_ajoutIdee" class="btn btn-default" runat="server" Text="Ajouter des idées" OnClick="btnAjoutIdee_Click"/>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updatePanelAjoutCadeaux" runat="server" UpdateMode="Conditional" Visible="false">
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_ajoutIdee" />
                <asp:PostBackTrigger ControlID="btn_AjouterCadeau" />
                <asp:PostBackTrigger ControlID="btn_ModifierCadeau" />
                <asp:PostBackTrigger ControlID="btn_CacherIdees" />
                <asp:PostBackTrigger ControlID="gridViewMesCadeaux" />
            </Triggers>
            <ContentTemplate>
                <p>
                        <legend>Ajouter un cadeau :</legend>
                        <p>
                            <asp:Label ID="TitreCadeauLabel" runat="server">Titre cadeau :</asp:Label>
                            <asp:TextBox ID="TitreCadeau" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TitreCadeauRequired" runat="server" ControlToValidate="TitreCadeau" 
                                 CssClass="failureNotification" ErrorMessage="Il faut donner un titre au cadeau." ToolTip="Un titre cadeau est requis." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="DescriptionLabel" runat="server">Description :</asp:Label>
                            <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Columns="60" Rows="8" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description" 
                                 CssClass="failureNotification" ErrorMessage="Une description est requise." ToolTip="Une description est requise." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PrixLabel" runat="server">Ordre d'idée du prix :</asp:Label>
                            <asp:TextBox ID="Prix" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PrixRequired" runat="server" ControlToValidate="Prix" 
                                 CssClass="failureNotification" ErrorMessage="Un ordre d'idée du prix est requis." ToolTip="Un ordre d'idée du prix est requis." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PrioriteLabel" runat="server">Priorité du cadeau :</asp:Label>
                            <asp:TextBox ID="Priorite" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Priorite" 
                                 CssClass="failureNotification" ErrorMessage="Une priorité est requise." ToolTip="Une priorité est requise." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                            <p>
                            <h3>Ajouter des liens</h3>
                                <div style="margin-left:5%;">
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
                                </div>
                            </p>
                        <p>
                            <asp:Button ID="btn_AjouterCadeau" class="btn btn-default" runat="server" Text="Ajouter l'idée" OnClick="btnAjouterIdee_Click"/>
                            <asp:Button ID="btn_ModifierCadeau" class="btn btn-default" runat="server" Text="Modifier l'idée" OnClick="btnModifierIdee_Click" Visible="false"/>
                            <br />
                            <asp:Button ID="btn_CacherIdees" class="btn btn-default" runat="server" Text="Retour à ma liste" OnClick="btnVoirMaListe_Click"/>
                        </p>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
</asp:Content>
