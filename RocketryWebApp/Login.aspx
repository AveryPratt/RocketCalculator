<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="RocketryWebApp.Login.LoginWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link rel="shortcut icon" type="image/x-icon" href="Logo.ico" />
        <meta name="google-site-verification" content="JfdbGc06ArxeTwfzdFKeNVTfVYUcgkr2s_dOTsNQ0Tw" />
        <title>RocketCalculator</title>
        <link href="CalculatorStyleSheet.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="wrapper">
                <header>
                    <img src="BigLogo.png" style="height: 60px; left: 20px; top: 10px; position: absolute;"/>
                    <div id="TitleDiv">
                        <h1>Rocket Calculator</h1>
                    </div>
                    <div id="LoginDiv" class="HeaderDiv" runat="server">
                        <asp:LinkButton CssClass="AccountLink" ID="HomeButton" runat="server" OnClick="HomeButton_Click">Home</asp:LinkButton>
                        <asp:LinkButton CssClass="AccountLink" ID="CreateAccountButton" runat="server" OnClick="CreateAccountButton_Click">Create Account</asp:LinkButton>
                    </div>
                </header>
                <div id="content" >
                    <div class="LoginFormDiv">
                        <asp:Table ID="LoginTable" runat="server">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>Login</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell style="text-align: right;">Username:</asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="LoginUserNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
                                <asp:TableCell id="LoginUserNameResponse" runat="server"></asp:TableCell>
    <%--                            <asp:TableCell><asp:RequiredFieldValidator ID="LoginUserNameRequiredFieldValidator" runat="server" ErrorMessage="Please enter Username" ControlToValidate="LoginUserNameTextBox"></asp:RequiredFieldValidator></asp:TableCell>--%>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="text-align: right;">Password:</asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="LoginPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox></asp:TableCell>
                                <asp:TableCell id="LoginPasswordResponse" runat="server"></asp:TableCell>
    <%--                            <asp:TableCell><asp:RequiredFieldValidator ID="LoginPasswordRequiredFieldValidator" runat="server" ErrorMessage="Please enter Password" ControlToValidate="LoginPasswordTextBox" TextMode="Password"></asp:RequiredFieldValidator></asp:TableCell>--%>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell><asp:Button ID="LoginButton" Text="Login" runat="server" OnClick="LoginButton_Clicked" /></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </div>
                <footer>
                    <p>
                        Created by Avery Pratt, © 2016. View my <a href="https://github.com/AveryPratt">Github</a> profile.<br />
                        Built using Microsoft <a href="http://www.asp.net/">ASP.NET</a>, <a href="https://azure.microsoft.com/en-us/">Azure</a>, and <a href="https://www.microsoft.com/en-us/download/details.aspx?id=42299">SQL Server</a>.<br />
                        Jebediah, Kerbal Space Program, and the names and properties of default rockets and celestial bodies belong to <a href="http://www.squad.com.mx/">Squad</a>.
                    </p>
                </footer>
            </div>
        </form>
    </body>
</html>
