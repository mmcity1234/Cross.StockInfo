using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Network
{
    public static class RestApi
    {
        public static async Task<string> GetHtmlTaskAsync(string url)
        {
          return await GetHtmlTaskAsync(url, Encoding.Default);
        }
        public static async Task<string> GetHtmlTaskAsync(string url, Encoding encoding)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Content-Type", "text/html; charset=utf-8"}
            };
 
            return await GetContentTaskAsync(url, headers, encoding);
        }

        public static async Task<string> GetJsonTaskAsync(string url)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"Accept", "application/json" },
                { "Content-Type", "application/json; charset=UTF-8"}
            };
            return await GetContentTaskAsync(url, headers, Encoding.Default);

        }

        public static async Task<string> GetContentTaskAsync(string url, Dictionary<string, string> headers, Encoding encoding)
        {
            var request = new RestRequest(Method.GET);
            if(headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            return await ExecuteStringTaskAsync(new Uri(url), request, encoding);         
        }

        /// <summary>
        /// Get rest api with task and auto deserialize to objet model
        /// </summary>
        /// <param name="bseeUri">e.g. http://xxxx.com.tw/v1/api</param>
        /// <param name="path">rest api resource path. e.g. dashboard/{id}</param>
        public static async Task<T> GetTaskAsync<T>(string url) 
        {
            var request = new RestRequest(Method.GET);
            
            // easily add HTTP Headers
            // request.AddHeader("header", "value");            
            return await ExecuteTaskAsync<T>(new Uri(url), request);
        }

       // public static Task<<string> GetAsync(string url) { }

        public static async Task<T> ExecuteTaskAsync<T>(Uri uri, RestRequest request) 
        {
            return await ExecuteTaskAsync<T>(uri, request, null, null);
        }

        public static async Task<T> ExecuteTaskAsync<T>(Uri uri, RestRequest request, string account, string password) 
        {
            var client = new RestClient();
            client.BaseUrl = uri;
            // account info
            if (account != null && password != null)
                client.Authenticator = new HttpBasicAuthenticator(account, password);

            var response = await client.ExecuteTaskAsync<T>(request);
            
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }
            return response.Data;
        }

        public static async Task<string> ExecuteStringTaskAsync(Uri uri, RestRequest request, Encoding encoding)
        {
            var client = new RestClient(uri);        
            // account info
            //if (account != null && password != null)
            //    client.Authenticator = new HttpBasicAuthenticator(account, password);
            
            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                string message = String.Format("Error retrieving response with status code {0}. Check inner details for more info.", response.StatusCode);
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }

            return encoding.GetString(response.RawBytes);         
        }


        public static async Task<string> PostContentTaskAsync(string url, Dictionary<string, string> parameters, Encoding encoding)
        {
            var request = new RestRequest(Method.POST);
            foreach (var param in parameters)
            {
                request.AddParameter(param.Key, param.Value);
            }

            return await ExecuteStringTaskAsync(new Uri(url), request, encoding);
            
        }
           
    }
}
