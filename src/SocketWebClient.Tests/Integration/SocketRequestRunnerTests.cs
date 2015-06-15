using NUnit.Framework;
using System;
using System.Net;

namespace SocketWebClient.Tests.Integration
{
    public class SocketRequestRunnerTests
    {
        private const string BaseUrl = "http://localhost:9001/";
        private const string ResponseContent = "<h1>content</h1>";

        private IRequestRunner runner;
        private WebServer webServer;

        [TestFixtureSetUp]
        public void Init()
        {
            webServer = new WebServer(SendResponse, BaseUrl);
            webServer.Run();
        }

        private static string SendResponse(HttpListenerRequest request)
        {
            return ResponseContent;
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            webServer.Stop();
        }

        [SetUp]
        public void Before_each_test()
        {
            runner = new TcpSocketRequestRunner();
        }

        [Test]
        public void Should_download_content_when_doing_a_get_request()
        {
            var request = new HttpRequest { Method = Method.GET, Url = new Uri(BaseUrl) };

            var response = runner.Execute(request);

            Assert.That(response.Content, Is.StringEnding(ResponseContent));
        }
    }
}