<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HienThiMonHoc.aspx.cs" Inherits="lab11.HienThiMonHoc" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hiển Thị Môn Học</title>
    <style>
        body {
            font-family: Times New Roman, sans-serif;
        }

         .header-container {
            padding: 10px 20px;
        }

        .user-info {
            font-weight: bold;
            display: inline;
        }

        .user-block {
            margin-bottom: 10px;
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

        .search-container {
            text-align: center;
            margin-bottom: 20px;
        }

        .search-box {
            width: 300px;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 16px;
        }

        .btn-search {
            background-color: #0056b3;
            color: white;
            padding: 8px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

        .table-container {
            width: 90%;
            margin: 20px auto;
            border-collapse: collapse;
        }

        .table-container th, .table-container td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        .table-container th {
            background-color: #1D5DA7;
            color: white;
            font-weight: bold;
        }

        .btn-view {
            background-color: #4CAF50;
            color: white;
            padding: 6px 12px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
               <div class="header-container" style="display: flex; justify-content: space-between; align-items: center;">
    <div>
        <span class="user-label">Mã CB: </span><asp:Label ID="lblMaCB" runat="server" CssClass="user-info"></asp:Label><br/>
        <span class="user-label">Tên Cán Bộ: </span><asp:Label ID="lblTenCB" runat="server" CssClass="user-info"></asp:Label>
    </div>
    <asp:Button ID="btnLogout" runat="server" Text="Đăng xuất" OnClick="btnLogout_Click" CssClass="btn-danger" />
</div>

            </div>

            <h2 align="center">Danh Sách Môn Học</h2>

            <div class="search-container">
                <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSearch">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" Placeholder="Nhập mã môn hoặc tên lớp..."></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn-search" OnClick="btnSearch_Click" />
                </asp:Panel>
            </div>

            <table class="table-container">
                <tbody>
                    <asp:GridView ID="GridViewMonHoc" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="3" OnPageIndexChanging="GridViewMonHoc_PageIndexChanging"
                    CssClass="table-container">
                        <Columns>
                            <asp:BoundField DataField="MAMON" HeaderText="Mã Môn" />
                            <asp:BoundField DataField="TENMON" HeaderText="Tên Môn" />
                            <asp:BoundField DataField="MALOP" HeaderText="Mã Lớp" />
                            <asp:BoundField DataField="TENLOP" HeaderText="Tên Lớp" />
                            <asp:TemplateField HeaderText="Tùy Chọn">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Xem Sinh Viên" CssClass="btn-view"
                                        CommandArgument='<%# Eval("MAMON") + "," + Eval("MALOP") %>'
                                        OnClick="btnXemSinhVien_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>