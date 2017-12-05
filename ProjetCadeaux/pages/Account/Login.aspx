<%@ Page Title="Se connecter" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="ProjetCadeaux.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Se connecter
    </h2>
    <p>
        Entrez le nom d'utilisateur et le mot de passe utilisé à la création du compte.<br />
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false" NavigateUrl="~/pages/Account/Register.aspx">S'inscrire en cliquant ici</asp:HyperLink> si vous n'avez pas déjà créé de compte.
    </p>
    <div class="row">
        <div class="col-xs-12 col-md-12">
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <div class="row">
                        <div class="col-xs-12 col-md-6">
                <fieldset class="login">
                    <legend>Informations de compte</legend>
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nom d'utilisateur :</asp:Label>
                        <asp:TextBox ID="UserName" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="Un nom d'utilisateur est requis." ToolTip="Un nom d'utilisateur est requis." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                             </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Mot de passe :</asp:Label>
                        <asp:TextBox ID="Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Un mot de passe est requis." ToolTip="Un mot de passe est requis." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                        <asp:HyperLink ID="OubliMotDePasse" runat="server" EnableViewState="false" NavigateUrl="~/pages/Account/ReinitPassword.aspx" >J'ai oublié mon mot de passe !</asp:HyperLink>
                    </div>
                    </div>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" class="btn btn-default" runat="server" OnClick="VerifierConnection_Click" Text="Se connecter" ValidationGroup="LoginUserValidationGroup"/>
                </p>
                </fieldset>
                </div>
                </div>
            </div>
            </div>

                <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
