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
                string query = @"SELECT DISTINCT M.MAMON, M.TENMON 
                                FROM MONHOC M 
                                JOIN GIANGDAY G ON M.MAMON = G.MAMON";

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

        protected void RepeaterMonHoc_ItemDataBound(object sender, RepeaterItemEventArgs e){
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem){
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string maMon = drv["MAMON"].ToString();
                Repeater rptLopHoc = (Repeater)e.Item.FindControl("RepeaterLopHoc");

                LoadLopHoc(maMon, rptLopHoc);
            }
        }

        private void LoadLopHoc(string maMon, Repeater rptLopHoc){
            using (SqlConnection conn = new SqlConnection(connectionString)){
                string query = @"SELECT L.MALOP, L.TENLOP, C.TENCANBO, @MAMON AS MAMON
                                FROM LOPHOC L 
                                JOIN GIANGDAY G ON L.MALOP = G.MALOP 
                                JOIN CANBO C ON G.MACB = C.MACB 
                                WHERE G.MAMON = @MAMON";

                using (SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@MAMON", maMon);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptLopHoc.DataSource = dt;
                    rptLopHoc.DataBind();
                }
            }
        }

        protected void btnXemSinhVien_Click(object sender, EventArgs e){
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');
            string maMon = args[0];
            string maLop = args[1];
            Response.Redirect($"CapNhatDiem.aspx?mamon={maMon}&malop={maLop}");
        }
    }
}