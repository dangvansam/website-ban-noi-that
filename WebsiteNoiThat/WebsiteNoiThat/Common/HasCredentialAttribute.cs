using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Common;

namespace WebsiteNoiThat.Common
{
   

        public class HasCredentialAttribute : AuthorizeAttribute
        {
            public string RoleId { get; set; }
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var session = (WebsiteNoiThat.UserLogin)HttpContext.Current.Session[Common.Commoncontent.user_sesion_admin];
                if (session == null)
                {
                    return false;
                }

                List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.Username); // Call another method to get rights of the user from DB

                if (privilegeLevels.Contains(this.RoleId) || session.GroupId == CommonConstant.ADMIN_GROUP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
                };
            }
            private List<string> GetCredentialByLoggedInUser(string userName)
            {
                var credentials = (List<string>)HttpContext.Current.Session[Common.Commoncontent.SESSION_CREDENTIALS];
                return credentials;
            }
        }
    }
