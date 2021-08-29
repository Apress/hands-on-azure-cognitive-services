using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Chap3ImageAnalysis
{
    public class ImageInterpretation
    {
        public static async Task AnalyzeImageFromUrl(ComputerVisionClient client, string imgURL)
        {
            List<VisualFeatureTypes?> featuresToBeAnalyzed = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };

            Console.WriteLine($"Analyzing the image {Path.GetFileName(imgURL)}...");
            Console.WriteLine();

            //request vision api and get the analysis result
            ImageAnalysis analysisData = await client.AnalyzeImageAsync(imgURL, featuresToBeAnalyzed);

            // Sunmarizes the image content.
            Console.WriteLine("Analysis at a glance:");
            foreach (var caption in analysisData.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
            }
            Console.WriteLine();

            Console.WriteLine("Image Categories:");
            foreach (var category in analysisData.Categories)
            {
                Console.WriteLine($"{category.Name} with confidence {category.Score}");
            }
            Console.WriteLine();

            Console.WriteLine("Image Tags with confidence score:");
            foreach (var tag in analysisData.Tags)
            {
                Console.WriteLine($"{tag.Name} {tag.Confidence}");
            }
            Console.WriteLine();

            Console.WriteLine("Identified Objects:");
            foreach (var obj in analysisData.Objects)
            {
                Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} at location {obj.Rectangle.X}, " +
                  $"{obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
            }
            Console.WriteLine();

            Console.WriteLine("Faces:");
            foreach (var face in analysisData.Faces)
            {
                Console.WriteLine($"A {face.Gender} of age {face.Age} at location {face.FaceRectangle.Left}, " +
                  $"{face.FaceRectangle.Left}, {face.FaceRectangle.Top + face.FaceRectangle.Width}, " +
                  $"{face.FaceRectangle.Top + face.FaceRectangle.Height}");
            }
            Console.WriteLine();

            Console.WriteLine("Image Adult contents, if any:");
            Console.WriteLine($"Has adult content: {analysisData.Adult.IsAdultContent} with confidence {analysisData.Adult.AdultScore}");
            Console.WriteLine($"Has racy content: {analysisData.Adult.IsRacyContent} with confidence {analysisData.Adult.RacyScore}");
            Console.WriteLine();

            Console.WriteLine("Image Brands:");
            foreach (var brand in analysisData.Brands)
            {
                Console.WriteLine($"Logo of {brand.Name} with confidence {brand.Confidence} at location {brand.Rectangle.X}, " +
                  $"{brand.Rectangle.X + brand.Rectangle.W}, {brand.Rectangle.Y}, {brand.Rectangle.Y + brand.Rectangle.H}");
            }
            Console.WriteLine();

            Console.WriteLine("Celebrities details from the image, if any:");
            foreach (var category in analysisData.Categories)
            {
                if (category.Detail?.Celebrities != null)
                {
                    foreach (var celeb in category.Detail.Celebrities)
                    {
                        Console.WriteLine($"{celeb.Name} with confidence {celeb.Confidence} at location {celeb.FaceRectangle.Left}, " +
                          $"{celeb.FaceRectangle.Top}, {celeb.FaceRectangle.Height}, {celeb.FaceRectangle.Width}");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Landmarks in image, if any:");
            foreach (var category in analysisData.Categories)
            {
                if (category.Detail?.Landmarks != null)
                {
                    foreach (var landmark in category.Detail.Landmarks)
                    {
                        Console.WriteLine($"{landmark.Name} with confidence {landmark.Confidence}");
                    }
                }
            }
            Console.WriteLine();

            Console.WriteLine("Image Color Scheme:");
            Console.WriteLine("Is black and white?: " + analysisData.Color.IsBWImg);
            Console.WriteLine("Accent color: " + analysisData.Color.AccentColor);
            Console.WriteLine("Dominant background color: " + analysisData.Color.DominantColorBackground);
            Console.WriteLine("Dominant foreground color: " + analysisData.Color.DominantColorForeground);
            Console.WriteLine("Dominant colors: " + string.Join(",", analysisData.Color.DominantColors));
            Console.WriteLine();

            Console.WriteLine("Image Type:");
            Console.WriteLine("Clip Art Type: " + analysisData.ImageType.ClipArtType);
            Console.WriteLine("Line Drawing Type: " + analysisData.ImageType.LineDrawingType);
            Console.WriteLine();

        }
    }
}
