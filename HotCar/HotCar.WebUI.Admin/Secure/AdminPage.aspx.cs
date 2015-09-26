using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Security.AccessControl;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotCar.BLL;
using HotCar.Entities;
using HotCar.Entities.Enums;
using HotCar.Repositories.Sql;
using HotCar.WebUI.Admin.Code;
using HotCar.WebUI.Admin.Code.Keys;

namespace HotCar.WebUI.Admin.Secure
{
    public partial class AdminPage : Page
    {
        #region ASP.NET Page Life Cycle Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                email.Text = Mail.MailLogin;
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    this.lblUsersListName.Text = "All users";
                }
                else
                {
                    this.lblUsersListName.Text = "Усі користувачі";
                }
            }
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session[SessionKeys.BIND_METHOD] != null)
            {
                this.odsGetUsers.SelectMethod = (string)Session[SessionKeys.BIND_METHOD];
            }
            this.GridViewUsers.DataBind();
        }

        #endregion

        #region GridView Events

        protected void GridViewUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridViewUsers.PageIndex = e.NewPageIndex;                
        }

        protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = this.GridViewUsers.Rows[e.NewEditIndex];
            UserRoles editRole = (UserRoles)(Enum.Parse(typeof(UserRoles),
                ((Label)row.FindControl("lblUserRole")).Text));

            if (User.IsInRole(UserRoles.Administrator.ToString()))
            {
                if (editRole == UserRoles.Administrator || editRole == UserRoles.Master)
                {
                    String permission = "";
                    String alert = "";
                    if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                    {
                        permission = "Rights restriction";
                        alert = "You do not have appropriate rights";
                    }

                    else
                    {
                        permission = "Обмеження прав";
                        alert = "У вас немає належних прав";
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), permission, "<script>alert('" + alert + "');</script>", false);

                    e.NewEditIndex = -1;
                }

                else
                {
                    this.GridViewUsers.EditIndex = e.NewEditIndex;
                }
            }

            else
            {
                this.GridViewUsers.EditIndex = e.NewEditIndex;
            }
        }

        protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var user = new User();
            GridViewRow row = this.GridViewUsers.Rows[e.RowIndex];

            user.Id = Convert.ToInt32(((Label)this.GridViewUsers.Rows[e.RowIndex].FindControl("lblUserIdu")).Text);
            user.Login = ((Label)this.GridViewUsers.Rows[e.RowIndex].FindControl("lblUserLoginu")).Text;
            user.FirstName = ((TextBox)this.GridViewUsers.Rows[e.RowIndex].FindControl("txtUserNameu")).Text;
            user.SurName = ((TextBox)this.GridViewUsers.Rows[e.RowIndex].FindControl("txtUserSurnameu")).Text;
            user.Role = (UserRoles)(Enum.Parse(typeof(UserRoles),
                ((DropDownList)this.GridViewUsers.Rows[e.RowIndex].FindControl("ddlUserRole")).SelectedValue));
            user.Phone = ((TextBox)this.GridViewUsers.Rows[e.RowIndex].FindControl("txtPhoneu")).Text;
            user.Mail = ((TextBox)this.GridViewUsers.Rows[e.RowIndex].FindControl("txtMailu")).Text;
            user.Birthday = Convert.ToDateTime(((TextBox)this.GridViewUsers.Rows[e.RowIndex].FindControl("txtBirthdayu")).Text);          
            user.AboutMe = ((TextBox)this.GridViewUsers.Rows[e.RowIndex].FindControl("txtAboutu")).Text;

            var role = User.IsInRole(UserRoles.Administrator.ToString()) ?
               UserRoles.Administrator : UserRoles.Master;

            if ((int) role <= (int) user.Role)
            {
                String permission = String.Empty;
                String alert = String.Empty;
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    permission = "Rights restriction";
                    alert = "You do not have appropriate rights";
                }

                else
                {
                    permission = "Обмеження прав";
                    alert = "У вас немає належних прав";
                }
                
                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('" + alert + "');</script>", false);
                this.GridViewUsers.EditIndex = -1;  
            }
            else
            {
                var usersManager = new UsersManager(new UserRepository(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString));
                usersManager.UpdateUserInfo(user);

                this.GridViewUsers.EditIndex = -1; 
                
            }                                                      
        }

        protected void GridViewUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridViewUsers.EditIndex = -1;           
        }

        protected void ddlGridViewPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GridViewUsers.PageSize = Convert.ToInt32(this.ddlGridViewPageSize.Items[this.ddlGridViewPageSize.SelectedIndex].Value);
        }

        #endregion

        #region Select Users Events

        protected void btnAdmins_Click(object sender, EventArgs e)
        {
            this.odsGetUsers.SelectParameters.Clear();
            Session[SessionKeys.BIND_METHOD] = "GetUsersByRole";
            this.odsGetUsers.SelectParameters.Add("roles", TypeCode.String, UserRoles.Administrator.ToString());
            
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
            {
                this.lblUsersListName.Text = "Administrators";
            }
            
            else
            {
                this.lblUsersListName.Text = "Адміністратори";
            }

        }

        protected void btnSimpleUsers_Click(object sender, EventArgs e)
        {
            Session[SessionKeys.BIND_METHOD] = "GetUsersByRole";
            this.odsGetUsers.SelectParameters.Clear();
            this.odsGetUsers.SelectParameters.Add("roles", TypeCode.String, UserRoles.User.ToString());

            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
            {
                this.lblUsersListName.Text = "Common users";
            }

            else
            {
                this.lblUsersListName.Text = "Прості користувачі";
            }
        }

        protected void btnInactiveUsers_Click(object sender, EventArgs e)
        {
            Session[SessionKeys.BIND_METHOD] = "GetUsersByRole";
            this.odsGetUsers.SelectParameters.Clear();
            this.odsGetUsers.SelectParameters.Add("roles", TypeCode.String, UserRoles.Inactive.ToString());

            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
            {
                this.lblUsersListName.Text = "Inactive users";
            }

            else
            {
                this.lblUsersListName.Text = "Неактивні користувачі";
            }
        }

        protected void btnAllUsers_Click(object sender, EventArgs e)
        {
            Session[SessionKeys.BIND_METHOD] = "GetUsersByRole";
            this.odsGetUsers.SelectParameters.Clear();
            this.odsGetUsers.SelectParameters.Add("roles", TypeCode.String, "all");

            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
            {
                this.lblUsersListName.Text = "All users";
            }

            else
            {
                this.lblUsersListName.Text = "Усі користувачі";
            }
        }

        #endregion

        #region WebMethods

        [WebMethod]
        public static string SendMail(string[] receivers, string subject, string message)
        {
            if (receivers.Length != 0)
            {
                foreach (var receiver in receivers)
                {
                    Mail.Send(to: receiver, subject: subject, message: message);
                }
                return "Message was sent";
            }
            
            return "Failed to sent message!";
        }

        [WebMethod]
        public static string GetInputMessagInfo()
        {
            return Mail.GetMessageStatus();
        }

        #endregion WebMethods

        #region Helpers

        private void GridViewBinding(List<User> users )
        {
            this.GridViewUsers.DataSource = users;
            this.GridViewUsers.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session[SessionKeys.BIND_METHOD] = "SearchUsers";
            this.odsGetUsers.SelectParameters.Clear();
            this.odsGetUsers.SelectParameters.Add("searchRequest", TypeCode.String, this.txtSearch.Text);         
        }
     
        protected override void InitializeCulture()
        {
            if (this.Session[SessionKeys.UI_CULTURE] == null)
            {
                this.Session[SessionKeys.UI_CULTURE] = "uk";
            }

            this.UICulture = (String)this.Session[SessionKeys.UI_CULTURE];
            base.InitializeCulture();
        }

        protected void btnLockUsers_Click(object sender, EventArgs e)
        {
            var manager = new UsersManager(new UserRepository(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString));
            var role = User.IsInRole(UserRoles.Administrator.ToString()) ?
                UserRoles.Administrator : UserRoles.Master;

            manager.UsersLock(CheckedList(), role);          
        }

        protected void btnUnlockUser_Click(object sender, EventArgs e)
        {
            var manager = new UsersManager(new UserRepository(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString));           
            manager.UsersUnlock(CheckedList());
            
        }
        
        private IDictionary<int, string> CheckedList()
        {
            var checkedUsers = new Dictionary<int, string>();

            foreach (GridViewRow row in this.GridViewUsers.Rows)
            {
                var chBox = ((CheckBox)row.FindControl("chkRow"));
                if (chBox.Checked)
                {
                    int uId = Convert.ToInt32(((Label)row.FindControl("lblUserId")).Text);
                    string uRole = ((Label)row.FindControl("lblUserRole")).Text;
                    checkedUsers.Add(uId, uRole);
                    chBox.Checked = false;
                }
            }

            return checkedUsers;
        }

        #endregion
    }
}