using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Exer1
{
	public partial class LoginForm : System.Web.UI.Page
	{
		private readonly LoginRepo repo = new LoginRepo();

		protected void Page_Load(object sender, EventArgs e)
		{
			Label1.Text = "";
			Label1.ForeColor = System.Drawing.Color.Red;
		}

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
			string input = inputtxtBox.Text;
			string pwd = txtPwd.Text;
			

			if(input != string.Empty && pwd != string.Empty)
			{
				if (repo.isEmail(input))
				{
					if(repo.authenticateEmail(input, pwd))
					{
						// go to mainform
						Response.BufferOutput = true;
						Response.Redirect("/MainForm.aspx");
						
					}else
					{
						Label1.Text = "Incorrect Username or Password!";
					}
				}
				else
				{
					if(repo.authenticateUsername(input, pwd))
					{
                        // go to mainform
                        Response.BufferOutput = true;
                        Response.Redirect("/MainForm.aspx");
                    }
					else
					{
                        Label1.Text = "Incorrect Username or Password!";
                    }
				}
			}
			Label1.Text = "Username or Password cannot be empty!";
        }
    }
}