using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace lab11
{
    public partial class HienThiMonHoc : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DESKTOP-1UFQCRO"].ConnectionString;
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
                    string macb = Session["MACB"].ToString();
                    LoadMonHoc(macb);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            string macb = Session["MACB"]?.ToString();
            LoadMonHoc(macb, searchQuery);
        }

        private void LoadMonHoc(string macb, string searchQuery = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT DISTINCT M.MAMON, M.TENMON, G.MACB, L.MALOP, L.TENLOP, C.TENCANBO
                         FROM MONHOC M
                         JOIN GIANGDAY G ON M.MAMON = G.MAMON
                         JOIN LOPHOC L ON G.MALOP = L.MALOP
                         JOIN CANBO C ON G.MACB = C.MACB
                         WHERE G.MACB = @MACB";

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query += " AND (M.MAMON LIKE @Search OR L.TENLOP LIKE @Search)";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MACB", macb);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                    }

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    RepeaterMonHoc.DataSource = dt;
                    RepeaterMonHoc.DataBind();
                }
            }
        }

        protected void RepeaterMonHoc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string maMon = drv["MAMON"].ToString();
                string macb = Session["MACB"]?.ToString();

                Repeater rptLopHoc = (Repeater)e.Item.FindControl("RepeaterLopHoc");
                LoadLopHoc(maMon, macb, rptLopHoc);
            }
        }

        private void LoadLopHoc(string maMon, string macb, Repeater rptLopHoc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT L.MALOP, L.TENLOP, C.TENCANBO, @MAMON AS MAMON, M.TENMON
                         FROM LOPHOC L 
                         JOIN GIANGDAY G ON L.MALOP = G.MALOP 
                         JOIN CANBO C ON G.MACB = C.MACB 
                         JOIN MONHOC M ON G.MAMON = M.MAMON
                         WHERE G.MAMON = @MAMON AND G.MACB = @MACB";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MAMON", maMon);
                    cmd.Parameters.AddWithValue("@MACB", macb);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptLopHoc.DataSource = dt;
                    rptLopHoc.DataBind();
                }
            }
        }

        protected void btnXemSinhVien_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');
            string maMon = args[0];
            string maLop = args[1];
            Response.Redirect($"CapNhatDiem.aspx?mamon={maMon}&malop={maLop}");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

    }
}