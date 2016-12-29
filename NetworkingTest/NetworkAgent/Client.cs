using System;
using System.Linq;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Threading;

namespace NetworkAgent
{
    public class Client : NetworkAgent
    {
        private const int MIN_PORT = 49152;
        private const int MAX_PORT = 65535;
        private string serverIP;
        private int serverPort;

        public event EventHandler RestartAsHostEventHandler;

        public Client(string serverSocket) : base()
        {
            //Parse the necessary information out of the provided string
            this.serverIP = serverSocket.Split(':').First();
            this.serverPort = int.Parse(serverSocket.Split(':').Last());
            
            //Trigger the method PrintIncomingMessage when a packet of type 'Config' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Config", UpdateConfig);
        }

        public override void Run()
        {
            //Message to send
            Console.WriteLine("\nEnter a message to send to host or \"quit\" and press ENTER:");

            string messageToSend;

            while ((messageToSend = Console.ReadLine()) != "quit")
            {
                if (!string.IsNullOrEmpty(messageToSend))
                {
                    //Send the message in a single line
                    NetworkComms.SendObject("Message", serverIP, serverPort, messageToSend);
                }
            }

            //We have used comms so we make sure to call shutdown
            NetworkComms.Shutdown();
            }

        private void UpdateConfig(PacketHeader header, Connection connection, string message)
        {
            string incomingSocketAddress = message.Split(':').First();
            int incomingSocketPort = int.Parse(message.Split(':').Last());
            int newSocketPort = incomingSocketPort - (incomingSocketPort % 2 == 0 ? -9 : 9);
            newSocketPort = Math.Min(newSocketPort, MAX_PORT);
            newSocketPort = Math.Max(newSocketPort, MIN_PORT);

            if (message == connection.ConnectionInfo.LocalEndPoint.ToString())
            {
                ResetAsHostEventArgs resetEventArgs = new ResetAsHostEventArgs();
                resetEventArgs.NewHostPort = newSocketPort;
                RestartAsHost(resetEventArgs);
            }
            else
            {
                this.serverIP = incomingSocketAddress;
                this.serverPort = newSocketPort;
            }
        }

        protected virtual void RestartAsHost(EventArgs e)
        {
            EventHandler handler = RestartAsHostEventHandler;
            NetworkComms.RemoveGlobalIncomingPacketHandler();
            NetworkComms.CloseAllConnections();

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
