<%@ Page Title="Bilan" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="Participations.aspx.cs" Inherits="ProjetCadeaux.pages.Participations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 100%;">
        <h2>
            Tableau de bord
        </h2>
        <p>
            C'est ici qu'on peut voir, pour un évènement, l'ensemble de nos participations,
            et des participations attendues.
            <br />
            Choisissez un évènement, puis accédez aux tableau récapitulatif des comptes. <br />(Attention, la page est un peu longue à charger.. Je l'optimiserai plus tard)
        </p>
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
        <br />
        <asp:DropDownList ID="ddl_listeEvenements" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_listeEvenements_Change" />
        <br />
        <br />
        <asp:UpdatePanel ID="updatePanel_recapitulatif" runat="server" Visible="false">
            <ContentTemplate>
                <hr />
                <h2>
                    Tableau de bord
                </h2>
                <p>
                    Voici l'ensemble des transactions financières qui vous concernent pour cet évènement :
                </p>
                <asp:GridView ID="gridView_listeDettes" RowStyle-Height="60" HeaderStyle-Height="40" runat="server" AutoGenerateColumns="false"
                    Width="50%">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Id" Visible="false" />
                        <asp:BoundField DataField="personne" HeaderText="Personne" HeaderStyle-Width="50%" />
                        <asp:BoundField DataField="jedois" HeaderText="Je lui dois" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="red" ItemStyle-Font-Bold="true"/>
                        <asp:BoundField DataField="onmedoit" HeaderText="Il me doit" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true"/>
                    </Columns>
                    <EmptyDataTemplate>
                        Il n'y a aucune participation, ni aucune dette pour l'instant
                    </EmptyDataTemplate>
                </asp:GridView>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
</asp:Content>
