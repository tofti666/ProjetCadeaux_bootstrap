﻿<%@ Page Title="Modifier le mot de passe" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePasswordSuccess.aspx.cs" Inherits="ProjetCadeaux.Account.ChangePasswordSuccess" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        J'ai retrouvé mon mot de passe !
    </h2>
    <p>
        Votre mot de passe a été modifié, et a été envoyé à l'adresse e-mail qui a été communiquée. Allez faire un tour dans votre boite mail !<br />
        <br />
        <br />
        A bientôt sur le site des cadeaux !
        <br />
    </p>
    <p>
        <asp:HyperLink ID="retourAccueil" runat="server" NavigateUrl="~/Account/Login.aspx" >Retour à la page de connexion.</asp:HyperLink>
    </p>
</asp:Content>
