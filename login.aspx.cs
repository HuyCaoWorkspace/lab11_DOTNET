using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace lab11
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            if (Session["UserID"] != null)
            {
                Response.Redirect("home.aspx");

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Xác thực người dùng
            if (ValidateUser(txtMacb.Text, txtPassword.Text))
            {
                // Lưu thông tin người dùng vào Session
                Session["UserID"] = txtMacb.Text;

                // Hiển thị alert và chuyển hướng sang home.aspx
                string script = "alert('Đăng nhập thành công!'); window.location='home.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "LoginSuccess", script, true);

                // Chuyển hướng đến trang chính sau khi đăng nhập thành công
                //Response.Redirect("home.aspx");
            }
            else
            {
                // Hiển thị thông báo lỗi
                lblErrorMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
            }
        }

        private bool ValidateUser(string macb, string password)
        {
            try
            {
                // Băm mật khẩu người dùng nhập vào
                string hashedInputPassword = HashPasswordSHA256(password);

                System.Diagnostics.Debug.WriteLine($"Macb: {macb}");
                System.Diagnostics.Debug.WriteLine($"Input Password Hash: {hashedInputPassword}");

                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT MATKHAU FROM CANBO WHERE MACB = @Macb";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Macb", macb);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        connection.Close();

                        if (result != null && result != DBNull.Value)
                        {
                            if (result is byte[] storedHashBytes)  // Kiểm tra nếu dữ liệu trả về là byte[]
                            {
                                // Chuyển byte[] về chuỗi HEX
                                string storedHashedPassword = "0x" + BitConverter.ToString(storedHashBytes).Replace("-", "").ToUpper();
                                System.Diagnostics.Debug.WriteLine($"Stored Password Hash: {storedHashedPassword}");

                                // So sánh mật khẩu không phân biệt hoa thường
                                bool matches = storedHashedPassword.Equals(hashedInputPassword, StringComparison.OrdinalIgnoreCase);
                                System.Diagnostics.Debug.WriteLine($"Passwords match: {matches}");

                                return matches;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Stored password is not in byte[] format!");
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("No user found with this username");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Login error: " + ex.Message);
                lblErrorMessage.Text = "Lỗi hệ thống: " + ex.Message;
            }

            return false;
        }


        private string HashPasswordSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                return "0x" + BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
            }
        }

    }
}