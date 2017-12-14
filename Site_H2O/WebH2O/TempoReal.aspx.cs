using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebH2O
{
	public partial class TempoReal : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HttpCookie cookie = Request.Cookies["login"];
			if (cookie == null || cookie.Value != "ok")
			{
				Response.Redirect("default.aspx");
				return;
			}
		}

		protected void btnLogout_Click(object sender, EventArgs e)
		{
			HttpCookie cookie = new HttpCookie("login");
			cookie.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(cookie);
			Response.Redirect("default.aspx");
		}
	}
}