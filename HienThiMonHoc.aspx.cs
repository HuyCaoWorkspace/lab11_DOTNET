using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace lab11{
    public partial class HienThiMonHoc : System.Web.UI.Page{
        private string connectionString = ConfigurationManager.ConnectionStrings["DESKTOP-3JFU13I"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e){
            if (!IsPostBack){
                LoadMonHoc();
            }
        }

        private void LoadMonHoc(){
            using (SqlConnection conn = new SqlConnection(connectionString)){
                string query = @"SELECT MONHOC.MAMON, MONHOC.TENMON, CANBO.TENCANBO 
                                 FROM MONHOC 
                                 JOIN GIANGDAY ON MONHOC.MAMON = GIANGDAY.MAMON 
                                 JOIN CANBO ON GIANGDAY.MACB = CANBO.MACB";

                using (SqlCommand cmd = new SqlCommand(query, conn)){
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    RepeaterMonHoc.DataSource = dt;
                    RepeaterMonHoc.DataBind();
                }
            }
        }

        protected void btnXemSinhVien_Click(object sender, EventArgs e){
            Button btn = (Button)sender;
            string mamon = btn.CommandArgument;
            Response.Redirect("CapNhatDiem.aspx?mamon=" + mamon);
        }
    }
}
