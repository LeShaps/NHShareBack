using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using NHShareBack.Exceptions;
using NHShareBack.Db;
using Newtonsoft.Json;

namespace NHShareBack
{
    class Treatment
    {
        public static async Task Dispatch(HttpMessageEventArgs e)
        {
            await Task.Run(() => e.Request.HttpMethod switch
            {
                "GET" => GetCollection(e),
                "POST" => PostCollection(e),
                "PUT" => PutItem(e),
                "PATCH" => PatchItem(e),
                "DELETE" => DeleteItem(e),
                _ => UnsupportedException(e)
            });
        }
        
        public static async Task GetCollection(HttpMessageEventArgs e)
        {
            string Author = e.Request.Cookies.GetCookie("Author");

            if (await Globals.Db.GetCollectionAsync(Author) is not Collection coll) {
                await e.Response.SendHttpResponseAsync("Error: Either you've never uploaded, either there's a problem here");
                return;
            }

            await e.Response.SendHttpResponseAsync($"Done: {coll.Name}");
        }

        public static async Task PostCollection(HttpMessageEventArgs e)
        {
            try
            {
                JObject RequestData = await e.Request.GetRequestJObjectAsync();
                User ToUploadUser = JsonConvert.DeserializeObject<User>($"{RequestData}");

                if (await Globals.Db.InitUserAsync(ToUploadUser)) {
                    await e.Response.SendHttpResponseAsync("Upload successeded");
                } else
                {
                    await e.Response.SendHttpResponseAsync("An error occured during the upload");
                }
            }
            catch (Exception ex) when (ex is InvalidRequestJsonException)
            {
                await e.Response.SendHttpResponseAsync("Sent Json was invalid");
            }
        }

        public static async Task PutItem(HttpMessageEventArgs e)
        {
        }

        public static async Task PatchItem(HttpMessageEventArgs e)
        {
        }

        public static async Task DeleteItem(HttpMessageEventArgs e)
        {
        }

        public static async Task UnsupportedException(HttpMessageEventArgs e)
        {
        }

        public static async Task GetAutorisations(HttpMessageEventArgs e)
        {
        }

        public static async Task UpdateAutorisations(HttpMessageEventArgs e)
        {
        }
    }
}
