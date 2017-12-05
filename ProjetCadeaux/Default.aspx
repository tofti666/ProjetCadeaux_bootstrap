<%@ Page Title="Page d'accueil" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ProjetCadeaux._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Bienvenue sur le site des cadeaux.
    </h2>
    <div style="text-align: justify;">
        <br />
        C'est ici qu'on peut :
        <ul>
            <li>publier sa liste de souhaits ;</li>
            <li>soumettre une idée de cadeau groupé pour quelqu'un ;</li>
            <li>se désigner volontaire pour être responsable du cadeau groupé d'une personne ;</li>
            <li>voter pour une idée de cadeau groupé ;</li>
            <li>annoncer sa participation à un cadeau ;</li>
        </ul>
        Par contre, bien entendu, on ne peut tenir responsable Christophe pour tout cadeau
        insatisfaisant !<br />
    </div>
    <br />
    <div class="row">
    <div class="col-xs-12 col-md-12">
    <asp:Accordion ID="AccordionInfos" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="95%">
        <Panes>

		<asp:AccordionPane ID="AccordionPane12017" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div style="text-align: center; color: #383839; font-size: x-large;">
                        2017</div>
                    (cliquer pour agrandir ou réduire)
                </Header>
                <Content>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>18/11/2017 - 12h05 : <span style="color: #8e0000;">Vieux motard que jamais</span></strong>
                        </div>
                        <div class="panel-body">
                            I'm back.
                            <br />
                            <br />
                            Des bisous !<br />
                            Christophe.
                        </div>
                    </div>
                </Content>
            </asp:AccordionPane>
		
        <asp:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div style="text-align: center; color: #383839; font-size: x-large;">
                        2016</div>
                    (cliquer pour agrandir ou réduire)
                </Header>
                <Content>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>23/11/2015 - 23h11 : <span style="color: #8e0000;">Vieux motard que jamais</span></strong>
                        </div>
                        <div class="panel-body">
                            Finalement, après 4 éditions de Noël par le site des cadeaux, on ne sait presque plus faire autrement.<br />
                            Du coup, j'ai passé un peu de temps à remettre le site en marche !<br />
                            On peut donc lancer un nouvel évènement pour ceux qui le souhaitent :-)
                            <br />
                            <br />
                            Des bisous !<br />
                            Christophe.
                        </div>
                    </div>
                </Content>
            </asp:AccordionPane>

        <asp:AccordionPane ID="pane2015" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div style="text-align: center; color: #383839; font-size: x-large;">
                        2015</div>
                    (cliquer pour agrandir ou réduire)
                </Header>
                <Content>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>31/10/2015 - 13h21 : <span style="color: #8e0000;">"I'm back"</span></strong>
                        </div>
                        <div class="panel-body">
                            Ceci n'est pas un record. Nous sommes déjà fin octobre, et c'est seulement aujourd'hui que le site revient ! 
                            <br />On a fait mieux en 2013...
                            <br />
                            <br />
                            Le record sera pour l'année prochaine.<br />
                            <br />
                            En tout cas, nous revoilà pour une fin de belle année 2015, qui a vu plein de beaux évènements se produire.<br />
                            N'oubliez pas de restaurer vos mots de passe si jamais vous ne vous en souvenez plus ! C'est <asp:HyperLink ID="OubliMotDePasse" runat="server" EnableViewState="false" NavigateUrl="~/pages/Account/ReinitPassword.aspx" >Ici</asp:HyperLink>.
                            <br />
                            <br />
                            Des bisous !<br />
                            Christophe.

                        </div>
                    </div>
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="pane2014" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div style="text-align: center; color: #383839; font-size: x-large;">
                        2014</div>
                    (cliquer pour agrandir ou réduire)
                </Header>
                <Content>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>16/11/2014 - 22h55 : <span style="color: #8e0000;">Winter is coming, Noël aussi !</span></strong>
                        </div>
                        <div class="panel-body">
                           Après un an de vacances, il est temps pour le père Sitedescadeaux.fr de reprendre du service.</br>
                           Voici donc, pour votre plus grand bonheur, le retour du sitedescadeaux.fr 2 ! Et le site fait peau neuve pour la peine. <br />
                           <br />
                           Espérons donc qu'il ne soit pas buggé de partout, car j'ai rien testé ! 
                           <br />
                           <br />
                           Comme on dit dans l'informatique : "Tester, c'est douter !"
                           <br />
                           Des bisous !<br />
                           Christophe.
                        </div>
                    </div>
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="pane2013" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div style="text-align: center; color: #383839; font-size: x-large">
                        2013</div>
                    (cliquer pour agrandir ou réduire)
                </Header>
                <Content>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>18/12/2013 - 00h35 : <span style="color: #8e0000">Ca approche.. :</span></strong>
                        </div>
                        <div class="panel-body">
                            <strong>Plus qu'une semaine à attendre pour les moins patients !<br />
                            </strong>Pour faire patienter un moment, <strong>j'ai rajouté le tableau de ce que l'on
                                doit / ce qu'on nous doit !</strong>
                            <br />
                            J'ai fait le test avec moi, et ça marche. Cependant, n'hésitez pas à me dire si
                            vous décelez des problèmes par rapport à vos listes !<br />
                            <br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>30/11/2013 - 20h30 : <span style="color: #8e0000">Responsable mais pas coupable
                                :</span></strong>
                        </div>
                        <div class="panel-body">
                            Le système des responsables est de retour ! Avec la création des différents évènements,
                            tout cela avait été désactivé. C'est désormais revenu.<br />
                            Il est donc possible de devenir responsable d'un cadeau, et consulter les résultats
                            des votes et des participations dans la page <strong>Mes responsabilités</strong>.
                            <br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>30/11/2013 - 15h15 : <span style="color: #8e0000">Modification de la participation
                                :</span></strong>
                        </div>
                        <div class="panel-body">
                            Pour faire plus simple, j'ai modifié la façon dont on participe à un cadeau. Il
                            est toujours possible de voter, mais <strong>la participation financière dépend maintenant
                                d'une liste.</strong><br />
                            Il n'est donc plus nécessaire de diviser les participations par cadeau.
                            <br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>17/11/2013 - 23h43 : <span style="color: #8e0000">Nouvelle version du site !
                                :</span></strong>
                        </div>
                        <div class="panel-body">
                            Pour permettre d'utiliser le site tout au long de l'année, ainsi qu'à tout le monde,
                            voici une mise à jour profonde du site.<br />
                            Désormais, il est possible de <strong>créer des évènements</strong>, et d'y <strong>
                                ajouter des personnes</strong> inscrites au site.<br />
                            <br />
                            Vous aurez accès à la page avec tous vos évènements, permettant d'<strong>administrer
                                la page</strong> si l'on est créateur de l'évènement, ou de <strong>consulter un évènement</strong>
                            pour voter et suggérer des idées de cadeaux.
                            <br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>06/11/2013 - 19h21 : <span style="color: #8e0000">Je m'appelle comment déjà
                                ? :</span></strong>
                        </div>
                        <div class="panel-body">
                            Ayé ! Le site grandit ! Il est désormais possible de <strong>modifier ses informations
                                personnelles</strong> !<br />
                            Nom, Prénom, E-mail, Mots de passe, ... Tout ceci n'a plus de raison d'être erroné
                            maintenant !<br />
                            <br />
                            Pour modifier vos informations personnelles, il faut <strong>se connecter</strong>,
                            puis cliquer sur <strong>Mon Compte</strong>.
                            <br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>15/10/2013 - 23h09 : <span style="color: #8e0000">Ca fait vraiment pro :</span></strong>
                        </div>
                        <div class="panel-body">
                            Prévoyant que chacun aura oublié son login//mot de passe, j'ai créé une fonctionnalité
                            de <strong>récupération de mot de passe</strong>. Il suffit pour cela de renseigner
                            son e-mail avec lequel vous vous êtes inscrit !<br />
                            Pour arriver à ce système, il suffit de cliquer sur <strong>Se connecter</strong>,
                            puis <strong>j'ai oublié mon mot de passe !</strong>.
                            <br />
                            La suite est indiquée.<br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>14/10/2013 - 19h42 : <span style="color: #8e0000">Hourra ! Hourra ! :</span></strong>
                        </div>
                        <div class="panel-body">
                            Noël, c'est déjà dans deux mois ! Après un longue hibernation (pendant les longs
                            mois d'été), et face à la quantité de requêtes (deux pour l'instant) pour savoir
                            quand j'allais relancer le site, j'ai été dans l'obligation de céder.<br />
                            Au fur et à mesure du temps, de nouvelles fonctionnalités devraient apparaître,
                            comme par exemple <strong>le listing des dettes</strong>, <strong>la création des évènements</strong>,
                            ainsi que quelques évolutions que j'espère user-friendly.<br />
                            Ne pas hésiter à me dire dans les cas où vous voyez des problèmes sur le site !<br />
                            <br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="pane2012" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                <Header>
                    <div style="text-align: center; color: #383839; font-size: x-large;">
                        2012</div>
                    (cliquer pour agrandir ou réduire)
                </Header>
                <Content>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>15/12/2012 - 15h55 : <span style="color: #8e0000">Mea culpa :</span></strong>
                        </div>
                        <div class="panel-body">
                            Face à l'affluence sur le site des cadeaux (~3-4 personnes simultanément.. Trop
                            de succès !), la base de données avait des ratés.<br />
                            J'ai corrigé le problème normalement. N'hésitez pas à me l'indiquer de nouveau si
                            jamais cela se reproduit !<br />
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>14/12/2012 - 23h35 : <span style="color: #8e0000">Oyez ! Oyez ! Avis à la popula-schtroumpf
                                :</span></strong>
                        </div>
                        <div class="panel-body">
                            Pour éviter de perdre les responsables dans les participations, il vaut mieux <b>ne
                                mettre qu'une seule participation</b> par liste de cadeau, qu'on peut éventuellement
                            passer d'un cadeau à l'autre. Pour pouvoir voter quand même, il faut alors mettre
                            une participation de 0, et mettre "oui"/"pourquoi pas"/"non".
                            <br />
                            Je vous invite à changer vos votes si vous n'aviez pas fait de cette façon !
                            <br />
                            des bisous.<br />
                            Christophe.<br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>12/12/2012 - 12h12 : <span style="color: #8e0000">Attention !</span></strong>
                        </div>
                        <div class="panel-body">
                            Il est prévu pour certains responsables d'effectuer les commandes au père noël à
                            partir de samedi. Pour les aider à ne pas faire de bêtises, prévenez-les de votre
                            participation, et faites votre liste de cadeaux !
                            <br />
                            <br />
                            <div style="text-align: center;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/pere_noel_presse.jpg" Width="250px"
                                    Height="200px" />
                            </div>
                            <br />
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>12/12/2012 - 12h12 : <span style="color: #8e0000;">Nouveau</span></strong>
                        </div>
                        <div class="panel-body">
                            Il est désormais possible d'émettre des commentaires sur une liste de cadeaux !
                            Pour ce faire, il faut cliquer sur l'image de la bulle.
                            <br />
                            A venir : la liste de ses participations, pour se souvenir ce que l'on a promis...
                            :-)
                            <br />
                        </div>
                    </div>
                </Content>
            </asp:AccordionPane>
        </Panes>
    </asp:Accordion>
    </div>
    </div>
    <div class="row" style="margin-top:50px; margin-bottom:50px; text-align:center; ">
        <div class="col-xs-12">
            <span style="font-size:larger;">Et Joyeux Noël !</span>
        </div>
    </div>
    <div style="margin:auto;" class="row">
        <div class="col-xs-12 col-md-8 col-md-offset-2">
            <asp:Image style="width:100%;" ID="imageBoulesAccueil" runat="server" ImageUrl="~/images/boules-de-noel-id258.jpg" />
        </div>
    </div>
</asp:Content>
