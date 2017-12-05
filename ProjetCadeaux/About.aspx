<%@ Page Title="Qui sommes-nous" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="ProjetCadeaux.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        À propos du site des cadeaux
    </h2>
    <br />
    <p>
        Ce site permet de centraliser les idées de cadeaux pour chacun, et de se mettre facilement d'accord sur des idées pour des cadeaux communs.
    </p>
    <br />
    <h4>Faire sa liste de cadeaux : </h4>
    <p>
        => François veut des Playmobils, et sait précisément lesquels : le bateau pirate et l'île au trésor.
        <br />
        Pas de problème ! Il suffit pour cela qu'il créé un compte sur le site des cadeaux, et qu'il renseigne sa liste de souhait.<br />
        Les autres pourront consulter la liste de voeux de François et voter pour ces idées. De plus, ils pourront indiquer leur participation. Le responsable du cadeau pourra voir les résultats du vote.
    </p>
    <br />
    <h4>Suggérer une idée de cadeau : </h4>
    <p>
        Il est également possible de proposer des idées de cadeaux pour quelqu'un : <br />
        => Anne-Marie verrait bien Agnès porter un déguisement de lapin rose qu'elle a vu sur internet :<br />
        Pas de problème encore ! Elle peut pour cela proposer une idée de cadeau, sans qu'Agnès ne le voit. De cette façon, on garde la surprise totale pour le jour de Noël.
    </p>
    <br />
    <h4>Gagner du temps en prenant la responsabilité de quelques cadeaux : </h4>
    <p>
        De façon à gagner du temps dans la recherche des cadeaux de Noël, on peut se désigner responsable des cadeaux d'une ou de plusieurs personnes. On peut alors se concentrer sur les cadeaux proposés, selon les participations sur les cadeaux.<br />
        => Olivier décide de s'occuper du cadeau de Céline, de celui d'Agnès, et de celui d'Alain. Il se désigne responsable du cadeau. Il s'aperçoit, grâce à l'écran de gestion des reponsabilités, que parmi la liste des cadeaux de Céline, deux ont plu à tout le monde et rentrent dans le budget : 
        <ul>
            <li>Un bonnet de laine rouge, que Céline avait mis dans sa liste</li>
            <li>Une soupière en fonte, que Julien avait proposé comme idée pour Céline</li>
        </ul>
        Olivier peut se débrouiller pour se les procurer (internet ou Vélizy 2), et les emballer pour les mettre sous le sapin.<br />
        Au final, ce dernier n'a eu la charge que de trois cadeaux, et peut passer le temps restant à s'occuper de Gary, au lieu de souffrir dans la chaleur et le monde de Vélizy 2.<br />
        Bien entendu, comme Olivier est quelqu'un d'organisé, il a pris soin de voter et d'indiquer sa participation pour les cadeaux des autres !
    </p>
    <br />
    <p>
        En espérant que le site fonctionne bien, qu'il plaise, et que tout le monde soit content de ses cadeaux !
    </p>
    <div style="margin:auto;" align="center">
            <asp:Image ID="imagePereNoel" runat="server" ImageUrl="~/images/pere-noel.jpg" />
        </div>
</asp:Content>
