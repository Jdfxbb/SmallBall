<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmallBall.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Log In
        </div>
        <div>
        <asp:Label ID="Label1" runat="server" Text="User Name" Width="12%">
        </asp:Label><asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="Label2" runat="server" Text="Password" Width="12%">
        </asp:Label><asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="LogInBtn" runat="server" Text="Log In" OnClick="LogInBtn_Click"/>
        </div>
        <asp:Label ID="ErrorLabel" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
