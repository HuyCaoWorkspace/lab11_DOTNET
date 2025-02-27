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
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            gap: 20px;
            padding: 20px;
            width: 80%;
            margin: 20px auto; 
        }
        .grid-item {
            background: #f0f0f0;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }
        .grid-item h3 {
            margin: 10px 0;
            text-align: center;
        }
        .lop-container {
            margin-top: 15px;
            border-top: 1px solid #ccc;
            padding-top: 10px;
        }
        .lop-item {
            background: #ffffff;
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        .lop-item p {
            margin: 5px 0;
        }
        .btn-view {
            background-color: #4CAF50;
            color: white;
            padding: 6px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 14px;
            margin: 5px 0;
            cursor: pointer;
            border: none;
            border-radius: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div text-align="center" >
            <h2 align="center">Danh Sách Môn Học</h2>
            <div class="grid-container">
                <asp:Repeater ID="RepeaterMonHoc" runat="server" OnItemDataBound="RepeaterMonHoc_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid-item">
                            <h3><%# Eval("TENMON") %></h3>
                            <h3>(<%# Eval("MAMON") %>)</h3>
                            <asp:HiddenField ID="hfMaMon" runat="server" Value='<%# Eval("MAMON") %>' />
                            <div class="lop-container">
                                <asp:Repeater ID="RepeaterLopHoc" runat="server">
                                    <ItemTemplate>
                                        <div class="lop-item">
                                            <p><strong>Lớp:</strong> <%# Eval("TENLOP") %></p>
                                            <p><strong>Giảng viên:</strong> <%# Eval("TENCANBO") %></p>
                                            <asp:Button runat="server" Text="Xem Sinh Viên" CssClass="btn-view"
                                                CommandArgument='<%# Eval("MAMON") + "," + Eval("MALOP") %>'
                                                OnClick="btnXemSinhVien_Click" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>