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
              function enregistrerParticipation() {
                  $.ajax({
                      url: "contentEvent.aspx?sauverparticipation=true",
                      type: "POST",
                      data: $('#form_participation').serialize(),
                      success:
                function (retour) {
                    $('#contentListe').html(retour); // rafraichi toute ta DIV "bien sur il lui faut un id "
                }
                  });
              }
          </script>
           <script type="text/javascript" language="javascript">
                      function enregistrerParticipation() {
                          $.ajax({
                              url: "contentEvent.aspx?sauveridee=true",
                              type: "POST",
                              data: $('#formAjoutCadeau').serialize(),
                              success:
                function (retour) {
                    $('#contentListe').html(retour); // rafraichi toute ta DIV "bien sur il lui faut un id "
                }
                          });
                      }
          </script>
    </head>

    <body>

        <form id="form1">
            <h3>Voir la liste de <strong><%=participantListe.nom_participant%></strong></h3>&nbsp;&nbsp;&nbsp;(Il n'y a pas de responsable pour cette liste !) ou (Machin est responsable)

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
                        NavigateUrl="" Visible="true" onclick="$('#div_participation_outside').show();" />
                </div>

                <div id="div_participation_outside" class="modalPopupCustomOutside" >
                    <div id="div_participation_inside" title="Modifier ma participation" class="modalPopupCustomInside">
                        <form id="form_participation">
                            <fieldset>
                                <div class="row" style="margin-left:25px;">
                                    <div class="row" style="margin-left:0px;">
                                        <label class="col-xs-12"><h2>Modifier ma participation à la liste</h2></label>
                                        <label for="name" class="col-xs-12" style="margin-top:30px;">Participation</label>
                                        <input type="text" name="participation" id="participation" value="" placeholder="participation à la liste" 
                                            style="width:80%;" class="form-control ui-widget-content ui-corner-all col-xs-12" /> 
                                    </div>
                                    <br />
                                    <div class="row">
                                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                                        <input class="btn btn-success col-xs-offset-3 col-xs-3" value="Enregistrer" onclick="$('#div_participation_outside').hide();" />
                                        <input class="btn btn-default col-xs-3" value="Annuler" onclick="$('#div_participation_outside').hide();" />
                                    </div>
                                </div>
                            </fieldset>
                        </form>
                    </div>
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
                <a style="color:White;" class="btn btn-primary" id="btnAjoutCadeau" onclick="$('#modal_pop_up').show();" >Ajouter une idée de cadeau</a>
            </div>

            <div id="modal_pop_up" class="modalPopupCustomOutside" >
                <div id="dialog-form" title="Create new user" class="modalPopupCustomInside">
                    <form id="formAjoutCadeau">
                        <fieldset>
                            <div class="row" style="margin-left:25px;">
                                <div class="row" style="margin-left:0px;">
                                    <label class="col-xs-12"><h2>Ajouter une idée de cadeau</h2></label>
                                    <label for="name" class="col-xs-12" style="margin-top:30px;">Titre</label>
                                    <input type="text" name="titre" id="titre" value="" placeholder="titre du cadeau" 
                                            style="width:80%;" class="form-control ui-widget-content ui-corner-all col-xs-12" />
                                    <label for="email" class="col-xs-12">Description</label>
                                    <textarea rows="5" name="description" id="description" 
                                            placeholder="description du cadeau" style="width:80%;" class="form-control ui-widget-content ui-corner-all col-xs-12" />
                                    <label for="password" class="col-xs-12">Prix</label>
                                    <input type="text" name="prix" id="prix" value="" placeholder="ordre d'idée du prix" maxlength="10" 
                                            style="width:80%;" class="form-control ui-widget-content ui-corner-all col-xs-12" />
                                </div>
                                <br />
                                <div class="row">
                                    <!-- Allow form submission with keyboard without duplicating the dialog button -->
                                    <input class="btn btn-success col-xs-offset-3 col-xs-3" value="Enregistrer" onclick="$('#modal_pop_up').hide();" />
                                    <input class="btn btn-default col-xs-3" value="Annuler" onclick="$('#modal_pop_up').hide();" />
                                </div>
                            </div>
                        </fieldset>
                    </form>
                </div>
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
                    <textarea id="commentaire" name="commentaire" class="jqte-test" rows="5" style="width:100%; border:1px solid grey; border-radius:5px;"></textarea>
                </div>
            
                <div class="col-xs-12" style="margin-top:5px;">
                    <div id="idCommentaireModifie" style="display:none; "></div>
                    <a id="btnEnregistrerCommentaire" class="btn btn-primary" onclick="btnEnregistrerCommentaire_Click();" >Enregistrer</a>
                </div>
            </div>
        </form>
    </body>
</html>