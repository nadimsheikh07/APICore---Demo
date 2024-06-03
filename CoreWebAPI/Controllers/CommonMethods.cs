using System;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    public class CommonMethods : ControllerBase
    {
        /// <summary>
        /// get http request Id for response object.
        /// </summary>
        /// <returns>request id</returns>
        public Int32 getRequestId(HttpRequest request)
        {
            request.Headers.TryGetValue("rId", out var headerValue);
            return Convert.ToInt32(headerValue);
        }
        /// <summary>
        ///  get http Authentication Key for response object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string getAuthenticationKey(HttpRequest request)
        {
            request.Headers.TryGetValue("AuthenticationKey", out var headerValue);
            return Convert.ToString(headerValue);
        }

        public Int32 getRequestIdForRestAPI(HttpRequest request)
        {
            request.Headers.TryGetValue("rid", out var headerValue);
            return Convert.ToInt32(headerValue);
        }

        /// <summary>
        /// Common functions to get body parameters using expando object
        /// </summary>
        /// <param name="json"></param>
        public string getBodyParameters(ExpandoObject json, string key)
        {
            var keyValuePairs = ((System.Collections.Generic.IDictionary<string, object>)json);
            string value = "";
            foreach (var item in keyValuePairs)
            {
                if (item.Key == key)
                {
                    value = item.Value.ToString();
                }
            }
            return value;
        }
    }
}
