<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapNhatDiem.aspx.cs" Inherits="lab11.CapNhatDiem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cap nhat diem sinh vien</title>
    <style type="text/css">
        body {
            font-family: Times New Roman, sans-serif;
        }
        .container {
            width: 100%;
            margin: 20px auto; 
            text-align: center;
        }
        .logout-container {
            display: flex;
            justify-content: flex-end;
            padding: 10px 20px;
        }
        .btn-danger {
            background-color: #D84040;
            color: white;
            padding: 6px 12px;
            font-size: 14px;
            cursor: pointer;
            border: none;
            border-radius: 4px;
        }
        .Table_DSSV {
            width: 90%;
            border-collapse: collapse; 
            margin: 20px auto;
        }

        .Table_DSSV th {
            background-color: #1D5DA7; 
            color: white;
            padding: 10px;
            text-align: center;
        }

        .Table_DSSV td {
            padding: 8px;
            text-align: center;
        }

        .btn-update{
            background-color: #4CAF50;
            color: white;
            padding: 6px 12px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-update:hover {
            background-color: #497D74;
        }
        .btn-search, .btn {
            background-color: #0056b3;
            color: white;
            padding: 8px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }
        .btn-search:hover, .btn:hover {
            background-color: #3674B5;
        }
        .search-box {
            width: 300px;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 16px;
        }
    </style>
    <script type="text/javascript">
        function confirmUpdate() {
            return confirm("Bạn có chắc chắn muốn cập nhật điểm không?");
        }
    </script>
</head>
<body>
    <form id="FormCapNhatDiem" runat="server">
        <div class="container">
            <div class="logout-container">
                <asp:Button ID="btnLogout" runat="server" Text="Đăng xuất" OnClick="btnLogout_Click" CssClass="btn-danger" />
            </div>
            <div style="font-size: x-large; text-align: center;">CẬP NHẬT ĐIỂM THI</div>
            <br />
            <asp:TextBox ID="txtMSSV" runat="server" CssClass="search-box" Placeholder="Nhập MSSV"></asp:TextBox>
            <asp:Button ID="btnLocSinhVien" CssClass="btn-search" runat="server" Text="Tìm" OnClick="btn_LocSinhVien" />
            <asp:Button ID="btnXemTatCa" CssClass="btn-search" runat="server" Text="Hiển thị tất cả" OnClick="btn_XemTatCa" />

           <asp:GridView ID="GridViewCapNhatDiem" runat="server" AllowPaging="True" PageSize="3" 
                AutoGenerateColumns="False" OnPageIndexChanging="GridViewCapNhatDiem_PageIndexChanging"
                CssClass="Table_DSSV" BorderWidth="1px" CellPadding="5" CellSpacing="0" GridLines="Both">
                <Columns>
                    <asp:BoundField DataField="MSSV" HeaderText="Mã Sinh Viên" />
                    <asp:BoundField DataField="HOVATEN" HeaderText="Họ và Tên" />
                    <asp:BoundField DataField="MAMON" HeaderText="Mã Môn" />
                    <asp:BoundField DataField="TENMON" HeaderText="Tên Môn" />
                    <asp:TemplateField HeaderText="Điểm Số">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiemSo" runat="server" Text='<%# Bind("DIEMSO") %>' Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Điểm Chữ">
                        <ItemTemplate>
                            <asp:Label ID="txtDiemChu" runat="server" ReadOnly="true" Text='<%# Bind("DIEMCHU") %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnCapNhatDiem" runat="server" CssClass="btn-update" Text="Cập Nhật" CommandArgument='<%# Eval("MSSV") + "," + Eval("MAMON") %>' OnClientClick="return confirmUpdate();" OnClick="btn_CapNhatDiem" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Làm mới trang" OnClick="btn_XemTatCa" CssClass="btn" />
            <asp:Button ID="btnDSMH" runat="server" Text="Danh Sách Môn Học" OnClick="btn_DSMH" CssClass="btn" />
        </div>
    </form>
</body>
</html>
