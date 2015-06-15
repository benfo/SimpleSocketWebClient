using NUnit.Framework;
using System;

namespace SocketWebClient.Tests
{
    public class SocketHttpRequestBuilderTests
    {
        [Test]
        [TestCase("http://www.test.com:8080/page", "www.test.com:8080")]
        [TestCase("http://www.test.com/page", "www.test.com")]
        public void Should_build_a_request_with_the_given_method_and_url(string url, string host)
        {
            string expected = "GET " + url + " HTTP/1.1\r\n" +
                                    "Host: " + host + "\r\n" +
                                    "Connection: Close\r\n";

            var builder = new SocketHttpRequestBuilder(Method.GET, new Uri(url));

            string result = builder.Build();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}