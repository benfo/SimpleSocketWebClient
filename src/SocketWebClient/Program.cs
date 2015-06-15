using System;

namespace SocketWebClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (ValidateArguments(args))
            {
                PrintUsage();
                return;
            }

            MakeGetWebRequest(args);
        }

        private static void MakeGetWebRequest(string[] args)
        {
            HttpClient client = new HttpClient();

            try
            {
                var response = client.Get(args[0]);
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while making a request to " + args[0]);
                Console.WriteLine(ex.Message);
            }
        }

        private static bool ValidateArguments(string[] args)
        {
            return args == null || args.Length != 1;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: websocketclient <url>");
        }
    }
}