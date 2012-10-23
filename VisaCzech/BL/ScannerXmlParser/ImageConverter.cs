using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace VisaCzech.BL.ScannerXmlParser
{
    public abstract class ImageConverter
    {
        public static string ConvertImageToBase64(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                var imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                var base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static Image ConvertBase64ToImage(string base64String)
        {
            var imageBytes = Convert.FromBase64String(RemoveEnters(base64String));
            var ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }

        protected static string RemoveEnters(string base64String)
        {
            var clearedString = base64String;
            clearedString = clearedString.Replace(((char)(0x0d)).ToString(CultureInfo.InvariantCulture), "");
            clearedString = clearedString.Replace(((char)(0x0a)).ToString(CultureInfo.InvariantCulture), "");
            return clearedString;
        }
    }
}
