using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YZM4215_Grup7.Extensions;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.CustomFilters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var activeUser = context.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            if (activeUser == null || context.HttpContext.Session.GetString("token")==null)
            {
                context.Result =new ViewResult();
            }
            else
            {
                if (activeUser.Roles == null)
                {
                    context.HttpContext.Session.Remove("activeUser");
                    context.Result = new ViewResult();
                }
                else
                {
                    foreach (var role in activeUser.Roles)
                    {
                        if (role.Contains("Admin"))
                        {
                            context.Result = new RedirectToActionResult("Index", "Home", new { @Area = "Admin" });
                        }
                        else
                        {
                            context.Result = new RedirectToActionResult("Index", "Home", new { @Area = "Member" });
                        }
                    }
                }

            }
        }
    }
}