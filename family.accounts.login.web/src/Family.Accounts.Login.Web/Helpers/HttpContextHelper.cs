using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Login.Web.Helpers
{
    public static class HttpContextHelper
    {
        private const string redirectSuccessResponseKey = "redirectSuccessResponse";
        private const string redirectErrorResponseKey = "redirectErrorResponse";

        public static bool IsSuccessResponse(this HttpContext context)
        {
            if (context.Request.Cookies[redirectSuccessResponseKey] != null)
                return true;

            return false;
        }

        public static void SetSuccessResponse(this HttpContext context, string message)
        {

            if (context.Request.Cookies[redirectSuccessResponseKey] != null)
            {
                context.Response.Cookies.Delete(redirectSuccessResponseKey);
            }

            context.Response.Cookies.Append(redirectSuccessResponseKey, message, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1),
            });
        }

        public static void SetErrorResponse(this HttpContext context, string message)
        {
            if (context.Request.Cookies[redirectErrorResponseKey] != null)
            {
                context.Response.Cookies.Delete(redirectErrorResponseKey);
            }

            context.Response.Cookies.Append(redirectErrorResponseKey, message, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1),
            });
        }

        public static string GetSuccessResponse(this HttpContext context)
        {
            if (context.Request.Cookies[redirectSuccessResponseKey] != null)
            {
                var message = context.Request.Cookies[redirectSuccessResponseKey];
                context.Response.Cookies.Delete(redirectSuccessResponseKey);
                return message;
            }

            return string.Empty;
        }

        public static string GetErrorResponse(this HttpContext context)
        {
            if (context.Request.Cookies[redirectErrorResponseKey] != null)
            {
                var message = context.Request.Cookies[redirectErrorResponseKey];
                context.Response.Cookies.Delete(redirectErrorResponseKey);
                return message;
            }

            return string.Empty;
        }

        public static bool IsErrorResponse(this HttpContext context)
        {
            if (context.Request.Cookies[redirectErrorResponseKey] != null)
                return true;

            return false;
        }
    }
}