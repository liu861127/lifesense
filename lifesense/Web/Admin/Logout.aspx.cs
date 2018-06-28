using System;
using System.Web.Security;
namespace Maticsoft.Web.Admin
{
	/// <summary>
	/// Logout 的摘要说明。
	/// </summary>
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            Session.Abandon();
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("Login.aspx");
				
		}
	}
}
