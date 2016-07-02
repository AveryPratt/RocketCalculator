<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="LoginWebForm.aspx.cs" Inherits="RocketryWebApp.Login.LoginWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>RocketCalculator</title>
        <link href="../Calculator/CalculatorStyleSheet.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="wrapper">
                <header>
                    <div id="HeaderDiv">
                        <h1>Rocket Calculator</h1>
                    </div>
                    <div id="HomeDiv">
                        <a href="../Calculator/CalculatorWebForm.aspx">Home</a>
                    </div>
                    <div id="LoginDiv">
                        <a href="../Login/LoginWebForm.aspx">Login</a>
                    </div>
                    <div id="CreateAccountDiv">
                        <a href="../CreateAccount/CreateAccountWebForm.aspx">Create Account</a>
                    </div>
                </header>
                <div id="content">
                    <asp:Table ID="LoginTable" runat="server">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>Login</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell>Username:</asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="LoginUserNameTextBox" runat="server"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:RequiredFieldValidator ID="LoginUserNameRequiredFieldValidator" runat="server" ErrorMessage="Please enter Username" ControlToValidate="LoginUserNameTextBox"></asp:RequiredFieldValidator></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Password:</asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="LoginPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:RequiredFieldValidator ID="LoginPasswordRequiredFieldValidator" runat="server" ErrorMessage="Please enter Password" ControlToValidate="LoginPasswordTextBox" TextMode="Password"></asp:RequiredFieldValidator></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="LoginButton" Text="Login" runat="server" OnClick="LoginButton_Clicked" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
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
