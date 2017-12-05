<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="creerEvenement.aspx.cs" Inherits="ProjetCadeaux.pages.Events.creerEvenement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>
            Création évènement
        </h2>
        <h3>
            Création d'un nouvel évènement, possibilité d'ajouter des participants, ...
        </h3>
        <asp:UpdatePanel ID="updatePanelNotification" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_AjouterEvenement" />
                <asp:AsyncPostBackTrigger ControlID="btn_ModifierCadeau" />
            </Triggers>
            <ContentTemplate>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <span class="successNotification">
                    <asp:Literal ID="SuccessText" runat="server"></asp:Literal>
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="updatePanelCreerEvenement" runat="server">
            <ContentTemplate>
                <div class="row">
                <div class="col-xs-12 col-md-6">
                        <legend>Créer un évènement :</legend>
                </div>
                </div>
                <div class="row">
                <div class="col-xs-12 col-md-6">
                            <asp:Label ID="TitreEvenementLabel" runat="server"  AssociatedControlID="TitreEvenementTb">Titre évènement :</asp:Label><br />
                            <asp:TextBox ID="TitreEvenementTb" runat="server" class="form-control" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TitreEvenementRequired" runat="server" ControlToValidate="TitreEvenementTb" 
                                 CssClass="failureNotification" ErrorMessage="Il faut donner un titre à l'évènement."
                                  ToolTip="Un titre à l'évènement est requis." ValidationGroup="EvenementGroupe" >Il faut donner un titre à l'évènement.</asp:RequiredFieldValidator>
                        </div>
                </div>
                        <div class="row">
                <div class="col-xs-12 col-md-6">
                            <asp:Label ID="DateEvenementLabel" runat="server" AssociatedControlID="dateEvenementTb">Date de l'évènement :</asp:Label><br />
                            <asp:TextBox ID="dateEvenementTb" runat="server" class="form-control" ></asp:TextBox>
                            <asp:CalendarExtender ID="calendarExtDateEvenement" runat="server" TargetControlID="dateEvenementTb" Format="dd/MM/yyyy"/>
                            <asp:RequiredFieldValidator ID="dateEvenementRequired" runat="server" ControlToValidate="dateEvenementTb" 
                                 CssClass="failureNotification" ErrorMessage="Une date pour l'évènement est requise."
                                 ToolTip="Une description est requise." ValidationGroup="EvenementGroupe">Une date pour l'évènement est requise.</asp:RequiredFieldValidator>
                        </div>
                </div>
                        <div class="row">
                <div class="col-xs-12 col-md-6">
                            <asp:Label ID="DateButoirLabel" runat="server" AssociatedControlID="dateButoirTb">Date butoir de l'évènement :</asp:Label><br />
                            <asp:TextBox ID="dateButoirTb" runat="server" class="form-control" ></asp:TextBox>
                            <asp:CalendarExtender ID="calendarExtDateButoir" runat="server" TargetControlID="dateButoirTb" Format="dd/MM/yyyy"/>
                            <asp:RequiredFieldValidator ID="DateButoirRequired" runat="server" ControlToValidate="dateButoirTb" 
                                 CssClass="failureNotification" ErrorMessage="Une date butoir pour l'évènement est requise." 
                                 ToolTip="Une date butoir pour l'évènement est requise." ValidationGroup="EvenementGroupe">Une date butoir pour l'évènement est requise.</asp:RequiredFieldValidator>
                        </div>
                </div>
                        <div class="row">
                <div class="col-xs-12 col-md-6">
                            <asp:Label ID="hasListeLabel" runat="server" AssociatedControlID="hasListeCb">Aurez-vous une liste de cadeaux pour cet évènement ?</asp:Label><br />
                            <asp:CheckBox ID="hasListeCb" runat="server" />
                            </div>
                </div>
                        <div class="row" style="margin-top:30px;">
                <div class="col-xs-12 col-md-6">
                            <asp:Button ID="btn_AjouterEvenement" class="btn btn-default"  runat="server" Text="Créer l'évènement" OnClick="btnAjouterEvt_Click" ValidationGroup="EvenementGroupe"/>
                            <asp:Button ID="btn_ModifierCadeau" class="btn btn-default"  runat="server" Text="Modifier l'évènement" OnClick="btnModifierEvt_Click" Visible="false"/>
                        </div>
                </div>
                        <div class="row" style="margin-top:30px;">
                <div class="col-xs-12 col-md-12">
                        <asp:LinkButton ID="lb_retourEvenements" runat="server" PostBackUrl="~/pages/Events/Evenements.aspx" Text="Retour aux évènements" />
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
