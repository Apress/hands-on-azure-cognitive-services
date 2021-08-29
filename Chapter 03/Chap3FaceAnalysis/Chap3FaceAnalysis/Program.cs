using Microsoft.Azure.CognitiveServices.Vision.Face;
using System;

namespace Chap3FaceAnalysis
{
    class Program
    {
        private static bool _continue;
        //Copy Subscription key and Endpoint from Azure Portal
        //refer to Chapter-3 for detailed steps
        //static string subscriptionKey = "SUBSCRIPTION_KEY_GOES_HERE";
        //static string endpoint = "ENDPOINT_GOES_HERE";
        //image url
        //private const string imgURL = "https://azurecomcdn.azureedge.net/cvt-caf9b3609b1d754524c718b4cde399fda4ea781184fcff2c2e29fbbded7c0ae5/images/shared/cognitive-services-demos/face-detection/detection-1-thumbnail.jpg";
        private const string imgURL = "https://azurecomcdn.azureedge.net/cvt-caf9b3609b1d754524c718b4cde399fda4ea781184fcff2c2e29fbbded7c0ae5/images/shared/cognitive-services-demos/face-detection/detection-6-thumbnail.jpg";

        static void Main(string[] args)
        {
            try
            {
                DoShowTheOptions();
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

        }

        private static void DoShowTheOptions()
        {
            var stringComparer = StringComparer.OrdinalIgnoreCase;
            _continue = true;
            while (_continue)
            {
                Console.WriteLine("Chapter-3 Computer facial analysis demo");
                Console.WriteLine("1 - Detect");
                Console.WriteLine("2 - Detect with details");
                Console.WriteLine("Q - Exit");
                Console.Write("Enter input:");
                var option = Console.ReadLine();
                Console.Clear();
                if (stringComparer.Equals("q", option))
                {
                    _continue = false;
                }
                else
                {
                    FaceClient client = AuthenticatedClient(endpoint, subscriptionKey);
                    if (option == "1")
                    {

                        FaceAnalysis.DetectFaceFromUrl(client, imgURL).Wait();
                    }
                    else if (option == "2")
                    {
                        FaceAnalysis.DetectFaceAndReetrieveFromUrl(client, imgURL).Wait();
                    }
                }

            }

        }

        private static FaceClient AuthenticatedClient(string endpoint, string subscriptionKey)
        {
            ApiKeyServiceClientCredentials clientCredentials = new ApiKeyServiceClientCredentials(subscriptionKey);
            return new FaceClient(clientCredentials) { Endpoint = endpoint };
        }
    }
}
