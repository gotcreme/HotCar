<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HotCar.WebUI.Admin.Pages.Login" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="register">    
        <li> 
          <h2 class="registration1">
              <asp:Login ID="loginForm" runat="server" OnAuthenticate="loginForm_Authenticate" FailureText="Wrong login data" LoginButtonText="Log in" PasswordLabelText="Password" PasswordRequiredErrorMessage="Enter Password" RememberMeText="Remember me" UserNameLabelText="Login" UserNameRequiredErrorMessage="Enter login" meta:resourcekey="loginFormResource1">
              </asp:Login>
          </h2>
        </li> 
    </ul> 
</asp:Content>
