using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
    public class KeyboardInputSource
    {
        static private KeyboardInputSource keyboardInputSourceInstance = null;
        static public KeyboardInputSource GetKeyboardInputSource()
        {
            if (keyboardInputSourceInstance == null)
            {
                keyboardInputSourceInstance = new KeyboardInputSource();
            }
            return keyboardInputSourceInstance;
        }

        HashSet<ConsoleKey> waitingKeys;
        private KeyboardInputSource()
        {
            waitingKeys = new HashSet<ConsoleKey>();
        }
        public bool HasKeyBeenPressed(ConsoleKey key)
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo readKey = Console.ReadKey(true);
                waitingKeys.Add(readKey.Key);
            }

            if (waitingKeys.Contains(key))
            {
                waitingKeys.Remove(key);
                return true;
            }

            return false;
        }


    }
}
