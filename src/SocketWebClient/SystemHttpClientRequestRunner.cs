namespace SocketWebClient
{
    public class SystemHttpClientRequestRunner : IRequestRunner
    {
        public HttpResponse Execute(HttpRequest request)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var httpResponseMessage = httpClient.GetAsync(request.Url).Result;

                return new HttpResponse
                {
                    Content = httpResponseMessage.Content.ReadAsStringAsync().Result
                };
            }
        }
    }
}