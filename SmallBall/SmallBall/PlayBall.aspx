﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayBall.aspx.cs" Inherits="SmallBall.PlayBall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="NewGame" runat="server" Text="New Game" Width="200px" OnClick="NewGame_Click" />
        <asp:TextBox ID="TeamName" runat="server" OnTextChanged="TeamName_TextChanged"></asp:TextBox>
        
        <div><asp:Button ID="Take" runat="server" Text="Take" Width="200px" OnClick="Take_Click" /></div>
        <div><asp:Button ID="GuessFB" runat="server" Text="Guess Fastball" Width="200px" OnClick="GuessFB_Click"/></div>
        <div><asp:Button ID="GuessBB" runat="server" Text="Guess Breaking Ball" Width="200px" OnClick="GuessBB_Click" /></div>
        <div><asp:Button ID="GuessOS" runat="server" Text="Guess Off Speed" Width="200px" OnClick="GuessOS_Click" /></div>
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
            ItemStyle-BorderColor="gray" HeaderStyle-BorderColor="Gray"
            
            
            >
            
        </asp:DataGrid>
    </form>
</body>
</html>