using Nod.Model.Entities;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Nod.Tcp.Api
{
    public class State
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 4096;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
        // Connection attributes
        public ConnectionAttribute connectionAttribute;
    }
}
