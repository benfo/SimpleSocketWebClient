using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketWebClient
{
    public class TcpSocketRequestRunner : IRequestRunner
    {
        private const int ReceiveBufferSize = 256;
        private const int ReceiveTimeoutInSeconds = 10;

        public HttpResponse Execute(HttpRequest httpRequest)
        {
            using (Socket tcpSocket = CreateTcpSocket(httpRequest.Url))
            {
                if (tcpSocket == null)
                    throw new Exception("Failed to created a connection.");

                SendRequest(tcpSocket, httpRequest);

                string result = ReceiveResponseAsString(tcpSocket);

                return new HttpResponse
                {
                    Content = result
                };
            }
        }

        private static void SendRequest(Socket tcpSocket, HttpRequest httpRequest)
        {
            var requestBuilder = new SocketHttpRequestBuilder(httpRequest.Method, httpRequest.Url);
            string requestString = requestBuilder.Build();
            byte[] request = Encoding.ASCII.GetBytes(requestString);

            tcpSocket.Send(request, request.Length, 0);
        }

        private static Socket CreateTcpSocket(Uri uri)
        {
            var hostEntry = Dns.GetHostEntry(uri.Host);

            foreach (var ipAddress in hostEntry.AddressList)
            {
                var port = uri.Port;
                var ipEndPoint = new IPEndPoint(ipAddress, port);
                var tcpSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tcpSocket.ReceiveTimeout = ReceiveTimeoutInSeconds * 1000;

                tcpSocket.Connect(ipEndPoint);

                if (tcpSocket.Connected)
                    return tcpSocket;
            }

            return null;
        }

        private static string ReceiveResponseAsString(Socket tcpSocket)
        {
            var contentBuilder = new StringBuilder();
            var receiveBuffer = new Byte[ReceiveBufferSize];

            while (true)
            {
                var numberOfBytesReceived = tcpSocket.Receive(receiveBuffer, ReceiveBufferSize, SocketFlags.None);
                contentBuilder.Append(Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesReceived));

                if (numberOfBytesReceived == 0)
                    break;
            }

            return contentBuilder.ToString();
        }
    }
}