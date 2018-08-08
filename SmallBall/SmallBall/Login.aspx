<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmallBall.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body background="BallBackground.jpg" background-size="cover" style="background-size:cover">
    <form id="form1" runat="server" style="background-color:gray">
        <div>
            <asp:Label runat="server" Text="Log In" ForeColor="White" Font-Size="XX-Large"></asp:Label>
        </div>
        <div>
        <asp:Label ID="Label1" runat="server" Text="User Name" Width="12%" ForeColor="White">
        </asp:Label><asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="Label2" runat="server" Text="Password" Width="12%" ForeColor="White">
        </asp:Label><asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="LogInBtn" runat="server" Text="Log In" OnClick="LogInBtn_Click"/>
        </div>
        <asp:Label ID="ErrorLabel" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Button ID="RegisterBtn" runat="server" Text="Register Instead" Visible="false" OnClick="RegisterBtn_Click" />
        
    </form>
</body>
</html>
