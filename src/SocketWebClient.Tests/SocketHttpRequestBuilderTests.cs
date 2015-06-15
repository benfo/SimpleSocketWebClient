using NUnit.Framework;
using System;

namespace SocketWebClient.Tests
{
    public class SocketHttpRequestBuilderTests
    {
        [Test]
        public void Should_build_a_request_with_the_given_method_and_url()
        {
            const string expected = "GET http://www.test.com/page HTTP/1.1\r\n" +
                                    "Host: www.test.com\r\n" +
                                    "Connection: Close\r\n";

            var builder = new SocketHttpRequestBuilder(Method.GET, new Uri("http://www.test.com/page"));

            string result = builder.Build();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}