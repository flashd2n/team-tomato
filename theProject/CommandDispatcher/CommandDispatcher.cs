using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class CommandDispatcher
	{
        private List<ICommandGenerator> commangGenerators;
        private List<ICommandReciever> commandRecievers;

        public CommandDispatcher()
        {
            this.commangGenerators = new List<ICommandGenerator>();
            this.commandRecievers = new List<ICommandReciever>();
        }

        public void AddCommandGenerator(ICommandGenerator commandGenerator)
        {
            this.commangGenerators.Add(commandGenerator);
        }

        public void AddCommandReciever(ICommandReciever commandReciever)
        {
            this.commandRecievers.Add(commandReciever);
        }

		public void DispatchCommands()
		{
            foreach (ICommandGenerator cg in commangGenerators)
            {
                Command c = cg.GetCommand();
                foreach (ICommandReciever cr in commandRecievers)
                {
                    cr.PushCommand(c);
                }
            }
		}
	}
}
