using System;

namespace SocketWebClient
{
    public class HttpClient
    {
        private readonly IRequestRunner _requestRunner;

        public HttpClient(IRequestRunner requestRunner)
        {
            _requestRunner = requestRunner;
        }

        public HttpResponse Get(string url)
        {
            if (url == null) throw new ArgumentNullException("url");

            var request = new HttpRequest
            {
                Url = new Uri(url),
                Method = Method.GET
            };

            return _requestRunner.Execute(request);
        }
    }
}