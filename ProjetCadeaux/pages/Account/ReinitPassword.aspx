<%@ Page Title="Modifier le mot de passe" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ReinitPassword.aspx.cs" Inherits="ProjetCadeaux.Account.ReinitPassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        J'ai perdu mon mot de passe.. :-(
    </h2>
    <p>
        Utilisez le formulaire ci-dessous pour récupérer un mot de passe.
    </p>
    <div class="accountInfo">
        <span class="failureNotification">
            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <p>
            Si vous avez oublié votre mot de passe, il est possible de le modifier ici. Après avoir saisi votre adresse e-mail, vous recevrez un message indiquant un lien cliquable, qui réinitialisera le mot de passe.
        </p>
        <p>
            <asp:Label ID="EmailRecoverLabel" runat="server" AssociatedControlID="EmailRecover">Mon e-mail :</asp:Label>
            <asp:TextBox ID="EmailRecover" runat="server" CssClass="textEntry"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRecoverRequired" runat="server" ControlToValidate="EmailRecover" 
                CssClass="failureNotification" ErrorMessage="Il faut saisir un e-mail." 
                ToolTip="Il faut saisir un e-mail." >*</asp:RequiredFieldValidator>
        </p>
        <p class="submitButton">
            <asp:Button ID="RecoverButton" runat="server" OnClick="ReinitPassword_Click" Text="Récupérer mon mot de passe"/>
        </p>

    </div>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
