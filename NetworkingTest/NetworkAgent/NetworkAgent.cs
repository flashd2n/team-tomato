using System;
using System.Net;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace NetworkAgent
{
    public abstract class NetworkAgent
    {
        public NetworkAgent()
        {
            //Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);
        }

        public abstract void Run();

        private void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            Console.WriteLine("[" + connection.ConnectionInfo.RemoteEndPoint.ToString() + "]:" + message);
        }
    }
}
