using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace ProductWebApi.Authentication
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = GenerateUnauthorizedResponse(actionContext);
            }
            else
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                try
                {
                    var authDetail = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken))?.Split(':');

                    if (UserHelper.Validate(authDetail[0], authDetail[1]))
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(authDetail[0]), null);
                    }
                    else
                    {
                        actionContext.Response = GenerateUnauthorizedResponse(actionContext);
                    }
                }
                catch(Exception ex)
                {
                    actionContext.Response = GenerateUnauthorizedResponse(actionContext, ex.Message);
                }
            }
        }

        private HttpResponseMessage GenerateUnauthorizedResponse(HttpActionContext actionContext, string message=null)
        {
            return string.IsNullOrEmpty(message) ?
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized) :
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, message);
        }
    }
}