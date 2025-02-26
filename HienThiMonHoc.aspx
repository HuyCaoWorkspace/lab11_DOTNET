<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HienThiMonHoc.aspx.cs" Inherits="lab11.HienThiMonHoc" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hiển Thị Môn Học</title>
    <style>
        body {
            font-family: Times New Roman, sans-serif;
        }
        .grid-container {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
            gap: 20px;
            padding: 20px;
        }
        .grid-item {
            background: #f0f0f0;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            text-align: center;
        }
        .grid-item h3 {
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2 align="center">Danh Sách Môn Học</h2>
            <div class="grid-container">
                <asp:Repeater ID="RepeaterMonHoc" runat="server">
                    <ItemTemplate>
                        <div class="grid-item">
                            <h3><%# Eval("TENMON") %></h3>
                            <p>Giảng viên: <%# Eval("TENCANBO") %></p>
                            <asp:Button runat="server" Text="Xem Sinh Viên"
                                CommandArgument='<%# Eval("MAMON") %>'
                                OnClick="btnXemSinhVien_Click" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
