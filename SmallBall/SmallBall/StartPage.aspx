<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="SmallBall.StartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body background="ball.jpg">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Title" runat="server" Text="Small Ball" Font-Size="XX-Large" Font-Bold="true" ForeColor="White"></asp:Label>
        </div>
        <div>
            <asp:Button ID="Login" runat="server" Text="Log In" OnClick="Login_Click" /><asp:Button ID="Register" runat="server" Text="Register" OnClick="Register_Click" />
        </div>
    </form>
</body>
</html>
