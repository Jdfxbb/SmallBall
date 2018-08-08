<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="SmallBall.MainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
</head>
<body background="Stadium2.jpg" style="background-size:cover;">
     <form id="form1" runat="server">
         <div>
             Main Menu
             <asp:HiddenField ID="LoadedLeague" ClientIDMode="Static" runat="server" value=""/>
             <asp:HiddenField ID="LoadedUserTeam" ClientIDMode="Static" runat="server" value=""/>
             <asp:HiddenField ID="LoadedLastPlayed" ClientIDMode="Static" runat="server" value=""/>
             <asp:HiddenField ID="LoadedCurrentGame" ClientIDMode="Static" runat="server" value=""/>
         </div>
        <div><asp:Button ID="NewCareer" runat="server" Text ="New Career" Width="25%" OnClick="NewCareer_Click"/></div>
        <div><asp:Button ID="LoadCareer" runat="server" Text ="Load Career" Width="25%" OnClick="LoadCareer_Click"/></div>
        <div><asp:Button ID="Practice" runat="server" Text ="Practice" Width="25%" OnClick="Practice_Click"/></div>
         <div><asp:Button ID="Logout" runat="server" Text ="Log out" Width="25%" OnClick="Logout_Click"></asp:Button></div>
        <div style="background-color:gray"><asp:Label runat="server" ID="WarningLabel" Visible="false" ForeColor="White"></asp:Label></div>
    </form>


</body>
</html>
    <script type="text/javascript">
        function Retrieve(id)
        {
            return localStorage.getItem(id);
        }
        var loadLeagueID = "League" + "<%=GetUserName()%>";
        var loadUserTeamID = "UserTeam" + "<%=GetUserName()%>";
        var loadLastPlayedID = "LastPlayed" + "<%=GetUserName()%>";
        var loadCurrentGameID = "CurrentGame" + "<%=GetUserName()%>";
        var loadLeague = Retrieve(loadLeagueID);
        var loadUserTeam = Retrieve(loadUserTeamID);
        var loadLastPlayed = Retrieve(loadLastPlayedID);
        var loadCurrentGame = Retrieve(loadCurrentGameID);
        form1.LoadedLeague.value = loadLeague;
        form1.LoadedUserTeam.value = loadUserTeam;
        form1.LoadedLastPlayed.value = loadLastPlayed;
        form1.LoadedCurrentGame.value = loadCurrentGame;
    </script>
