using NUnit.Framework;
using System;
using System.Net;

namespace SocketWebClient.Tests.Integration
{
    public class SocketRequestRunnerTests
    {
        private const string BaseUrl = "http://localhost:8080/test/";
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
            //runner = new SocketRequestRunner();
            runner = new SystemHttpClientRequestRunner();
        }

        [Test]
        public void Should_download_content()
        {
            var request = new HttpRequest { Method = Method.GET, Url = new Uri(BaseUrl) };

            var response = runner.Execute(request);

            Assert.That(response.Content, Is.EqualTo(ResponseContent));
        }
    }
}