<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="RocketryWebApp.WebForm1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            How many stages is your rocket?<br />
            <asp:TextBox ID="StageNumberTextBox" runat="server" MaxLength="1" Width="20px" />

            <br />
            Find:</div>
        <asp:CheckBox ID="DeltaVCheckBox" runat="server" Text="ΔV" OnCheckedChanged="DeltaVCheckBox_CheckedChanged" AutoPostBack="True" />
            <asp:CheckBox ID="IspCheckBox" runat="server" Text="Isp" OnCheckedChanged="IspCheckBox_CheckedChanged" AutoPostBack="True" />
            <br />
        <br />
            <asp:CheckBox ID="TWRCheckBox" runat="server" Text="TWR" AutoPostBack="True" OnCheckedChanged="TWRCheckBox_CheckedChanged" />
            <asp:CheckBox ID="ThrustCheckBox" runat="server" Text="Thrust" AutoPostBack="True" OnCheckedChanged="ThrustCheckBox_CheckedChanged" />
        <br />
        <asp:DropDownList ID="ParentBodyDropDownList" runat="server" Enabled="False" Height="16px">
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
            <asp:CheckBox ID="MinTWRCheckBox" runat="server" Text="min" Checked="True" AutoPostBack="True" Enabled="False" />
            <asp:CheckBox ID="MaxTWRCheckBox" runat="server" Text="max" Checked="True" AutoPostBack="True" Enabled="False" />
        <p>
            <asp:Button ID="SetStagesButton" runat="server" OnClick="SetStagesButton_Clicked" Text="Create Rocket" />
        </p>
        <asp:Table ID="RocketTable" runat="server" GridLines="Both">
            <asp:TableRow runat="server">
                <asp:TableCell Width="100" runat="server" Text="Rocket" />
                <asp:TableCell Width="100" runat="server" Text="Wet Mass (tonnes):" />
                <asp:TableCell Width="100" runat="server" Text="Dry Mass (tonnes):" />
                <asp:TableCell Width="100" runat="server" Text="Isp (seconds):" />
                <asp:TableCell Width="100" runat="server" Text="Δv (m/s):" />
                <asp:TableCell Width="100" runat="server" Text="Thrust (kN):" />
                <asp:TableCell Width="100" runat="server" Text="Min. TWR:" />
                <asp:TableCell Width="100" runat="server" Text="Max. TWR:" />
                <asp:TableCell Width="100">
                    <asp:Button ID="AddStageButton" runat="server" Text="Add Stage" OnClick="AddStageButton_Clicked" Width="100" />
                
