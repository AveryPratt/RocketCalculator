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
        <asp:TextBox ID="StageNumberTextBox" runat="server" MaxLength="2" OnTextChanged="StageNumberTextBox_TextChanged" Width="30px"></asp:TextBox>
    
    </div>
        <asp:CheckBox ID="DeltaVCheckBox" runat="server" OnCheckedChanged="DeltaVCheckBox_CheckedChanged" Text="Find ΔV" />
        <p>
            <asp:CheckBox ID="TWRCheckBox" runat="server" AutoPostBack="True" OnCheckedChanged="TWRCheckBox_CheckedChanged" Text="Find TWR" />
        </p>
        <asp:DropDownList ID="ParentBodyDropDownList" runat="server" Enabled="False" Height="16px" OnSelectedIndexChanged="ParentBodyDropDownList_SelectedIndexChanged">
            <asp:ListItem Value="1">Kerbol</asp:ListItem>
            <asp:ListItem Value="2">Moho</asp:ListItem>
            <asp:ListItem Value="3">Eve</asp:ListItem>
            <asp:ListItem Value="4">Gilly</asp:ListItem>
            <asp:ListItem Selected="True" Value="5">Kerbin</asp:ListItem>
            <asp:ListItem Value="6">Mun</asp:ListItem>
            <asp:ListItem Value="7">Minmus</asp:ListItem>
            <asp:ListItem Value="8">Duna</asp:ListItem>
            <asp:ListItem Value="9">Ike</asp:ListItem>
            <asp:ListItem Value="10">Dres</asp:ListItem>
            <asp:ListItem Value="11">Jool</asp:ListItem>
            <asp:ListItem Value="12">Laythe</asp:ListItem>
            <asp:ListItem Value="13">Vall</asp:ListItem>
            <asp:ListItem Value="14">Tylo</asp:ListItem>
            <asp:ListItem Value="15">Bop</asp:ListItem>
            <asp:ListItem Value="16">Pol</asp:ListItem>
            <asp:ListItem Value="17">Eeloo</asp:ListItem>
        </asp:DropDownList>
        <p>
            <asp:Button ID="SetStagesButton" runat="server" OnClick="SetStagesButton_Clicked" Text="Create Rocket" />
        </p>
        <asp:Table ID="RocketTable" runat="server" Visible="False">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="My Rocket:" />
                <asp:TableCell runat="server" Text="Δv" Visible="False" />
                <asp:TableCell runat="server" Text="TWR" Visible="False" />
            </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
