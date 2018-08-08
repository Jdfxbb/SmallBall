<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SmallBall.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body background="BallBackground.jpg" background-size="cover" style="background-size:cover">
    <form id="form1" runat="server" style="background-color:gray">
        <div>
            <asp:Label runat="server" ForeColor="White" Font-Size="XX-Large" Text="Register"></asp:Label>
        </div>
        <div>
        <asp:Label ID="Label1" runat="server" Text="User Name" Width="12%" ForeColor="White">
        </asp:Label><asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="Label4" runat="server" Text="Email" Width="12%" ForeColor="White">
        </asp:Label><asp:TextBox ID="Email" runat="server" TextMode="Email"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="Label2" runat="server" Text="Password" Width="12%" ForeColor="White">
        </asp:Label><asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="Label3" runat="server" Text="Confirm Password" Width="12%" ForeColor="White">
        </asp:Label><asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="ConfirmRegister" runat="server" Text="Register User" OnClick="ConfirmRegister_Click"/>
        </div>
        <asp:Label ID="ErrorLabel" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
