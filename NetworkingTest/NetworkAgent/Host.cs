using System;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Net;
using System.Linq;

namespace NetworkAgent
{
    class Host : NetworkAgent
    {
        public Host(int newServerPort = 0) : base()
        {
            //Start listening for incoming connections
            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, newServerPort));

            //Print out the IPs and ports we are now listening on
            Console.WriteLine("\nServer listening for TCP connection on:");
            foreach (IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
        }

        public override void Run()
        {
            //Message to send
            Console.WriteLine("\nEnter a message to send all clients or \"quit\" and press ENTER:");

            string messageToSend;

            while ((messageToSend = Console.ReadLine()) != "quit")
            {
                if (!string.IsNullOrEmpty(messageToSend))
                {
                    foreach (Connection connection in NetworkComms.GetExistingConnection(ConnectionType.TCP))
                    {
                        connection.SendObject("Message", messageToSend);
                    }
                }
            }

            //The first available connection should become new host
            foreach (Connection connection in NetworkComms.GetExistingConnection(ConnectionType.TCP))
            {
                connection.SendObject("Config", NetworkComms.GetExistingConnection(ConnectionType.TCP).First().ConnectionInfo.RemoteEndPoint.ToString());
            }

            //We have used comms so we make sure to call shutdown
            NetworkComms.Shutdown();
        }

    }
}
