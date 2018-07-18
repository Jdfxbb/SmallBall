<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayBall.aspx.cs" Inherits="SmallBall.PlayBall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div><asp:Button ID="NewCareer" runat="server" Text ="New Career" Width="25%" OnClick="NewCareer_Click" /></div>
        <div><asp:Button ID="LoadCareer" runat="server" Text ="Load Career" Width="25%" OnClick="LoadCareer_Click" /></div>
        <div><asp:Button ID="Practice" runat="server" Text ="Practice" Width="25%" OnClick="Practice_Click" /></div>
        <asp:Button ID="NewGame" runat="server" Text="New Game" Width="25%" OnClick="NewGame_Click" />
        <asp:TextBox ID="TeamName" runat="server" OnTextChanged="TeamName_TextChanged" Width="25%"></asp:TextBox>
        <div>
            <asp:Button ID="Take" runat="server" Text="Take" Width="25%" OnClick="Take_Click" />
            <asp:Button ID="PitchOut" runat="server" Text="Pitch Out" Width="25%" OnClick="PitchOut_Click"/>
        </div>
        <div><asp:Button ID="GuessFB" runat="server" Text="Guess Fastball" Width="25%" OnClick="GuessFB_Click"/>
            <asp:Button ID="Fastball" runat="server" Text="Fastball" Width="25%" OnClick="Fastball_Click"/>
        </div>
        <div><asp:Button ID="GuessBB" runat="server" Text="Guess Breaking Ball" Width="25%" OnClick="GuessBB_Click" />
            <asp:Button ID="BreakingBall" runat="server" Text="Breaking Ball" Width="25%" OnClick="BreakingBall_Click"/>
        </div>
        <div><asp:Button ID="GuessOS" runat="server" Text="Guess Off Speed" Width="25%" OnClick="GuessOS_Click" />
            <asp:Button ID="OffSpeed" runat="server" Text="Off Speed" Width="25%" OnClick="OffSpeed_Click"/>
        </div>
        <div><asp:DataGrid ID="BoxScore" 
                runat="server" 
                BorderColor="Black" 
                BorderWidth="1" 
                ShowFooter="false" 
                AutoGenerateColumns="true" 
                CellPadding="10" 
                BackColor="Black" 
                ForeColor="White" 
                GridLines="Both" 
                BorderStyle="Ridge" 
                ItemStyle-BorderColor="gray" HeaderStyle-BorderColor="Gray">
            </asp:DataGrid>
            <asp:Label ID="Label3" runat="server" Text="Balls"></asp:Label>
            <asp:TextBox ID="Balls" runat="server" Enabled="false" Width="3%"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Strikes"></asp:Label>
            <asp:TextBox ID="Strikes" runat="server" Enabled="false" Width="3%"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Outs"></asp:Label>
            <asp:TextBox ID="Outs" runat="server" Enabled="false" Width="3%"></asp:TextBox>       
        </div>
        <div>
            <asp:RadioButton ID="Third" runat="server" BorderWidth="100" BorderColor="#996633" Enabled="false" ForeColor="Black"/>
            <asp:RadioButton ID="Second" runat="server" BorderWidth="100" BorderColor="#996633" Enabled="false"/>
        </div>
        <div>
            <asp:RadioButton ID="Home" runat="server" BorderWidth="100" BorderColor="#996633" Enabled="false" ForeColor="Black"/>
            <asp:RadioButton ID="First" runat="server" BorderWidth="100" BorderColor="#996633" Enabled="false"/>
        </div>
    </form>
</body>
</html>