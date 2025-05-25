using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Management.Web.Helpers
{
    public static class HttpContextExtension
    {
        private const string redirectResponse = "SuccessRedirectedResponse";
        private const string redirectResponseError = "ErrorRedirectedResponse";

        public static bool IsMessageSuccess(this HttpContext context){
            if(context.Request.Cookies[redirectResponse] is not null)
                return true;

            return false;
        }

        public static string? GetMessageSuccess(this HttpContext context){
            string? response = null;
            if(context.Request.Cookies[redirectResponse] is not null){
                response = context.Request.Cookies[redirectResponse];
                context.Response.Cookies.Delete(redirectResponse);
            } 
            return response;
        }

        public static void AddMessageSuccess(this HttpContext context, string message){
            if(context.Request.Cookies[redirectResponse] is not null)
                context.Response.Cookies.Delete(redirectResponse);

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(1);

            context.Response.Cookies.Append(redirectResponse, message, options);
        }


        public static bool IsMessageError(this HttpContext context){
            if(context.Request.Cookies[redirectResponseError] is not null)
                return true;

            return false;
        }

        public static string? GetMessageError(this HttpContext context){
            string? response = null;
            if(context.Request.Cookies[redirectResponseError] is not null){
                response = context.Request.Cookies[redirectResponseError];

                context.Response.Cookies.Delete(redirectResponseError);
            } 
            return response;
        }


        public static void AddMessageError(this HttpContext context, string message){
            if(context.Request.Cookies[redirectResponseError] is not null)
                context.Response.Cookies.Delete(redirectResponseError);

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(1);

            context.Response.Cookies.Append(redirectResponseError, message, options);
        }
    }
}