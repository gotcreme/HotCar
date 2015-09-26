<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="HotCar.WebUI.Admin.Controls.Menu" %>
    <div class="right_header">
        <asp:Label ID="lblLeftSeparator" runat="server" Text="|" meta:resourcekey="lblLeftSeparatorResource1"></asp:Label><asp:LinkButton ID="lBtnEng" runat="server" Text="Eng" meta:resourcekey="lBtnEngResource1"></asp:LinkButton><asp:Label ID="lblComa" runat="server" Text="," meta:resourcekey="lblComaResource1"></asp:Label>
        <asp:LinkButton ID="lBtnUkr" runat="server" Text="Ukr" meta:resourcekey="lBtnUkrResource1"></asp:LinkButton><asp:Label ID="lblRightSeparator" runat="server" Text="|" meta:resourcekey="lblRightSeparatorResource1"></asp:Label>
        <asp:LoginView ID="lv1" runat="server">    
            <LoggedInTemplate>
                <asp:Label ID="lblHello" runat="server" Text="Hi" meta:resourcekey="lblHelloResource1"></asp:Label> 
                <asp:LoginName runat="server" ID="ln1" ForeColor="Orange" Font-Bold="True" meta:resourcekey="ln1Resource1" />,
                <asp:HyperLink ID="linkHome" runat="server" Text="Visit a site" meta:resourcekey="linkHomeResource1" />|
            </LoggedInTemplate>
        </asp:LoginView><asp:LoginStatus runat="server" id="ls1" meta:resourcekey="ls1Resource1" />
        
    </div>
