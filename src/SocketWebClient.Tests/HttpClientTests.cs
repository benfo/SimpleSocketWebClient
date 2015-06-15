using Moq;
using NUnit.Framework;
using System;

namespace SocketWebClient.Tests
{
    public class HttpClientTests
    {
        private HttpClient client;
        private Mock<IRequestRunner> requestRunner;

        [SetUp]
        public void Before_each_test()
        {
            requestRunner = new Mock<IRequestRunner>();
            client = new HttpClient(requestRunner.Object);
        }

        [Test]
        public void Get_should_thown_an_exception_for_a_null_url()
        {
            Assert.Throws<ArgumentNullException>(() => client.Get(null));
        }

        [Test]
        public void Should_return_content_when_making_a_get_request()
        {
            const string url = "http://www.test.com/";
            const Method method = Method.GET;
            const string content = "content";
            var request = new HttpRequest { Method = method, Url = new Uri(url) };

            requestRunner
                .Setup(r => r.Execute(It.Is<HttpRequest>(httpRequest => httpRequest.Equals(request))))
                .Returns(new HttpResponse { Content = content });

            HttpResponse response = client.Get(url);

            Assert.That(response.Content, Is.EqualTo(content));
        }
    }
}