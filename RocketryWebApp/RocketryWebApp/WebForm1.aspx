<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="RocketryWebApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            How many stages is your rocket?<br />
            <asp:TextBox ID="StageNumberTextBox" runat="server" MaxLength="2" Width="30px" />

            <br />
            Find:</div>
        <asp:CheckBox ID="DeltaVCheckBox" runat="server" Text="ΔV" Checked="True" AutoPostBack="True" />
            <asp:CheckBox ID="TWRCheckBox" runat="server" Text="TWR" Checked="True" AutoPostBack="True" />
            <asp:CheckBox ID="IspCheckBox" runat="server" Text="Isp" Checked="True" AutoPostBack="True" />
            <asp:CheckBox ID="ThrustCheckBox" runat="server" Text="Thrust" Checked="True" AutoPostBack="True" />
        <br />
        <br />
        TWR:<p>
        <asp:DropDownList ID="ParentBodyDropDownList" runat="server" Enabled="False" Height="16px" AutoPostBack="True">
            <asp:ListItem Value="1" Text="Kerbol" />
            <asp:ListItem Value="2" Text="Moho" />
            <asp:ListItem Value="3" Text="Eve" />
            <asp:ListItem Value="4" Text="Gilly" />
            <asp:ListItem Value="5" Text="Kerbin" Selected="True" />
            <asp:ListItem Value="6" Text="Mun" />
            <asp:ListItem Value="7" Text="Minmus" />
            <asp:ListItem Value="8" Text="Duna" />
            <asp:ListItem Value="9" Text="Ike" />
            <asp:ListItem Value="10" Text="Dres" />
            <asp:ListItem Value="11" Text="Jool" />
            <asp:ListItem Value="12" Text="Laythe" />
            <asp:ListItem Value="13" Text="Vall" />
            <asp:ListItem Value="14" Text="Tylo" />
            <asp:ListItem Value="15" Text="Bop" />
            <asp:ListItem Value="16" Text="Pol" />
            <asp:ListItem Value="17" Text="Eeloo" />
        </asp:DropDownList>
            <asp:CheckBox ID="MinTWRCheckBox" runat="server" Text="min" Checked="True" AutoPostBack="True" />
            <asp:CheckBox ID="MaxTWRCheckBox" runat="server" Text="max" Checked="True" AutoPostBack="True" />
        </p>
        <p>
            <asp:Button ID="SetStagesButton" runat="server" OnClick="SetStagesButton_Clicked" Text="Create Rocket" />
        </p>
        <asp:Table ID="RocketTable" runat="server" GridLines="Both">
        </asp:Table>
        <p>
            <asp:Button ID="CalculateButton" runat="server" OnClientClick="CalculateButton_Clicked" Text="Calculate" Visible="False" />
        </p>
    </form>
</body>
</html>
