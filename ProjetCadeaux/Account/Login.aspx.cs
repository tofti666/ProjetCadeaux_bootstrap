using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetCadeaux_Connection;
using System.Data;
using ProjetCadeaux_BLL;

namespace ProjetCadeaux.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void VerifierConnection_Click(object sender, EventArgs e)
        {
            Compte compte = new Compte();

            if (compte.getPassword(UserName.Text, HashMd5.getMd5Hash(Password.Text)))
            {
                DataTable dt = compte.getInformationsPersonne(UserName.Text);

                Session["connecte"] = "true";
                Session["personnePrenom"] = dt.Rows[0].ItemArray.GetValue(1).ToString();
                Session["personneNom"] = dt.Rows[0].ItemArray.GetValue(0).ToString();
                Session["personneEmail"] = dt.Rows[0].ItemArray.GetValue(5).ToString();
                Session["personneLogin"] = dt.Rows[0].ItemArray.GetValue(2).ToString();
                Session["personneID"] = dt.Rows[0].ItemArray.GetValue(4).ToString();

                Response.Redirect("~/");
            }
            else
            {
                FailureText.Text = "L'identification a échoué";
            }

        }
    }
}
