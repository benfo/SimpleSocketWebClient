using System;
using System.Text;

namespace SocketWebClient
{
    public class SocketHttpRequestBuilder
    {
        public SocketHttpRequestBuilder(Method method, Uri url)
        {
            Method = method;
            Url = url;
        }

        public Method Method { get; private set; }

        public Uri Url { get; private set; }

        public string Build()
        {
            var requestBuilder = new StringBuilder();
            requestBuilder.AppendFormat("{0} {1} HTTP/1.1", Method, Url);
            requestBuilder.AppendLine();
            requestBuilder.AppendFormat("Host: {0}", Url.Host);
            requestBuilder.AppendLine();
            requestBuilder.AppendLine("Connection: Close");
            return requestBuilder.ToString();
        }
    }
}