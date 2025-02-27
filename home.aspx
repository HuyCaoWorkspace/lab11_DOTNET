<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="lab11.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trang chủ</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>HI, this is your home page!</h3>
            <asp:Label runat="server" ID="lblWelcome" Font-Bold="true" Font-Size="Large" ForeColor="Green" /><br /><br />
            <p><b>Mã cán bộ:</b> <asp:Label runat="server" ID="lblMacb" /></p>
            <p><b>Họ và tên:</b> <asp:Label runat="server" ID="lblHoten" /></p>

        </div>
    </form>
</body>
</html>
