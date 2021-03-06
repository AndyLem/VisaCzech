﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace VisaCzech.BL.BGModel
{
    public abstract class ImageConverter
    {
        public static string ConvertImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static Image ConvertBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(RemoveEnters(base64String));
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        protected static string RemoveEnters(string base64String)
        {
            string clearedString = base64String;
            clearedString = clearedString.Replace(((char)(0x0d)).ToString(), "");
            clearedString = clearedString.Replace(((char)(0x0a)).ToString(), "");
            return clearedString;
        }
    }
}
