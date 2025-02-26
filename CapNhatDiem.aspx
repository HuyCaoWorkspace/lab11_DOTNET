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
            width: 70%;
            margin: 20px auto; 
            text-align: center;
        }

        .auto-style1 {
            width: 100%;
            border-collapse: collapse; 
            margin-top: 20px;
        }

        .auto-style1 th {
            background-color: #1D5DA7; 
            color: white;
            padding: 10px;
            text-align: center;
        }

        .auto-style1 td {
            padding: 8px;
            text-align: center;
        }

        .btn-update{
            background-color: #295F98; 
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 4px;
        }

        .btn-update:hover {
            background-color: #4D779B;
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
            <div style="font-size: x-large; text-align: center;">CẬP NHẬT ĐIỂM THI</div>
            <br />
            <asp:TextBox ID="txtMSSV" runat="server" Placeholder="Nhập MSSV"></asp:TextBox>
            <asp:Button ID="btnLocSinhVien" CssClass="btn-update" runat="server" Text="Lọc" OnClick="btn_LocSinhVien" />
            <asp:Button ID="btnXemTatCa" CssClass="btn-update" runat="server" Text="Hiển thị tất cả" OnClick="btn_XemTatCa" />

            <asp:GridView ID="GridViewCapNhatDiem" runat="server" AutoGenerateColumns="False" CssClass="auto-style1" BorderWidth="1px" CellPadding="5" CellSpacing="0" GridLines="Both">
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
                            <asp:Button ID="btnCapNhatDiem" runat="server" CssClass="btn-updates" Text="Cập Nhật" CommandArgument='<%# Eval("MSSV") + "," + Eval("MAMON") %>' OnClientClick="return confirmUpdate();" OnClick="btn_CapNhatDiem" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
