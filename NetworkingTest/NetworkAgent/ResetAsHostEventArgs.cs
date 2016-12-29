using System;

namespace NetworkAgent
{
    class ResetAsHostEventArgs : EventArgs
    {
        public int NewHostPort { get; set; }
    }
}
