using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetCadeaux.pages.Events
{
    public partial class Evenements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connecte"] != null)
            {

            }
            else
            {
                Response.Redirect("~/");
            }
        }
    }
}