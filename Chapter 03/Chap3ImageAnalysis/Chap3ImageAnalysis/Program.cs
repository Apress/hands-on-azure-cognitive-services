using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;

namespace Chap3ImageAnalysis
{
    /// <summary>
    /// A short demo to test the Computer Vision API
    /// <see cref="https://github.com/Azure-Samples/cognitive-services-quickstart-code/blob/master/dotnet/ComputerVision/ComputerVisionQuickstart.cs"/>
    /// </summary>
    class Program
    {
        //Copy Subscription key and Endpoint from Azure Portal
        //refer to Chapter-3 for detailed steps
        static string subscriptionKey = "SUBSCRIPTION_KEY_GOES_HERE";
        static string endpoint = "ENDPOINT_GOES_HERE";
       
        //image url
        private const string imgURL = "https://docs.microsoft.com/en-us/learn/wwl-data-ai/analyze-images-computer-vision/media/woman-roof.png";

        static void Main(string[] args)
        {
            Console.WriteLine("Chapter-3 Computer Vision demo");
            Console.WriteLine();

            ComputerVisionClient client = AuthenticatedClient(endpoint, subscriptionKey);

            ImageInterpretation.AnalyzeImageFromUrl(client, imgURL).Wait();

            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Image analysis is complete.");
            Console.WriteLine();
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }


        private static ComputerVisionClient AuthenticatedClient(string endpoint, string subscriptionKey)
        {
            ApiKeyServiceClientCredentials clientCredentials = new ApiKeyServiceClientCredentials(subscriptionKey);
            return new ComputerVisionClient(clientCredentials) { Endpoint = endpoint };
        }
        
    }
}
