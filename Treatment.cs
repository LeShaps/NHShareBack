using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NHShareBack
{
    class Treatment
    {
        public static async Task Dispatch(HttpMessageEventArgs e)
        {
            _ = Task.Run(() => e.Request.HttpMethod switch
            {
                "GET" => GetCollection(e),
                "POST" => PostCollection(e),
                "PUT" => PutItem(e),
                "PATCH" => PatchItem(e),
                "DELETE" => DeleteItem(e),
                _ => UnsupportedException(e)
            });

            await e.Response.SendHttpResponseAsync("OK");
        }

        public static async Task GetCollection(HttpMessageEventArgs e)
        {
        }

        public static async Task PostCollection(HttpMessageEventArgs e)
        {
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
    }
}
