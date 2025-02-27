using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab11
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("login.aspx"); // Nếu chưa đăng nhập, chuyển về trang đăng nhập
            }
            else
            {
                string macb = Session["UserID"].ToString();
                LoadUserInfo(macb);
            }
        }

        private void LoadUserInfo(string macb)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT TENCANBO FROM CANBO WHERE MACB = @Macb";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Macb", macb);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        lblMacb.Text = macb;
                        lblHoten.Text = reader["TENCANBO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblWelcome.Text = "Lỗi khi tải thông tin người dùng: " + ex.Message;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("login.aspx");
        }


    }
}