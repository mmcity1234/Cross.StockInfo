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
        public static async Task<string> GetTaskAsyncContent(string url)
        {
            var request = new RestRequest(Method.GET);

            var client = new RestClient(url);          

            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }
            return response.Content;
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
    }
}
