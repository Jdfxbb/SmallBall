<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Practice.aspx.cs" Inherits="SmallBall.Practice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color:lightblue">
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Label runat="server" ID="HomeLabel" Text="Your Team"></asp:Label>
                <asp:DropDownList ID="HomeTeamDDL" runat="server" AutoPostBack="true"></asp:DropDownList>
                <asp:Label runat="server" ID="AwayLabel" Text="Opponent"></asp:Label>
                <asp:DropDownList ID="AwayTeamDDL" runat="server" AutoPostBack="true"></asp:DropDownList>
                <asp:label ID="Heading" runat="server" Font-Size="X-Large"></asp:label>
            </div>
            <asp:Button ID="Take" runat="server" Text="Take" Width="25%" Visible="false" OnClick="Take_Click"/>
            <asp:Button ID="PitchOut" runat="server" Text="Pitch Out" Width="25%" Visible="false" OnClick="PitchOut_Click"/>
            <asp:Label runat="server" ID="Result" Visible="false"></asp:Label>
            
        </div>
        <div>
            <asp:Button ID="GuessFB" runat="server" Text="Guess Fastball" Width="25%" Visible="false" OnClick="GuessFB_Click"/>
            <asp:Button ID="Fastball" runat="server" Text="Fastball" Width="25%" Visible="false" OnClick="Fastball_Click"/>
            <asp:Button ID="StartGame" runat="server" Text="Start Game" Width="25%" Visible="true" OnClick="NextGame_Click" />
        </div>
        <div>
            <asp:Button ID="GuessBB" runat="server" Text="Guess Breaking Ball" Width="25%" Visible="false" OnClick="GuessBB_Click"/>
            <asp:Button ID="BreakingBall" runat="server" Text="Breaking Ball" Width="25%" Visible="false" OnClick="BreakingBall_Click"/>
        </div>
        <div>
            <asp:Button ID="GuessOS" runat="server" Text="Guess Off Speed" Width="25%" Visible="false" OnClick="GuessOS_Click"/>
            <asp:Button ID="OffSpeed" runat="server" Text="Off Speed" Width="25%" Visible="false" OnClick="OffSpeed_Click"/>
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
                ItemStyle-BorderColor="gray" 
                HeaderStyle-BorderColor="Gray">
            </asp:DataGrid>
            </div>
        
        <asp:Panel runat="server" BackImageUrl="paintfield.jpg" Width="390px" Height="400px" ID="Panel1">
            <asp:Image runat="server" ID="FireWorks1" ImageUrl="~/Fireworks_transparent_background_12.png" Height="60px" Width="60px" Visible="false"/>
            <asp:Image runat="server" ID="FireWorks2" ImageUrl="~/Fireworks_transparent_background_12.png" Height="60px" Width="60px" Visible="false"/>
            <asp:Image runat="server" ID="FireWorks3" ImageUrl="~/Fireworks_transparent_background_12.png" Height="60px" Width="60px" Visible="false"/>
            <div style="position:absolute;top:505px;left:5px;width=50px;height=50px">
                <asp:Image runat="server" ImageUrl="~/IMG_0707.PNG" ID="Third" Height="45px" Width="45px" Visible="false"/>
                
                
            </div>
            <div style="position:absolute;top:505px;left:90px;height:50px;width:50px">
            <asp:Image runat="server" ImageUrl="~/IMG_0707.PNG" ID="Second" Height="45px" Width="45px" Visible="false"/>
            </div>
            <div style="position:absolute;top:595px;left:90px;height:50px;width:50px">
                <asp:Image runat="server" ImageUrl="~/IMG_0707.PNG" ID="First" Height="45px" Width="45px" Visible="false"/>
            </div>
        </asp:Panel>
            
            

            <asp:Label ID="Label3" runat="server" Text="Balls"></asp:Label>
            <asp:TextBox ID="Balls" runat="server" Enabled="false" Width="3%" style="text-align:center"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Strikes"></asp:Label>
            <asp:TextBox ID="Strikes" runat="server" Enabled="false" Width="3%" style="text-align:center"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Outs"></asp:Label>
            <asp:TextBox ID="Outs" runat="server" Enabled="false" Width="3%" style="text-align:center"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="Feed"></asp:Label>
            <asp:TextBox ID="GameFeed" runat="server" Width="50%" TextMode="MultiLine" ReadOnly="true" Enabled="false" Rows="3"></asp:TextBox>
        <div>
            <asp:Button ID="Quit" runat="server" Width="25%" text="Quit" OnClick="Quit_Click"/>
         </div>
    </form>
</body>
</html>
