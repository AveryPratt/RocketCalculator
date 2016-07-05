<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CalculatorWebForm.aspx.cs" Inherits="RocketryWebApp.Calculator.CalculatorWebForm" MaintainScrollPositionOnPostBack = "true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>RocketCalculator</title>
        <link href="CalculatorStyleSheet.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="wrapper">
                <header>
                    <div id="HeaderDiv">
                        <h1>Rocket Calculator</h1>
                    </div>
                    <div id="UserNameDiv">
                        <p id="UserName" runat="server"></p>
                    </div>
                    <div id="LoginDiv" align="horizontal">
                        <a class="LoginLink" href="../Calculator/CalculatorWebForm.aspx">Home</a>
                        <a class="LoginLink" href="../Login/LoginWebForm.aspx">Login</a>
                        <a class="LoginLink" href="../CreateAccount/CreateAccountWebForm.aspx">Create Account</a>
                    </div>
                </header>
                <div id="content">
                    <div id="DescriptionDiv" class="ContentDiv">
                        <div class="DescriptionDiv">
                            <p id="Description" runat="server"></p>
                        </div>
                    </div>
                    <div id="CheckBoxDiv" class="ContentDiv">
                        <p>Find:</p>
                        <div class="CheckBoxDiv">
                            <asp:CheckBox ID="DeltaVCheckBox" runat="server" Text="ΔV" OnCheckedChanged="DeltaVCheckBox_CheckedChanged" AutoPostBack="True" />
                            or
                            <asp:CheckBox ID="IspCheckBox" runat="server" Text="Isp" OnCheckedChanged="IspCheckBox_CheckedChanged" AutoPostBack="True" />
                        </div>
                        <div class="CheckBoxDiv">
                            <asp:CheckBox ID="TWRCheckBox" runat="server" Text="TWR" AutoPostBack="True" OnCheckedChanged="TWRCheckBox_CheckedChanged" />
                            or
                            <asp:CheckBox ID="ThrustCheckBox" runat="server" Text="Thrust" AutoPostBack="True" OnCheckedChanged="ThrustCheckBox_CheckedChanged" />
                            <br />
                            <asp:DropDownList ID="RssParentBodyDropDownList" runat="server" Enabled="False" Height="16px" Visible="false">
                                <asp:ListItem Value="1" Text="Sun" />
                                <asp:ListItem Value="2" Text="Mercury" />
                                <asp:ListItem Value="3" Text="Venus" />
                                <asp:ListItem Value="4" Text="Earth" Selected="True" />
                                <asp:ListItem Value="5" Text="Moon" />
                                <asp:ListItem Value="6" Text="Mars" />
                                <asp:ListItem Value="7" Text="Phobos" />
                                <asp:ListItem Value="8" Text="Deimos" />
                                <asp:ListItem Value="9" Text="Vesta" />
                                <asp:ListItem Value="10" Text="Ceres" />
                                <asp:ListItem Value="11" Text="Pallas" />
                                <asp:ListItem Value="12" Text="Interamnia" />
                                <asp:ListItem Value="13" Text="Hygiea" />
                                <asp:ListItem Value="14" Text="Jupiter" />
                                <asp:ListItem Value="15" Text="Io" />
                                <asp:ListItem Value="16" Text="Europa" />
                                <asp:ListItem Value="17" Text="Ganymede" />
                                <asp:ListItem Value="18" Text="Callisto" />
                                <asp:ListItem Value="19" Text="Saturn" />
                                <asp:ListItem Value="20" Text="Mimas" />
                                <asp:ListItem Value="21" Text="Enceladus" />
                                <asp:ListItem Value="22" Text="Tethys" />
                                <asp:ListItem Value="23" Text="Dione" />
                                <asp:ListItem Value="24" Text="Rhea" />
                                <asp:ListItem Value="25" Text="Titan" />
                                <asp:ListItem Value="26" Text="Iapetus" />
                                <asp:ListItem Value="27" Text="Uranus" />
                                <asp:ListItem Value="28" Text="Miranda" />
                                <asp:ListItem Value="29" Text="Ariel" />
                                <asp:ListItem Value="30" Text="Umbriel" />
                                <asp:ListItem Value="31" Text="Titania" />
                                <asp:ListItem Value="32" Text="Oberon" />
                                <asp:ListItem Value="33" Text="Neptune" />
                                <asp:ListItem Value="34" Text="Proteus" />
                                <asp:ListItem Value="35" Text="Triton" />
                                <asp:ListItem Value="36" Text="Nereid" />
                                <asp:ListItem Value="37" Text="Pluto" />
                                <asp:ListItem Value="38" Text="Charon" />
                                <asp:ListItem Value="39" Text="Haumea" />
                                <asp:ListItem Value="40" Text="MakeMake" />
                                <asp:ListItem Value="40" Text="Eris" />
                            </asp:DropDownList>
                            <asp:DropDownList ID="KspParentBodyDropDownList" runat="server" Enabled="False" Height="16px">
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
                            <asp:CheckBox ID="MinTWRCheckBox" runat="server" Text="min" Checked="True" AutoPostBack="True" Enabled="False" OnCheckedChanged="MinCheckBox_CheckedChanged" />
                            <asp:CheckBox ID="MaxTWRCheckBox" runat="server" Text="max" Checked="True" AutoPostBack="True" Enabled="False" OnCheckedChanged="MaxCheckBox_CheckedChanged" />
                            <br />
                            <asp:CheckBox ID="SolarSystemSelector" runat="server" Font-Size="12px" Text="Use Real Solar System" OnCheckChanged="SolarSystemSelector_CheckedChanged" AutoPostBack="True" OnCheckedChanged="SolarSystemSelector_CheckedChanged"/>
                        </div>
                        <p>What is the name of your rocket?</p>
                        <div class="CheckBoxDiv">
                            <asp:TextBox ID="RocketNameTextBox" runat="server" />
                        </div>
                        <p>How many stages is your rocket?</p>
                        <div class="CheckBoxDiv">
                            <asp:TextBox ID="StageNumberTextBox" runat="server" MaxLength="1" Width="20px" />
                        </div>
                        <asp:Button ID="CreateRocketButton" CssClass="Button" runat="server" OnClick="CreateRocketButton_Clicked" Text="Create Rocket" />
                    </div>
                    <div id="TableDiv" class="ContentDiv" runat="server" visible="false">
                        <p>Payload Mass (tonnes):</p>
                        <p style="font-size: 12px;">(Optional)</p>
                        <div class="TableDiv">
                            <asp:TextBox Width="100" runat="server" ID="PayloadTextBox"></asp:TextBox>
                            <p style="font-size: 12px;">*<em>Do <strong><u>not</u></strong> include payload mass in 1st stage.</em></p>
                        </div>
                        <p>Booster:</p>
                        <asp:Table ID="RocketTable" runat="server" GridLines="Both">
                            <asp:TableRow runat="server" ID="HeaderRow">
                                <asp:TableCell ID="NameCell" runat="server" CssClass="TableCell"><nobr>Rocket</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell"><nobr>Wet Mass (tonnes):</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell"><nobr>Dry Mass (tonnes):</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell"><nobr>Isp (seconds):</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell"><nobr>Δv (m/s):</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell"><nobr>Thrust (kN):</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell"><nobr>Min. TWR:</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell"><nobr>Max. TWR:</nobr></asp:TableCell>
                                <asp:TableCell runat="server" CssClass="TableCell">
                                    <asp:Button ID="AddStageButton" runat="server" Text="Add Stage" OnClick="AddStageButton_Clicked" Width="100" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 1:</asp:TableCell>
                                <asp:TableCell ID="tc1" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox1" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc2" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox2" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc3" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox3" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc4" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox4" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc5" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox5" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc6" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox6" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc7" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox7" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button1" Text="Delete Stage" runat="server" OnClick="Button1_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 2:</asp:TableCell>
                                <asp:TableCell ID="tc8" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox8" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc9" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox9" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc10" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox10" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc11" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox11" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc12" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox12" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc13" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox13" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc14" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox14" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button2" Text="Delete Stage" runat="server" OnClick="Button2_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 3:</asp:TableCell>
                                <asp:TableCell ID="tc15" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox15" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc16" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox16" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc17" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox17" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc18" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox18" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc19" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox19" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc20" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox20" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc21" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox21" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button3" Text="Delete Stage" runat="server" OnClick="Button3_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 4:</asp:TableCell>
                                <asp:TableCell ID="tc22" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox22" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc23" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox23" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc24" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox24" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc25" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox25" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc26" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox26" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc27" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox27" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc28" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox28" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button4" Text="Delete Stage" runat="server" OnClick="Button4_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 5:</asp:TableCell>
                                <asp:TableCell ID="tc29" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox29" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc30" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox30" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc31" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox31" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc32" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox32" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc33" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox33" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc34" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox34" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc35" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox35" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button5" Text="Delete Stage" runat="server" OnClick="Button5_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 6:</asp:TableCell>
                                <asp:TableCell ID="tc36" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox36" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc37" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox37" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc38" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox38" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc39" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox39" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc40" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox40" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc41" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox41" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc42" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox42" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button6" Text="Delete Stage" runat="server" OnClick="Button6_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 7:</asp:TableCell>
                                <asp:TableCell ID="tc43" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox43" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc44" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox44" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc45" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox45" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc46" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox46" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc47" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox47" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc48" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox48" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc49" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox49" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button7" Text="Delete Stage" runat="server" OnClick="Button7_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 8:</asp:TableCell>
                                <asp:TableCell ID="tc50" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox50" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc51" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox51" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc52" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox52" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc53" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox53" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc54" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox54" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc55" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox55" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc56" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox56" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button8" Text="Delete Stage" runat="server" OnClick="Button8_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Stage 9:</asp:TableCell>
                                <asp:TableCell ID="tc57" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox57" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc58" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox58" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc59" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox59" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc60" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox60" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc61" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox61" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc62" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox62" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="tc63" runat="server">
                                    <asp:TextBox Width="100" ID="TextBox63" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server">
                                    <asp:Button ID="Button9" Text="Delete Stage" runat="server" OnClick="Button8_Clicked" Width="100" />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow runat="server" ID="FooterRow" Height="24px">
                                <asp:TableCell runat="server" CssClass="TableCell">Totals:</asp:TableCell>
                                <asp:TableCell runat="server" ID="FooterWetMass" CssClass="TableCell"></asp:TableCell>
                                <asp:TableCell runat="server" ID="FooterDryMass" CssClass="TableCell"></asp:TableCell>
                                <asp:TableCell runat="server" ID="FooterIsp" CssClass="TableCell"></asp:TableCell>
                                <asp:TableCell runat="server" ID="FooterDeltaV" CssClass="TableCell"></asp:TableCell>
                                <asp:TableCell runat="server" ID="FooterThrust" CssClass="TableCell"></asp:TableCell>
                                <asp:TableCell runat="server" ID="FooterMinTWR" CssClass="TableCell"></asp:TableCell>
                                <asp:TableCell runat="server" ID="FooterMaxTWR" CssClass="TableCell"></asp:TableCell>
                                <asp:TableCell runat="server" ID="Reference" CssClass="TableCell"></asp:TableCell>

                            </asp:TableRow>

                        </asp:Table>
                        <asp:Button ID="CalculateButton" CssClass="Button" runat="server" Text="Calculate" OnClick="CalculateButton_Clicked" Visible="false" />
                        <asp:Button ID="SaveButton" CssClass="Button" runat="server" Text="Save Rocket" OnClick="SaveRocketButton_Clicked" visible="false" />
                        <div align="center">
                            <p id="ErrorMessage" runat="server"></p>
                        </div>
                    </div>
                    <div id="RocketsDiv" class="ContentDiv" visible="false">
                        <div class="RocketsDiv">
                            <asp:GridView ID="UserRocketsGridView" runat="server" OnRowDeleting="UserRocketsGridView_RowDeleting" OnRowEditing="UserRocketsGridView_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="true" />
                                    <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="InstructionsDiv" class="ContentDiv">
                        <div class="InstructionsDiv">
                            <p id="Instructions" runat="server"></p>
                        </div>
                    </div>
                </div>
                <footer>
                    <p>
                        Created by Avery Pratt, © 2016. View my <a href="https://github.com/AveryPratt">Github</a> profile.<br />
                        Built using Microsoft <a href="http://www.asp.net/">ASP.NET</a> and <a href="https://www.microsoft.com/en-us/download/details.aspx?id=42299">SQL Server</a>.<br />
                        Jebediah, KSP, Kerbal Space Program, and all the celestial body names and properties belong to <a href="http://www.squad.com.mx/">Squad</a>.
                    </p>
                </footer>
            </div>
        </form>
    </body>
</html>