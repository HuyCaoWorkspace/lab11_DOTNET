using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab11.Components
{
    public partial class login : System.Web.UI.Page
    {
        private string GetSHA256Hash(string input)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return "0x" + BitConverter.ToString(hash).Replace("-", ""); // Convert to SQL Server HEX format
            }
        }

        protected void btn_login(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string loginQuery = @"SELECT CONVERT(NVARCHAR(MAX), MATKHAU, 1) FROM CANBO WHERE MACB = @MACB";

                    SqlCommand cmd = new SqlCommand(loginQuery, conn);
                    cmd.Parameters.AddWithValue("@MACB", maCB.Text);

                    object result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != null)
                    {
                        string inputPassword = matkhau.Text;
                        string hashedInput = GetSHA256Hash(inputPassword);
                        string storedHash = result.ToString();

                        if (storedHash == hashedInput)
                        {
                            Session["MACB"] = maCB.Text;

                            Response.Redirect("HienThiMonHoc.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('Đăng nhập thất bại! Kiểm tra lại thông tin.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Không tìm thấy tài khoản!');</script>");
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Đã xảy ra lỗi! Vui lòng thử lại.');</script>");
                }
            }
        }


        protected void btn_cancel(object sender, EventArgs e)
        {
            maCB.Text = "";
            matkhau.Text = "";
        }

    }
}
