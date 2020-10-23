using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.CustomFilters
{
    public class JwtAuthorize : ActionFilterAttribute
    {

        public string Roles { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new RedirectToActionResult("LogIn", "Auth", null);
            }

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = httpClient.GetAsync("http://localhost:58546/api/auth/GetActiveUser").Result;

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                bool accessStatus=false;
                var activeUser = JsonConvert.DeserializeObject<AppUserViewModel>(responseMessage.Content.ReadAsStringAsync().Result);
                if (!string.IsNullOrWhiteSpace(Roles))
                {
                    if (Roles.Contains(","))
                    {
                        var acceptedRoles = Roles.Split(",");
                        foreach (var role in acceptedRoles)
                        {
                            if (activeUser.Roles.Contains(role))
                            {
                                accessStatus=true;
                            }
                        }
                    }
                    else
                    {
                        if (activeUser.Roles.Contains(Roles))
                        {
                            accessStatus=true;
                        }
                    }

                    if(!accessStatus){
                        context.Result= new RedirectToActionResult("AccessDenied","Auth",null);
                    }
                }
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.HttpContext.Session.Remove("token");
                context.Result=new RedirectToActionResult("SignIn","Auth",null);
            }
            else
            {
                var statusCode = responseMessage.StatusCode.ToString();
                context.HttpContext.Session.Remove("token");
                context.Result=new RedirectToActionResult("ApiError","Auth", new{code=statusCode});
            }

        }
    }
}