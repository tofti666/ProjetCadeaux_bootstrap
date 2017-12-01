<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contentEvent.aspx.cs" Inherits="ProjetCadeaux.pages.Events.contentEvent" %>

<html>
    <head>
        <script type="text/javascript" src="../../Scripts/jquery-te-1.4.0.min.js" charset="utf-8"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="../../Styles/jquery-te-1.4.0.css" />
          <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
          <link rel="stylesheet" href="/resources/demos/style.css">
          <style>
            label, input { display:block; }
            input.text { margin-bottom:12px; width:95%; padding: .4em; }
            fieldset { padding:0; border:0; margin-top:25px; }
            h1 { font-size: 1.2em; margin: .6em 0; }
            div#users-contain { width: 350px; margin: 20px 0; }
            div#users-contain table { margin: 1em 0; border-collapse: collapse; width: 100%; }
            div#users-contain table td, div#users-contain table th { border: 1px solid #eee; padding: .6em 10px; text-align: left; }
            .ui-dialog .ui-state-error { padding: .3em; }
            .validateTips { border: 1px solid transparent; padding: 0.3em; }
          </style>

        <script type="text/javascript" language="javascript">
            function btnEnregistrerCommentaire_Click() {
                $.ajax({
                    url: "contentEvent.aspx?sauvercommentaire=true",
                    type: "POST",
                    data: $('#form1').serialize(),
                    success:
                function (retour) {
                    $('#contentListe').html(retour); // rafraichi toute ta DIV "bien sur il lui faut un id "
                }
                });
            }
        </script>
        <script type="text/javascript" language="javascript">
            function show_hide_comment_panel() {
                $("#div_commentaires").toggle("fast");
                if ($("#reducebutton").attr('value') == "(afficher)") {
                    $("#reducebutton").attr('value','(masquer)');
                } else {
                    $("#reducebutton").attr('value', '(afficher)');
                }

                
            }
        </script>
        <script type="text/javascript" language="javascript">
              $(function () {
                  var dialog, form,

              name = $("#name"),
              email = $("#email"),
              password = $("#password"),
              allFields = $([]).add(name).add(email).add(password),
              tips = $(".validateTips");

                  function updateTips(t) {
                      tips
                .text(t)
                .addClass("ui-state-highlight");
                      setTimeout(function () {
                          tips.removeClass("ui-state-highlight", 1500);
                      }, 500);
                  }

                  function checkLength(o, n, min, max) {
                      if (o.val().length > max || o.val().length < min) {
                          o.addClass("ui-state-error");
                          updateTips("Length of " + n + " must be between " +
                  min + " and " + max + ".");
                          return false;
                      } else {
                          return true;
                      }
                  }


                  function addUser() {
                      var valid = true;



                      if (valid) {

                          dialog.dialog("close");
                      }
                      return valid;
                  }

                  dialog = $("#dialog-form").dialog({
                      autoOpen: false,
                      height: 400,
                      width: 350,
                      modal: true,
                      buttons: {
                          "Ajouter le cadeau": addUser,
                          Cancel: function () {
                              dialog.dialog("close");
                          }
                      },
                      close: function () {
                          form[0].reset();
                          allFields.removeClass("ui-state-error");
                      }
                  });

                  form = dialog.find("#formAjoutCadeau").on("submit", function (event) {
                      event.preventDefault();
                      addUser();
                  });

                  $("#btnAjoutCadeau").button().on("click", function () {
                      dialog.dialog("open");
                  });
              });
          </script>
    </head>

    <body>

        <form id="form1">
            <h3>Voir la liste de <strong><%=participantListe.nom_participant%></strong></h3>

            <!-- Afficher un bouton pour devenir responsable si la personne n'en a pas -->

            <!-- Afficher la participation et le bouton pour participer à la liste -->
            <div class="row" style="margin-top: 20px; font-size: 0.9em; ">
                <div class="col-xs-6 col-md-4">
                    ( Ma participation à cette liste est de :
                </div>
                <div class="col-xs-6 col-md-4">
                    <% if (participation != null && participation.participation != 0)
                       { %>
                        <%= participation.participation%> €
                    <% }
                       else
                       { %>
                    <p style="color:red; float:left;">0 €</p>
                    <% } %>&nbsp;)
                </div>
                <div class="col-xs-12 col-md-4" >
                    <asp:HyperLink style="color:White;" CssClass="btn btn-primary" ID="HyperLink1" runat="server" Text="Modifier ma participation"
                        NavigateUrl="" Visible="true" />
                </div>
            </div>

            <!-- Afficher la liste des cadeaux de la personne -->
            <div class="row" style="margin-top:0px;">
                <h2>Idées cadeaux</h2>
            </div>
        
            <div class="row" style="margin-bottom:5px; margin-top: 10px; background-color:#eeeeee">
                <div class="col-xs-3" style=""> 
                    <strong>Titre cadeau</strong>
                </div>
                <div class="col-xs-5" style=""> 
                    <strong>Description</strong>
                </div>
                <div class="col-xs-1" style=""> 
                    <strong>Prix</strong>
                </div>
                <div class="col-xs-1" style=""> 
                    <strong>Date d'ajout</strong>
                </div>
                <div class="col-xs-2" style=""> 
                    <strong>Proposé par</strong>
                </div>
            </div>
                <% if (listeCadeaux != null)
                   {
                       foreach (ProjetCadeaux_Entites.IdeeCadeauPourListe idee in listeCadeaux)
                       { %>
                       <div class="row"  style="margin-bottom:5px; background-color:#eeeeee" >
                            <div class="col-xs-0" style="display:none;"> 
                                <%=  idee.cadeau.id_cadeau%>
                            </div>
                            <div class="col-xs-3" style=""> 
                                <%= idee.cadeau.intitule_cadeau%>
                            </div>
                            <div class="col-xs-5" style=""> 
                                <%= idee.cadeau.description%>
                            </div>
                            <div class="col-xs-1" style=""> 
                                <%= idee.cadeau.prix%>
                            </div>
                            <div class="col-xs-1" style=""> 
                                <%= idee.dateAjoutIdeeCadeau.ToShortDateString()%>
                            </div>
                            <div class="col-xs-2" style=""> 
                                <%= idee.proposePar.prenom + " " + idee.proposePar.nom%>
                            </div>
                        </div>
                <%      }
                   } %>
       
            <div class="row" style=" margin-top:10px; padding-left:30px;">
                <a style="color:White;" class="btn btn-primary" id="btnAjoutCadeau" >Ajouter une idée de cadeau</a>
            </div>

            <div id="dialog-form" title="Create new user">
                <p class="validateTips">All form fields are required.</p>
 
                <form id="formAjoutCadeau">
                    <fieldset>
                        <label for="name">Name</label>
                        <input type="text" name="name" id="name" value="Jane Smith" class="text ui-widget-content ui-corner-all">
                        <label for="email">Email</label>
                        <input type="text" name="email" id="email" value="jane@smith.com" class="text ui-widget-content ui-corner-all">
                        <label for="password">Password</label>
                        <input type="password" name="password" id="password" value="xxxxxxx" class="text ui-widget-content ui-corner-all">
 
                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                        <input type="submit" tabindex="-1" style="position:absolute; top:-1000px">
                    </fieldset>
                </form>
            </div>


            <div class="row" style="margin-top:20px;">
                <h2 class="col-xs-3">Commentaires (<%= listeCommentaires.Count %>)</h2>
                <input data-toggle="tooltip" value="(afficher)" title="Afficher / Réduire les commentaires" style="margin-top:20px; border:0px; color:Gray;" class="col-xs-2" id="reducebutton" onclick="show_hide_comment_panel();" ></input>
            </div>
            <!-- Afficher les commentaires -->
            <div id="div_commentaires" class="row" style="margin-top:10px; display:none;">

                <% foreach (ProjetCadeaux_Entites.Commentaire com in listeCommentaires)
                   { %>
                   <% if (com.id_auteur == personneConnectee.id_personne)
                      { %>
                          <div class="row">
                                <div class="col-xs-offset-2 col-md-offset-0 col-xs-10 col-md-8" style="background-color:#dddddd; min-height:5em; margin-bottom:10px;">
                                    <%if (!com.deleted)
                                      { %>
                                        <%= com.commentaire%>
                                    <% }
                                      else
                                      { %>
                                      <p style="font-style:italic"><strong>commentaire effacé</strong></p>
                                    <% } %>
                               </div>
                               <div class="col-xs-0 col-md-1" style="text-align:right; margin-right:-15px; ">
                                    <img src="../../images/coin_gris_fleche_reversed.png" style="width:20px;" />
                               </div>
                               <div class="col-xs-12 col-md-3" style="background-color:#dddddd;">
                                    <%= com.ecrit_par%><br />
                                    créé le <%= com.date_creation%>
                                    <%if (com.date_modification != null && com.date_modification.ToShortDateString() != "01/01/0001")
                                      { %>
                                      <p style="font-style:italic;">modifié le <%= com.date_modification%></p>
                                    <%} %>
                               </div>
                           </div>
                   <% }
                      else
                      { %>
                          <div class="row">
                               <div class="col-xs-12 col-md-3" style="background-color:#dddddd;">
                                    <%= com.ecrit_par%><br />
                                    créé le <%= com.date_creation%>
                                    <%if (com.date_modification != null && com.date_modification.ToShortDateString() != "01/01/0001")
                                      { %>
                                      <p style="font-style:italic;">modifié le <%= com.date_modification%></p>
                                    <%} %>
                               </div>
                               <div class="col-xs-0 col-md-1" style="margin-left:-15px;">
                                    <img src="../../images/coin_gris_fleche_2.png" style="width:20px;" />
                               </div>
                               <div class="col-xs-offset-2 col-md-offset-0 col-xs-10 col-md-8" style="background-color:#dddddd; min-height:5em; margin-bottom:10px;">
                                    <%if (!com.deleted)
                                      { %>
                                        <%= com.commentaire%>
                                    <% }
                                      else
                                      { %>
                                      <p style="font-style:italic"><strong>commentaire effacé</strong></p>
                                    <% } %>
                               </div>
                           </div>
                   <%} %>
        
                <% } %>
            </div>

            <!-- Afficher un bouton pour commenter -->
            <div class="row" style="margin-top:5px;">
                <h2>Ajouter un commentaire</h2>
            </div>

            <div class="row" style="margin-top:5px;">
            
                <div class="col-xs-12">
                    <textarea id="commentaire" name="commentaire" class="jqte-test" rows="5" style="width:100%;"></textarea>
                </div>
            
                <div class="col-xs-12" style="margin-top:5px;">
                    <div id="idCommentaireModifie" style="display:none; "></div>
                    <a id="btnEnregistrerCommentaire" class="btn btn-primary" onclick="btnEnregistrerCommentaire_Click();" >Enregistrer</a>
                </div>
            </div>
        </form>
    </body>
</html>