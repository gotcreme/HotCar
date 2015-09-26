<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="HttpErrorPage.aspx.cs" Inherits="HotCar.WebUI.Admin.Pages.HttpErrorPage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/Error/css/HttpError.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <img id="backgroundImage" src="../App_Themes/Error/images/error.png"/>
    <span id="error">
    <asp:Label ID="lblError" runat="server" Text="Error" meta:resourcekey="lblErrorResource1"></asp:Label><br/></span>
    <div id="errorContent">
        <asp:Label ID="lblErrorCode" CssClass="lblErrorCode" runat="server" meta:resourcekey="lblErrorCodeResource1"></asp:Label>
    </div>
</asp:Content>
