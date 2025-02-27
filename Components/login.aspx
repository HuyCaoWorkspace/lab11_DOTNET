<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="lab11.Components.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng Nhập</title>
    <style>
        body {
            font-family: 'Times New Roman', sans-serif;
            background-color: #f5f5f5;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        form {
            background: white;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            padding: 30px;
            width: 350px;
            text-align: center;
            border: none;
        }

        h2 {
            color: #143D60;
            font-size: 28px;
            margin-bottom: 20px;
        }

        label {
            font-size: 18px;
            font-weight: bold;
            display: block;
            text-align: left;
            margin-bottom: 5px;
        }

        input[type="text"], input[type="password"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
        }

        .btn-container {
            display: flex;
            justify-content: space-between;
            margin-top: 15px;
        }

        .btn-login, .btn-cancel {
            width: 48%;
            padding: 10px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
        }

        .btn-login {
            background-color: #4CAF50;
            color: white;
        }

        .btn-login:hover {
            background-color: #45a049;
        }

        .btn-cancel {
            background-color: #D84040;
            color: white;
        }

        .btn-cancel:hover {
            background-color: #b22d2d;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Đăng Nhập</h2>
        
        <label for="maCB">Mã Cán Bộ:</label>
        <asp:TextBox ID="maCB" runat="server" Height="35px" Width="93%" />

        <label for="matkhau">Mật Khẩu:</label>
        <asp:TextBox ID="matkhau" runat="server" TextMode="Password" Height="35px" Width="93%" />

        <div class="btn-container">
            <asp:Button CssClass="btn-login" ID="Button1" runat="server" Text="Login" OnClick="btn_login" />
            <asp:Button CssClass="btn-cancel" ID="Button2" runat="server" Text="Cancel" OnClick="btn_cancel" />
        </div>
    </form>

</body>
</html>
