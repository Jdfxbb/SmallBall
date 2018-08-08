<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCareer.aspx.cs" Inherits="SmallBall.NewCareer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body background="Bleachers.jpg" style="background-size:cover" >
    <form id="form1" runat="server" style="background-color:lightblue;border-color:black;border-width:medium">
        <div>
            Start New Career
        </div>
        <div>
            <asp:DropDownList ID="CityList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CityList_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="TeamNameList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TeamNameList_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div>
            <asp:label ID="UserTeam" runat="server" text=""></asp:label>
        </div>
        <div>
            <asp:Button ID="Confirm" runat="server" Text="Create Team" OnClick="Confirm_Click" />
        </div>
    </form>
</body>
</html>
