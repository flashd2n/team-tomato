using System;
using System.Security.Permissions;
using System.Threading;

namespace NetworkAgent
{
    class NetworkAgents
    {
        static Thread currThread;
        static Client client;
        static Host host;

        static void Main()
        {
            Console.WriteLine("Type \"c\" for client or \"h\" for host and press ENTER:");

            switch (Console.ReadLine())
            {
                case "c":
                    //Request server IP and port number
                    Console.WriteLine("Please enter the server socket (IP and port in the format 192.168.0.1:10000) and press ENTER:");
                    string serverInfo = Console.ReadLine();
                    client = new Client(serverInfo);
                    client.RestartAsHostEventHandler += RestartAsHost;
                    currThread = new Thread(new ThreadStart(client.Run));
                    currThread.Start();
                    break;
                case "h":
                    host = new Host();
                    host.Run();
                    break;
                default:
                    Console.WriteLine("Wrong Input");
                    break;
            }
        }


        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        static void RestartAsHost(object sender, EventArgs e)
        {
            host = new Host((e as ResetAsHostEventArgs).NewHostPort);
            Thread newThread = new Thread(new ThreadStart(host.Run));
            newThread.Start();

            try
            {
                currThread.Abort();
            }
            catch (ThreadAbortException ex)
            {
            }
        }
    }
}
