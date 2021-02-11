using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using NHShareBack.Exceptions;
using System.Linq;

namespace NHShareBack
{
    public static class HttpListenerExtension
    {
        public static async Task<string> GetResponseTextAsync(this HttpListenerRequest Request)
        {
            using var reader = new StreamReader(Request.InputStream, Request.ContentEncoding);
            return await reader.ReadToEndAsync();
        }

        public static async Task<JObject> GetRequestJObjectAsync(this HttpListenerRequest Request)
        {
            using var reader = new StreamReader(Request.InputStream, Request.ContentEncoding);
            try
            {
                return JsonConvert.DeserializeObject<JObject>(await reader.ReadToEndAsync());
            }
            catch (Exception)
            {
                throw new InvalidRequestJsonException();
            }
        }

        public static Task SendHttpResponseAsync(this HttpListenerResponse Response, string Content)
        {
            byte[] Buffer = Encoding.UTF8.GetBytes(Content);

            Response.ContentLength64 = Buffer.Length;
            Stream Output = Response.OutputStream;
            Output.Write(Buffer, 0, Buffer.Length);

            Output.Close();

            return Task.CompletedTask;
        }

        public static string GetCookie(this CookieCollection Cookies, string Name) {
            if (Cookies.Count == 0) return null;
            if (Cookies.Where(x => x.Name == Name).FirstOrDefault() is not Cookie cookie)
                return null;

            return cookie.Value;
        }
    }
}
