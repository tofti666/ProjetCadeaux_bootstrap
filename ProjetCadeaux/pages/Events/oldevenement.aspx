<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="oldevenement.aspx.cs" Inherits="ProjetCadeaux.pages.Events.evenement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>
            Gestion des évènements
        </h2>
        <h3>
            Cette page permet de visualiser les évènements auxquels on participe, en créer de nouveaux, et administrer ceux que l'on dirige.
        </h3>
        <span class="failureNotification">
            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <span class="successNotification">
            <asp:Literal ID="SuccessText" runat="server"></asp:Literal>
        </span>

        <asp:UpdatePanel ID="updatePanelMesEvenements" runat="server" UpdateMode="Conditional" Visible="true">
            <Triggers>
                <asp:PostBackTrigger ControlID="gridViewMesEvenements" />
            </Triggers>
            <ContentTemplate>
                <p>
                    <asp:Label ID="lbl_maListe" runat="server" Text="Voici mes évènements : " />
                    <br />
                    <div align="center">
                        <asp:Button ID="bouton_refresh" class="btn btn-default" runat="server" Text="Rafraîchir la liste" />                     
                    </div>
                    <br />
                    <asp:GridView ID="gridViewMesEvenements" runat="server" Width="95%"
                        AutoGenerateColumns="false" RowStyle-Height="60" HeaderStyle-Height="40" OnRowCommand="RowCommand_click" DataKeyNames="id_evenement">
                        <Columns>
                            <asp:BoundField DataField="id_evenement" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="libelle" HeaderText="Evenement"/>
                            <asp:BoundField DataField="dateButoir" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date butoir" HeaderStyle-Width="10%"/>
                            <asp:BoundField DataField="dateEvenement" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date de l'évènement" HeaderStyle-Width="15%" />
                            <asp:BoundField DataField="id_admin" HeaderText="ID_admin" Visible="false"/>
                            <asp:ButtonField HeaderText="Consulter" Text="Consulter" CommandName="voir" HeaderStyle-Width="10%" />
                        </Columns>
                    </asp:GridView>
                    <br />
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lbl_creerEvt" runat="server" Text="Créer un évènement :" />
        <asp:Button ID="btn_creerEvt" class="btn btn-default" runat="server" Text="Créer un évènement" OnClick="btnCreerEvt_Click"/>
                

    </div>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
