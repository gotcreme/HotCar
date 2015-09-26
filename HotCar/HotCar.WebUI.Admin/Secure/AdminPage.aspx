<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="HotCar.WebUI.Admin.Secure.AdminPage" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="~/Controls/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/Libs/ddaccordion.js"></script>
    <script type="text/javascript" src="../Scripts/ddaccordion_init.js"></script>
    <link href="../Styles/jq_styles.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/AdminPage.js"></script>
    <script src="../Scripts/Libs/jquery-1.8.2.js"></script>
    <script src="../Scripts/Libs/jquery-ui.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper2">
        <div class="section">
            <div class="content">
                <div class="left_content">
                    <div class="sidebar_search">
                        <div>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="search_input" onclick="this.value=''" meta:resourcekey="txtSearchResource1"></asp:TextBox>

                            <asp:Button ID="btnSearch" runat="server" CssClass="search_submit" BorderStyle="None" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" />
                        </div>

                    </div>
                    <div class="clear"></div>
                    <div class="sidebarmenu">
                        <a class="menuitem submenuheader" href="">
                            <asp:Label ID="lblUsers" runat="server" Text="Users" meta:resourcekey="lblUsersResource1"></asp:Label>
                        </a>
                        <div class="submenu">
                            <ul>
                                <li>
                                    <asp:Button ID="btnAdmins" runat="server" Text="Administrators" OnClick="btnAdmins_Click" meta:resourcekey="btnAdminsResource1" />

                                </li>
                                <li>
                                    <asp:Button ID="btnSimpleUsers" runat="server" Text="CommonUsers" OnClick="btnSimpleUsers_Click" meta:resourcekey="btnSimpleUsersResource1" />

                                </li>
                                <li>
                                    <asp:Button ID="btnInactiveUsers" runat="server" Text="Inactive Users" OnClick="btnInactiveUsers_Click" meta:resourcekey="btnInactiveUsersResource1" />
                                </li>
                                <li>
                                    <asp:Button ID="btnAllUsers" runat="server" Text="All Users" OnClick="btnAllUsers_Click" meta:resourcekey="btnAllUsersResource1" />
                                </li>
                            </ul>
                        </div>
                        <a class="menuitem submenuheader" href="">
                            <asp:Label ID="lblSettings" runat="server" Text="Settings" meta:resourcekey="lblSettingsResource1"></asp:Label>
                        </a>
                        <div class="submenu">
                            <ul>
                                <li>
                                    <asp:Label ID="Label1" runat="server" Text="Користувачів на сторінці" meta:resourcekey="Label1Resource1"></asp:Label>                          
                                    <asp:DropDownList ID="ddlGridViewPageSize" runat="server" CssClass="GridSettings" AutoPostBack="True" OnSelectedIndexChanged="ddlGridViewPageSize_SelectedIndexChanged" meta:resourcekey="ddlGridViewPageSizeResource1">
                                        <asp:ListItem Value="10" Text="10 користувачів" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20 користувачів" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50 користувачів" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100 користувачів" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                            </ul>
                        </div>
                    </div>
                     <div class="sidebar_box">
                        <div class="sidebar_box_top"></div>
                        <div class="sidebar_box_content">
                            <br/>
                            <div>
                                <asp:Label ID="lblMessages" CssClass="lblMessages" runat="server" Text="Messages" meta:resourcekey="lblMessagesResource1"></asp:Label>
                                <br/>
                                <img id="gmailImage" src="../App_Themes/Standart/images/gmail.png"/>
                                <span id="messagesInfo"></span>
                            </div>
                            <asp:Label runat="server" ID="email" CssClass="email"></asp:Label>
                            <br/>
                            <br/>
                        </div>
                        <div class="sidebar_box_bottom"></div>
                    </div>

                    <div class="sidebar_box">
                        <div class="radio">
                            <a id="onlineRadioLink" href="http://radiotuna.com/classical-radio">classical radio</a><script type="text/javascript" src="http://radiotuna.com/OnlineRadioPlayer/EmbedRadio?playerParams=%7B%22styleSelection0%22%3A126%2C%22styleSelection1%22%3A178%2C%22styleSelection2%22%3A154%2C%22textColor%22%3A0%2C%22backgroundColor%22%3A13551045%2C%22buttonColor%22%3A5397227%2C%22glowColor%22%3A5397227%2C%22playerSize%22%3A200%2C%22playerType%22%3A%22style%22%7D&width=175&height=244"></script>
                        </div>
                    </div>

                   
                </div>


                <div class="right_content">
                    <h2>
                        <asp:Label ID="lblUserList" runat="server" Text="User List" meta:resourcekey="lblUserListResource1"></asp:Label>
                        (<asp:Label ID="lblUsersListName" runat="server" meta:resourcekey="lblUsersListNameResource1"></asp:Label>)
                    </h2>
                    <div style="overflow-x: auto; width: 970px">
                        <asp:GridView ID="GridViewUsers" runat="server" CssClass="gridStyle" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Vertical" Font-Size="Small" ShowFooter="True" AllowPaging="True" AutoGenerateEditButton="True" OnPageIndexChanging="GridViewUsers_PageIndexChanging" OnRowCancelingEdit="GridViewUsers_RowCancelingEdit" OnRowEditing="GridViewUsers_RowEditing" OnRowUpdating="GridViewUsers_RowUpdating" DataSourceID="odsGetUsers" meta:resourcekey="GridViewUsersResource1">

                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" meta:resourcekey="chkSelectAllResource1" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" meta:resourcekey="chkRowResource1" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" meta:resourcekey="TemplateFieldResource2">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblUserIdu" runat="server" Text='<%# Bind("Id") %>' meta:resourcekey="lblUserIduResource1"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserId" runat="server" Text='<%# Bind("Id") %>' meta:resourcekey="lblUserIdResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Login" meta:resourcekey="TemplateFieldResource3">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblUserLoginu" runat="server" Text='<%# Bind("Login") %>' meta:resourcekey="lblUserLoginuResource1"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserLogin" runat="server" Text='<%# Bind("Login") %>' meta:resourcekey="lblUserLoginResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First name" meta:resourcekey="TemplateFieldResource4">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUserNameu" runat="server" Text='<%# Bind("FirstName") %>' meta:resourcekey="txtUserNameuResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName"
                                            runat="server"
                                            ControlToValidate="txtUserNameu"
                                            ErrorMessage="Enter first name"
                                            ForeColor="White"
                                            Display="Dynamic" meta:resourcekey="RequiredFieldValidatorFirstNameResource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorFirstName"
                                            runat="server"
                                            ErrorMessage="Wrong first name"
                                            ForeColor="White"
                                            ControlToValidate="txtUserNameu"
                                            ValidationExpression="^[^\W\d_ ]{1,10}$" meta:resourcekey="RegularExpressionValidatorFirstNameResource1"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("FirstName") %>' meta:resourcekey="lblUserNameResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sur name" meta:resourcekey="TemplateFieldResource5">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUserSurnameu" runat="server" Text='<%# Bind("SurName") %>' meta:resourcekey="txtUserSurnameuResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSurName"
                                            runat="server"
                                            ControlToValidate="txtUserSurnameu"
                                            ErrorMessage="Enter sur name"
                                            ForeColor="White"
                                            Display="Dynamic" meta:resourcekey="RequiredFieldValidatorSurNameResource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorSurName"
                                            runat="server"
                                            ErrorMessage="Wrong sur name"
                                            ForeColor="White"
                                            ControlToValidate="txtUserSurnameu"
                                            ValidationExpression="^[^\W\d_ ]{1,20}$" meta:resourcekey="RegularExpressionValidatorSurNameResource1"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserSurname" runat="server" Text='<%# Bind("SurName") %>' meta:resourcekey="lblUserSurnameResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rights" meta:resourcekey="TemplateFieldResource6">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlUserRole" runat="server" DataSourceID="odsUserRole" DataTextField="Value" DataValueField="Value" meta:resourcekey="ddlUserRoleResource1">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserRole" runat="server" Text='<%# Bind("Role") %>' meta:resourcekey="lblUserRoleResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Phone" meta:resourcekey="TemplateFieldResource7">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPhoneu" runat="server" Text='<%# Bind("Phone") %>' meta:resourcekey="txtPhoneuResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone"
                                            runat="server"
                                            ControlToValidate="txtPhoneu"
                                            ErrorMessage="Enter phone"
                                            ForeColor="White"
                                            Display="Dynamic" meta:resourcekey="RequiredFieldValidatorPhoneResource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone"
                                            runat="server"
                                            ErrorMessage="Wrong phone"
                                            ForeColor="White"
                                            ControlToValidate="txtPhoneu"
                                            ValidationExpression="^\+([0-9]{12})$" meta:resourcekey="RegularExpressionValidatorPhoneResource1"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' meta:resourcekey="lblPhoneResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail" meta:resourcekey="TemplateFieldResource8">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMailu" runat="server" Text='<%# Bind("Mail") %>' meta:resourcekey="txtMailuResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMail"
                                            runat="server"
                                            ControlToValidate="txtMailu"
                                            ErrorMessage="Enter mail"
                                            ForeColor="White"
                                            Display="Dynamic" meta:resourcekey="RequiredFieldValidatorMailResource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorMail"
                                            runat="server"
                                            ErrorMessage="Wrong mail format"
                                            ForeColor="White"
                                            ControlToValidate="txtMailu"
                                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidatorMailResource1"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMail" runat="server" Text='<%# Bind("Mail") %>' meta:resourcekey="lblMailResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BirthDate" meta:resourcekey="TemplateFieldResource9">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBirthdayu" runat="server" Text='<%# Convert.ToDateTime(Eval("Birthday")).ToString("d") %>' meta:resourcekey="txtBirthdayuResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBirthDate"
                                            runat="server"
                                            ControlToValidate="txtBirthdayu"
                                            ErrorMessage="Enter birth date"
                                            ForeColor="White"
                                            Display="Dynamic" meta:resourcekey="RequiredFieldValidatorBirthDateResource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBirthDate"
                                            runat="server"
                                            ErrorMessage="Wrong birth date format"
                                            ForeColor="White"
                                            ControlToValidate="txtBirthdayu"
                                            ValidationExpression="^([0-9]{2})[.\/]([0-9]{2})[.\/]([0-9]{4})$" meta:resourcekey="RegularExpressionValidatorBirthDateResource1"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBirthday" runat="server" Text='<%# Convert.ToDateTime(Eval("Birthday")).ToString("d") %>' meta:resourcekey="lblBirthdayResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="About" meta:resourcekey="TemplateFieldResource10">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAboutu" runat="server" Text='<%# Bind("AboutMe") %>' meta:resourcekey="txtAboutuResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAbout" runat="server" Text='<%# Bind("AboutMe") %>' meta:resourcekey="lblAboutResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblSearchResults" runat="server" Text="SearchNoResults" meta:resourcekey="lblSearchResultsResource1"></asp:Label>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="gridHeader" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>

                    </div>
                    <asp:ObjectDataSource ID="odsUserRole" runat="server" SelectMethod="GetAllRoles" TypeName="HotCar.BLL.UsersManager"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsGetUsers" runat="server" TypeName="HotCar.BLL.UsersManager" DataObjectTypeName="HotCar.Entities.User" UpdateMethod="Update" SelectMethod="GetUsersByRole">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="all" Name="roles" Type="String" />
                        </SelectParameters>

                        <UpdateParameters>
                            <asp:Parameter Name="Birthday" Type="DateTime" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                    
                    <div class ="clear">
                    <button id="btnSendMsg" class="hiddenBtn" hidden="hidden">
                        <asp:Label ID="lblSendMessages" runat="server" Text="Send Messages" meta:resourcekey="lblSendMessagesResource1"></asp:Label></button>                                                      
                    <asp:Button runat="server" id="btnLockUsers" CssClass="hiddenBtn" hidden="hidden" Text="Lock" OnClick="btnLockUsers_Click" meta:resourcekey="btnLockUsersResource1"></asp:Button>
                    <asp:Button runat="server" id="btnUnlockUser" CssClass="hiddenBtn" hidden="hidden" Text="UnLock" OnClick="btnUnlockUser_Click" meta:resourcekey="btnUnlockUserResource1"></asp:Button>
                    </div>


                    <div id="SendMailDialog" title="Відправити повідомлення">
                        <label for="subject">Subject</label><br />
                        <input type="text" name="subject" id="subject" value="" style="width: 300px" /><br />
                        <label for="message">Message</label><br />
                        <textarea id="message" rows="5" style="width: 299px"></textarea>
                        <label id="valid-error" class="ui-state-error" hidden="hidden">Minimum length: 5 symbols</label>
                    </div>

                    <div class="clear">
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
</asp:Content>
