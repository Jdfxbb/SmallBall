<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayBall.aspx.cs" Inherits="SmallBall.PlayBall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="NewGame" runat="server" Text="New Game" Width="200px" OnClick="NewGame_Click" />
        <asp:TextBox ID="TeamName" runat="server" OnTextChanged="TeamName_TextChanged"></asp:TextBox>
        
        <div><asp:Button ID="Take" runat="server" Text="Take" Width="200px" OnClick="Take_Click" />
            <asp:Button ID="Walk" runat="server" Text="Walk" Width="200px" OnClick="Walk_Click"/>
        </div>
        <div><asp:Button ID="GuessFB" runat="server" Text="Guess Fastball" Width="200px" OnClick="GuessFB_Click"/>
            <asp:Button ID="Out" runat="server" Text="Out" Width="200px" OnClick="Out_Click"/>
        </div>
        <div><asp:Button ID="GuessBB" runat="server" Text="Guess Breaking Ball" Width="200px" OnClick="GuessBB_Click" />
            <asp:Button ID="Single" runat="server" Text="Single" Width="200px" OnClick="Single_Click"/>
        </div>
        <div><asp:Button ID="GuessOS" runat="server" Text="Guess Off Speed" Width="200px" OnClick="GuessOS_Click" />
            <asp:Button ID="Double" runat="server" Text="Double" Width="200px" OnClick="Double_Click"/>
        </div>
           <div>
            <asp:DataGrid ID="BoxScore" 
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

