using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NHShareBack
{
    class Program
    {
        readonly Listener Server = new Listener();

        static async Task Main(string[] args)
        {
            try
            {
                await new Program().MainAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (Debugger.IsAttached)
                    throw;
                Console.WriteLine(e.Message);
            }
        }

        public async Task MainAsync()
        {
            Server.OnMessageReceived += Treatment.Dispatch;
            Server.OnStartListening += DisplayStart;
            Server.StartListening();

            await Task.Delay(-1).ConfigureAwait(false);
        }

        public Task DisplayStart()
        {
            Console.WriteLine("Start listening");
            return Task.CompletedTask;
        }
    }
}
