using System;

namespace SocketWebClient
{
    public class HttpRequest : IEquatable<HttpRequest>
    {
        public Method Method { get; set; }

        public Uri Url { get; set; }

        public bool Equals(HttpRequest other)
        {
            return Method == other.Method && Url == other.Url;
        }
    }
}