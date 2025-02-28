using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab11
{
    public partial class CapNhatDiem : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MACB"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    string mamon = Request.QueryString["mamon"];
                    string malop = Request.QueryString["malop"];
                    LoadData("", mamon, malop);
                }
            }
        }

        private void LoadData(string mssv = "", string mamon = "", string malop = "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["DESKTOP-1UFQCRO"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT SV.MSSV, SV.HOVATEN, HM.MAMON, MH.TENMON, HM.DIEMSO, HM.DIEMCHU 
                            FROM HOCMON HM 
                            INNER JOIN SINHVIEN SV ON HM.MSSV = SV.MSSV 
                            INNER JOIN MONHOC MH ON HM.MAMON = MH.MAMON 
                            WHERE (@MSSV = '' OR HM.MSSV = @MSSV) 
                            AND (@MAMON = '' OR HM.MAMON = @MAMON)
                            AND (@MALOP = '' OR SV.MALOP = @MALOP)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MSSV", mssv);
                cmd.Parameters.AddWithValue("@MAMON", mamon);
                cmd.Parameters.AddWithValue("@MALOP", malop);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridViewCapNhatDiem.DataSource = dt;
                GridViewCapNhatDiem.DataBind();
            }
        }

        protected void btn_XemTatCa(object sender, EventArgs e)
        {
            txtMSSV.Text = "";
            string mamon = Request.QueryString["mamon"];
            string malop = Request.QueryString["malop"];
            LoadData("", mamon, malop);
        }

        protected void btn_LocSinhVien(object sender, EventArgs e)
        {
            string mssv = txtMSSV.Text.Trim();
            string mamon = Request.QueryString["mamon"];
            string malop = Request.QueryString["malop"];
            LoadData(mssv, mamon, malop);
        }

        private string ConvertDiemSoToDiemChu(decimal diemSo)
        {
            if (diemSo >= 9.0m) return "A";
            if (diemSo >= 8.0m) return "B+";
            if (diemSo >= 7.0m) return "B";
            if (diemSo >= 6.0m) return "C+";
            if (diemSo >= 5.0m) return "C";
            if (diemSo >= 4.5m) return "D+";
            if (diemSo >= 4.0m) return "D";
            return "F";
        }

        protected void btn_CapNhatDiem(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');
            string mssv = args[0];
            string mamon = args[1];

            GridViewRow row = (GridViewRow)btn.NamingContainer;
            TextBox txtDiemSo = (TextBox)row.FindControl("txtDiemSo");

            if (decimal.TryParse(txtDiemSo.Text.Trim(), out decimal diemSo))
            {

                if (diemSo < 0 || diemSo > 10)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Điểm số phải từ 0 đến 10!');", true);
                    txtDiemSo.Text = "";
                    return;
                }

                string diemChu = ConvertDiemSoToDiemChu(diemSo);
                string connStr = ConfigurationManager.ConnectionStrings["DESKTOP-3JFU13I"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "UPDATE HOCMON SET DIEMSO = @DiemSo, DIEMCHU = @DiemChu WHERE MSSV = @MSSV AND MAMON = @MAMON";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DiemSo", diemSo);
                    cmd.Parameters.AddWithValue("@DiemChu", diemChu);
                    cmd.Parameters.AddWithValue("@MSSV", mssv);
                    cmd.Parameters.AddWithValue("@MAMON", mamon);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật điểm thành công!');", true);

                string mamonQuery = Request.QueryString["mamon"];
                string malopQuery = Request.QueryString["malop"];

                if (txtMSSV.Text == mssv)
                {
                    LoadData(mssv, mamonQuery, malopQuery);
                }
                else
                {
                    LoadData("", mamonQuery, malopQuery);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập số hợp lệ!');", true);
                txtDiemSo.Text = "";
            }
        }

        protected void btn_DSMH(object sender, EventArgs e)
        {
            Response.Redirect("HienthiMonHoc.aspx");
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}