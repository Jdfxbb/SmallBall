using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SmallBall
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void ConfirmRegister_Click(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;
            try
            {
                SqlCommand command = new SqlCommand();
                SqlConnection sql = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Joshua\\source\\repos\\SmallBall\\SmallBall\\SmallBall\\App_Data\\SmallBallDB.mdf;Integrated Security=True;Connect Timeout=30");

                sql.Open();
                string s = /*"DECLARE @responseMessage NVARCHAR(250) " +  */"EXEC AddUser @Id='" + UserName.Text + "',@Password='" + Password.Text + "',@Email='" + Email.Text + "'"/*"',@responseMessage=@responseMessage OUTPUT"*/;
                command = new SqlCommand(s, sql);
                command.ExecuteNonQuery();
                sql.Close();
                Server.Transfer("MainMenu.aspx");
            }
            catch (SqlException exception)
            {
                ErrorLabel.Visible = true;
                switch (exception.Number)
                {
                    case 2627: ErrorLabel.Text = "Username already exists"; break;
                }
            }

    //            DECLARE @responseMessage NVARCHAR(250)

                //exec AddUser

                //    @Id = 'Admin',
                //	@Password = 'Admin',
                //	@Email = 'Admin@Admin.Admin',
                //	@responseMessage = @responseMessage OUTPUT


        }
    }
}