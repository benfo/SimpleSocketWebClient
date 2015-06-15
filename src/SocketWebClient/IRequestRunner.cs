namespace SocketWebClient
{
    public interface IRequestRunner
    {
        HttpResponse Execute(HttpRequest request);
    }
}