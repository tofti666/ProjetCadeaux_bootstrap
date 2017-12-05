<%@ Page Title="Modifier le mot de passe" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="MonCompte.aspx.cs" Inherits="ProjetCadeaux.Account.MonCompte" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Mon compte personnel
    </h2>
    <p>
        Cette page permet de modifier ses informations personnelles (nom, prénom, mot de
        passe, ...).
    </p>
    <div class="row">
        <span class="failureNotification">
            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span><span class="successNotification">
            <asp:Literal ID="SuccessText" runat="server"></asp:Literal>
        </span>
        <div class="col-xs-12 col-md-4">
            <asp:Label ID="NomLabel" runat="server" AssociatedControlID="Nom">Nom :</asp:Label>
            <br />
            <asp:TextBox ID="Nom" class="form-control col-xs-12 col-md-4" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="NomRequired" runat="server" ControlToValidate="Nom"
                CssClass="failureNotification" ErrorMessage="Il faut saisir un nom." ToolTip="Il faut saisir un nom."
                ValidationGroup="Groupe_informationsPersos">Il faut saisir un nom.</asp:RequiredFieldValidator>
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-md-4">
            <asp:Label ID="PrenomLabel" runat="server" AssociatedControlID="Prenom">Prénom :</asp:Label>
            <br />
            <asp:TextBox ID="Prenom" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PrenomRequired" runat="server" ControlToValidate="Prenom"
                CssClass="failureNotification" ErrorMessage="Il faut saisir un prénom." ToolTip="Il faut saisir un prénom."
                ValidationGroup="Groupe_informationsPersos">Il faut saisir un prénom.</asp:RequiredFieldValidator>
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-md-4">
            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">Mon e-mail :</asp:Label>
            <br />
            <asp:TextBox ID="Email" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                CssClass="failureNotification" ErrorMessage="Il faut saisir un e-mail." ToolTip="Il faut saisir un e-mail."
                ValidationGroup="Groupe_informationsPersos">Il faut saisir un e-mail.</asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row" style="margin-bottom:40px;">
        <div class="col-xs-12 col-md-4">
            <asp:Button ID="enregistrerInfosBouton" class="btn btn-default" runat="server" OnClick="ModifierInfos_Click"
                Text="Modifier mes informations" ValidationGroup="Groupe_informationsPersos" />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-md-4">
            <asp:Label ID="oldMotDePasseLabel" runat="server" AssociatedControlID="oldMotDePasseTb">Mon ancien mot de passe :</asp:Label><br />
            <asp:TextBox ID="oldMotDePasseTb" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="oldMotDePasseValidator" CssClass="failureNotification"
                ControlToValidate="oldMotDePasseTb" ValidationGroup="Groupe_motDePasse" ErrorMessage="Vous devez saisir votre ancien mot de passe."
                runat="Server">Vous devez saisir votre ancien mot de passe.</asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-md-4">
            <asp:Label ID="newMotDePasse1Label" runat="server" AssociatedControlID="newMotDePasse1Tb">Mon nouveau mot de passe :</asp:Label>
            <br />
            <asp:TextBox ID="newMotDePasse1Tb" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="newMotDePasse1Validator" CssClass="failureNotification"
                ControlToValidate="newMotDePasse1Tb" ValidationGroup="Groupe_motDePasse" ErrorMessage="Vous devez saisir votre nouveau mot de passe."
                runat="Server">Vous devez saisir votre nouveau mot de passe.</asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-md-4">
            <asp:Label ID="newMotDePasse2Label" runat="server" AssociatedControlID="newMotDePasse2Tb">Confirmez le nouveau mot de passe :</asp:Label>
            <br />
            <asp:TextBox ID="newMotDePasse2Tb" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="newMotDePasse2Validator" ControlToValidate="newMotDePasse2Tb"
                CssClass="failureNotification" ValidationGroup="Groupe_motDePasse" ErrorMessage="Vous devez confirmer votre mot de passe."
                runat="Server">Vous devez confirmer votre mot de passe.</asp:RequiredFieldValidator>
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-md-4">
            <asp:Button ID="RecoverButton" class="btn btn-default" runat="server" OnClick="ModifierMotDePasse_Click"
                Text="Modifier mon mot de passe" ValidationGroup="Groupe_motDePasse" />
        </div>
    </div>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
