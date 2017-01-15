using System;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Net;

namespace RearEndCollision
{
	public class NetworkCommunicator : ICommandReciever, ICommandGenerator
	{
        private Command currentCommand;

        public string HostIp { get; private set; }

        public int HostPort { get; private set; }

        public char[,] GetMap()
		{
			throw new NotImplementedException();
		}

		public void PushCommand(Command cmd)
        {
            NetworkComms.SendObject("Command", this.HostIp, this.HostPort, cmd);
        }

		public void Connect(string hostIp, int hostPort)
        {
            this.HostIp = hostIp;
            this.HostPort = hostPort;
            NetworkComms.AppendGlobalIncomingPacketHandler<Command>("Command", ProcessCommand);
        }

		public void Host(int newHostPort = 0)
		{
            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, newHostPort));
            
            Console.WriteLine("\nServer listening for TCP connection on:");
            foreach (IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
            }

            NetworkComms.AppendGlobalIncomingPacketHandler<Command>("Command", ProcessCommandAsHost);
        }

        private void ProcessCommandAsHost(PacketHeader packetHeader, Connection connection, Command cmd)
        {
            foreach (Connection hostConnection in NetworkComms.GetExistingConnection(ConnectionType.TCP))
            {
                connection.SendObject("Command", cmd);
            }
        }

        public void ProcessCommand(PacketHeader header, Connection connection, Command cmd)
        {
            this.currentCommand = cmd;
        }

        public Command GetCommand()
        {
            return this.currentCommand;
        }
    }
}
