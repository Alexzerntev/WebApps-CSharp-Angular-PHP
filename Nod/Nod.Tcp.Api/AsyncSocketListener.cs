using Nod.Bll.Interfaces;
using Nod.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Nod.Tcp.Api
{
    public class AsyncSocketListener
    {
        // Thread signal.  
        public ManualResetEvent allDone = new ManualResetEvent(false);

        private readonly IDeviceDataInsertionService _insertionService;
        public AsyncSocketListener(IDeviceDataInsertionService insertionService)
        {
            _insertionService = insertionService;
        }

        public void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[2048];


            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).LastOrDefault(); // IPv4 
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Console.WriteLine("Local address and port : {0}", localEndPoint.ToString());

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //Console.WriteLine("Closing the listener...");
            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                // Signal the main thread to continue.  
                allDone.Set();

                // Get the socket that handles the client request.  
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                // Create the state object.  
                State state = new State();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, State.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }
            catch (Exception e)
            {

                Console.WriteLine("Exeption " + e);
            }

        }

        private async void ReadCallback(IAsyncResult ar)
        {
            try
            {
                String content = String.Empty;

                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                State state = (State)ar.AsyncState;
                Socket handler = state.workSocket;

                // Read data from the client socket.   
                SocketError errorCode;
                int bytesRead = handler.EndReceive(ar, out errorCode);
                if (errorCode != SocketError.Success)
                {
                    bytesRead = 0;
                }

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  

                    state.sb.Clear();
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read   
                    // more data.
                    content = state.sb.ToString();

                    if (content.Contains("$"))
                    {
                        content = content.Substring(content.IndexOf('$'));
                    }
                    else
                    {
                        content = "";
                    }

                    if (content != "")
                    {
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                        var returnedConnectionAttribute = await _insertionService.InsertAsync(content, state.connectionAttribute);
                        if (returnedConnectionAttribute != null)
                        {
                            state.connectionAttribute = returnedConnectionAttribute;
                        }

                        if (state.connectionAttribute?.QualityOfService == 1)
                        {
                            Send(handler, Constants.Acknowledge);
                        }
                    }

                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, State.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                    
                }
                else
                {
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    state = null;
                    GC.Collect();
                    Console.WriteLine("Connection terminated");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exeption " + e);
            }
        }

        private void Send(Socket handler, String data)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.  
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.  
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            }
            catch (Exception e)
            {

                Console.WriteLine("Exeption " + e);
            }

        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
