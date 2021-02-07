using System;
using System.Net;

namespace NHShareBack
{
    class HttpMessageEventArgs : EventArgs
    {
        public HttpListenerRequest Request;
        public HttpListenerResponse Response;
    }
}