<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ExceptionErrorPage.aspx.cs" Inherits="HotCar.WebUI.Admin.Pages.ExceptionErrorPage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/Error/css/ExceptionError.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <img id="backgroundImage" src="../App_Themes/Error/images/error.png"/>
    <span id="errorInfoTitle">
        <asp:Label ID="lblSmthHappen" runat="server" Text="Something was wrong :(" meta:resourcekey="lblSmthHappenResource1"></asp:Label>
        <br/>
    </span>
    <div id="errorContent">
        <span id="textWhat">
            <asp:Label ID="lblError" runat="server" Text="Error: " meta:resourcekey="lblErrorResource1"></asp:Label></span>
        <asp:Label ID="lblWhat" CssClass="lblWhat" runat="server" meta:resourcekey="lblWhatResource1"></asp:Label>
        <span id="textWhy"><br/><br/><br/>
                <asp:Label ID="lblReason" runat="server" Text="Reason: " meta:resourcekey="lblReasonResource1"></asp:Label>
        </span>
        <asp:Label ID="lblWhy" runat="server" CssClass="lblWhy" meta:resourcekey="lblWhyResource1"></asp:Label>
        <span id="textSuggestion"><br/><br/><br/>
            <asp:Label ID="lblWhatToDo" runat="server" Text="What to do: " meta:resourcekey="lblWhatToDoResource1"></asp:Label>
        </span>
        <asp:Label ID="lblSuggestion" runat="server" CssClass="lblSuggestion" meta:resourcekey="lblSuggestionResource1"></asp:Label>
    </div>
</asp:Content>
