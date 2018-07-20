<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="SmallBall.MainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
         <div>
             Main Menu
         </div>
        <div><asp:Button ID="NewCareer" runat="server" Text ="New Career" Width="25%" OnClick="NewCareer_Click"/></div>
        <div><asp:Button ID="LoadCareer" runat="server" Text ="Load Career" Width="25%" OnClick="LoadCareer_Click"/></div>
        <div><asp:Button ID="Practice" runat="server" Text ="Practice" Width="25%" OnClick="Practice_Click"/></div>
    </form>
</body>
</html>
