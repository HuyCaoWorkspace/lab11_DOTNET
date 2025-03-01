using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace lab11
{
    public partial class HienThiMonHoc : System.Web.UI.Page
    {
<<<<<<< HEAD
        private string connectionString = ConfigurationManager.ConnectionStrings["DBC"].ConnectionString;
=======
>>>>>>> c455aaa9598e042b9e55b6da3e6828f9d0319387

        private string connectionString = ConfigurationManager.ConnectionStrings["DBC"].ConnectionString;
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
                    LoadMonHocVaLopHoc(macb);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            string macb = Session["MACB"]?.ToString();
            LoadMonHocVaLopHoc(macb, searchQuery);
        }

        protected void GridViewMonHoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMonHoc.PageIndex = e.NewPageIndex; 
            string macb = Session["MACB"]?.ToString();
            LoadMonHocVaLopHoc(macb);
        }


        private void LoadMonHocVaLopHoc(string macb, string searchQuery = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT M.MAMON, M.TENMON, L.MALOP, L.TENLOP, C.TENCANBO
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

                    GridViewMonHoc.DataSource = dt;
                    GridViewMonHoc.DataBind();
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
