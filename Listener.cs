using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NHShareBack
{
    class Listener
    {
        private Thread _listenerThread;
        private HttpListener _listener;

        public event Func<HttpMessageEventArgs, Task> OnMessageReceived;
        public event Func<Task> OnStartListening;
        public event Func<Task> OnStopListening;
        public event Func<HttpMessageEventArgs, Task> OnPostResponse;

        public Listener()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:3009/");
            _listener.Prefixes.Add("http://localhost:3009/Config/");

            _listenerThread = new Thread(new ThreadStart(Loop));
        }

        public void StartListening()
        {
            _listener.Start();
            _listenerThread.Start();
            OnStart(EventArgs.Empty);
        }

        public void StopListening()
        {
            _listener.Stop();
            OnStop(EventArgs.Empty);
        }

        /* Main Loop */
        private async void Loop()
        {
            while (_listener.IsListening)
            {
                HttpListenerContext Context = await _listener.GetContextAsync();
                HttpListenerRequest Request = Context.Request;
                HttpListenerResponse Response = Context.Response;

                HttpMessageEventArgs Event = new HttpMessageEventArgs
                {
                    Request = Request,
                    Response = Response
                };

                _ = OnMessageAsync(Event).ConfigureAwait(false);
            }
        }

        /* Event dispachers */
        protected virtual async Task OnMessageAsync(HttpMessageEventArgs e)
        {
            await OnMessageReceived?.Invoke(e);
        }

        protected virtual async Task OnPostProcess(HttpMessageEventArgs e)
        {
            await OnPostResponse?.Invoke(e);
        }

        protected virtual void OnStart(EventArgs e)
        {
            OnStartListening?.Invoke();
        }

        protected virtual void OnStop(EventArgs e)
        {
            OnStopListening?.Invoke();
        }
    }
}
