using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {

        private readonly string _resource;
        private readonly string _permission;

        public AuthorizeRoleAttribute(string resource, string permission)
        {
            _resource = resource;
            _permission = permission;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated) return false;

            var authService = DependencyResolver.Current.GetService<IAuthorizationService>();
            if (authService == null)
            {
                // Nếu chưa cấu hình DI, fail safe: deny access
                return false;
            }

            var username = httpContext.User.Identity.Name;
            return authService.HasPermission(username, _resource, _permission);
        }


        // cần fix lỗi query dẫn đến sai ???
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // chưa login -> redirect login
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            else
            {
                // đã login nhưng không có quyền -> 403
                filterContext.Result = new HttpStatusCodeResult(403, "Bạn không có quyền truy cập");
            }
        }

    }
}