</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 1:</asp:TableCell>
                <asp:TableCell ID ="tc1" runat="server">
                    <asp:TextBox Width="100" ID="TextBox1" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc2" runat="server">
                    <asp:TextBox Width="100" ID="TextBox2" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc3" runat="server">
                    <asp:TextBox Width="100" ID="TextBox3" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc4" runat="server">
                    <asp:TextBox Width="100" ID="TextBox4" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc5" runat="server">
                    <asp:TextBox Width="100" ID="TextBox5" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc6" runat="server">
                    <asp:TextBox Width="100" ID="TextBox6" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc7" runat="server">
                    <asp:TextBox Width="100" ID="TextBox7" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button1" Text="Delete Stage" runat="server" OnClick="Button1_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 2:</asp:TableCell><asp:TableCell ID ="tc8" runat="server">
                    <asp:TextBox Width="100" ID="TextBox8" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc9" runat="server">
                    <asp:TextBox Width="100" ID="TextBox9" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc10" runat="server">
                    <asp:TextBox Width="100" ID="TextBox10" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc11" runat="server">
                    <asp:TextBox Width="100" ID="TextBox11" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc12" runat="server">
                    <asp:TextBox Width="100" ID="TextBox12" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc13" runat="server">
                    <asp:TextBox Width="100" ID="TextBox13" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc14" runat="server">
                    <asp:TextBox Width="100" ID="TextBox14" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button2" Text="Delete Stage" runat="server" OnClick="Button2_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 3:</asp:TableCell><asp:TableCell ID ="tc15" runat="server">
                    <asp:TextBox Width="100" ID="TextBox15" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc16" runat="server">
                    <asp:TextBox Width="100" ID="TextBox16" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc17" runat="server">
                    <asp:TextBox Width="100" ID="TextBox17" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc18" runat="server">
                    <asp:TextBox Width="100" ID="TextBox18" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc19" runat="server">
                    <asp:TextBox Width="100" ID="TextBox19" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc20" runat="server">
                    <asp:TextBox Width="100" ID="TextBox20" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc21" runat="server">
                    <asp:TextBox Width="100" ID="TextBox21" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button3" Text="Delete Stage" runat="server" OnClick="Button3_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 4:</asp:TableCell><asp:TableCell ID ="tc22" runat="server">
                    <asp:TextBox Width="100" ID="TextBox22" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc23" runat="server">
                    <asp:TextBox Width="100" ID="TextBox23" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc24" runat="server">
                    <asp:TextBox Width="100" ID="TextBox24" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc25" runat="server">
                    <asp:TextBox Width="100" ID="TextBox25" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc26" runat="server">
                    <asp:TextBox Width="100" ID="TextBox26" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc27" runat="server">
                    <asp:TextBox Width="100" ID="TextBox27" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc28" runat="server">
                    <asp:TextBox Width="100" ID="TextBox28" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button4" Text="Delete Stage" runat="server" OnClick="Button4_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 5:</asp:TableCell><asp:TableCell ID ="tc29" runat="server">
                    <asp:TextBox Width="100" ID="TextBox29" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc30" runat="server">
                    <asp:TextBox Width="100" ID="TextBox30" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc31" runat="server">
                    <asp:TextBox Width="100" ID="TextBox31" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc32" runat="server">
                    <asp:TextBox Width="100" ID="TextBox32" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc33" runat="server">
                    <asp:TextBox Width="100" ID="TextBox33" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc34" runat="server">
                    <asp:TextBox Width="100" ID="TextBox34" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc35" runat="server">
                    <asp:TextBox Width="100" ID="TextBox35" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button5" Text="Delete Stage" runat="server" OnClick="Button5_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 6:</asp:TableCell><asp:TableCell ID ="tc36" runat="server">
                    <asp:TextBox Width="100" ID="TextBox36" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc37" runat="server">
                    <asp:TextBox Width="100" ID="TextBox37" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc38" runat="server">
                    <asp:TextBox Width="100" ID="TextBox38" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc39" runat="server">
                    <asp:TextBox Width="100" ID="TextBox39" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc40" runat="server">
                    <asp:TextBox Width="100" ID="TextBox40" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc41" runat="server">
                    <asp:TextBox Width="100" ID="TextBox41" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc42" runat="server">
                    <asp:TextBox Width="100" ID="TextBox42" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button6" Text="Delete Stage" runat="server" OnClick="Button6_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 7:</asp:TableCell><asp:TableCell ID ="tc43" runat="server">
                    <asp:TextBox Width="100" ID="TextBox43" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc44" runat="server">
                    <asp:TextBox Width="100" ID="TextBox44" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc45" runat="server">
                    <asp:TextBox Width="100" ID="TextBox45" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc46" runat="server">
                    <asp:TextBox Width="100" ID="TextBox46" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc47" runat="server">
                    <asp:TextBox Width="100" ID="TextBox47" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc48" runat="server">
                    <asp:TextBox Width="100" ID="TextBox48" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc49" runat="server">
                    <asp:TextBox Width="100" ID="TextBox49" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button7" Text="Delete Stage" runat="server" OnClick="Button7_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 8:</asp:TableCell><asp:TableCell ID ="tc50" runat="server">
                    <asp:TextBox Width="100" ID="TextBox50" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc51" runat="server">
                    <asp:TextBox Width="100" ID="TextBox51" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc52" runat="server">
                    <asp:TextBox Width="100" ID="TextBox52" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc53" runat="server">
                    <asp:TextBox Width="100" ID="TextBox53" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc54" runat="server">
                    <asp:TextBox Width="100" ID="TextBox54" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc55" runat="server">
                    <asp:TextBox Width="100" ID="TextBox55" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc56" runat="server">
                    <asp:TextBox Width="100" ID="TextBox56" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                    <asp:Button ID="Button8" Text="Delete Stage" runat="server" OnClick="Button8_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableCell runat="server">Stage 9:</asp:TableCell><asp:TableCell ID ="tc57" runat="server">
                    <asp:TextBox Width="100" ID="TextBox57" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc58" runat="server">
                    <asp:TextBox Width="100" ID="TextBox58" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc59" runat="server">
                    <asp:TextBox Width="100" ID="TextBox59" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc60" runat="server">
                    <asp:TextBox Width="100" ID="TextBox60" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc61" runat="server">
                    <asp:TextBox Width="100" ID="TextBox61" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc62" runat="server">
                    <asp:TextBox Width="100" ID="TextBox62" runat="server" />
                

</asp:TableCell><asp:TableCell ID ="tc63" runat="server">
                    <asp:TextBox Width="100" ID="TextBox63" runat="server" />
                

</asp:TableCell><asp:TableCell runat="server">
                        <asp:Button ID="Button9" Text="Delete Stage" runat="server" OnClick="Button8_Clicked" Width="100" />
                
</asp:TableCell></asp:TableRow><asp:TableRow runat="server"><asp:TableCell runat="server">Total:</asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell></asp:TableRow></asp:Table><p>

        <p>
            <asp:Button ID="CalculateButton" runat="server" Text="Calculate" OnClick="CalculateButton_Clicked" /></p>
    </form>
</body>
</html>
