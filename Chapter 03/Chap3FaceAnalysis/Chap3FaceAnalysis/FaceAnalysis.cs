using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Chap3FaceAnalysis
{
    public class FaceAnalysis
    {
        /// <summary>
        /// Analayzes the face from URL.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="imgURL">The img URL.</param>
        internal static async Task DetectFaceFromUrl(FaceClient client, string imgURL)
        {
            Console.WriteLine($"Analyzing the image {Path.GetFileName(imgURL)}...");
            Console.WriteLine();
            //request face api and get the analysis result
            IList<DetectedFace> faces = await client.Face.DetectWithUrlAsync(url: imgURL, returnFaceId: true, detectionModel: DetectionModel.Detection02);
            Console.WriteLine("Analysis at a glance:");
            foreach (var face in faces)
            {
                FaceRectangle rect = face.FaceRectangle;
                Console.WriteLine($"Face:{face.FaceId} is located in the image in marked point having dimensions: height-{rect.Height} width-{rect.Width} which is available from Top-{rect.Top} Left-{rect.Left}.");

            }
            ConsoleUtility();
        }



        internal static async Task DetectFaceAndReetrieveFromUrl(FaceClient client, string imgURL)
        {
            var faceAttributes = new FaceAttributeType?[] {
                                                                        FaceAttributeType.Age,
                                                                        FaceAttributeType.Gender,
                                                                        FaceAttributeType.Smile,
                                                                        FaceAttributeType.FacialHair,
                                                                        FaceAttributeType.HeadPose,
                                                                        FaceAttributeType.Glasses,
                                                                        FaceAttributeType.Emotion
                                                                    };

            Console.WriteLine($"Analyzing the image {Path.GetFileName(imgURL)}...");
            Console.WriteLine();
            //request face api and get the analysis result
            // Note DetectionModel.Detection02 cannot be used with returnFaceAttributes.
            var faces = await client.Face.DetectWithUrlAsync(url: imgURL, returnFaceId: true, returnFaceAttributes: faceAttributes, detectionModel: DetectionModel.Detection01);
            Console.WriteLine("Analysis at a glance:");
            foreach (var face in faces)
            {
                var atrr = face.FaceAttributes;
                FaceRectangle rect = face.FaceRectangle;
                var hair = atrr.FacialHair;
                var head = atrr.HeadPose;
                var glasses = atrr.Glasses;
                var emotion = atrr.Emotion;
                Console.WriteLine($"Face:{face.FaceId} is located in the image in marked point having dimensions: height-{rect.Height} width-{rect.Width} which is available from Top-{rect.Top} Left-{rect.Left}.");
                Console.WriteLine();
                Console.WriteLine($"Having attributes:\nAge:{atrr.Age}\nGender:{atrr.Gender}\nSmile:{atrr.Smile}");
                Console.WriteLine();
                Console.WriteLine($"Hair analysis:\nBeard:{hair.Beard}\nMoustache:{hair.Moustache}\nSideburns:{hair.Sideburns}");
                Console.WriteLine();
                Console.WriteLine($"Head analsis:\nPitch:{head.Pitch}\nRoll:{head.Roll}\nYaw:{head.Yaw}");
                Console.WriteLine();
                Console.WriteLine($"Wearing glasses analysis:\nPitch:{glasses.GetValueOrDefault()}");
                Console.WriteLine();
                Console.WriteLine($"Emotions analysis:\nAnger:{emotion.Anger}\nContempt:{emotion.Contempt}\nDisgust:{emotion.Disgust}\nFear:{emotion.Fear}\nHappiness:{emotion.Happiness}\nNeutral:{emotion.Neutral}");
            }
            ConsoleUtility();
        }
        private static void ConsoleUtility()
        {
            Console.WriteLine();
            Console.WriteLine("Image analysis is complete.");
            Console.WriteLine();
            Console.WriteLine("Press enter for more options...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
