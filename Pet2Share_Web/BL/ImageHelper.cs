using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.BL
{
    public sealed class ImageHelper
    {
        private static readonly ImageHelper instance = new ImageHelper();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ImageHelper()
        {
        }

        private ImageHelper()
        {
        }

        public static ImageHelper Instance
        {
            get
            {
                return instance;
            }
        }
        public Byte[] CreateImage(Bitmap original, int x, int y, int width, int height)
        {
            var img = new Bitmap(width, height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            return ImageToByte(img);
        }

        public Byte[] ImageToByte(Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (Byte[])converter.ConvertTo(img, typeof(Byte[]));
        }

        public Byte[] StreamToByte(Stream InputStream)
        {
            Byte[] data;
            using (Stream inputStream = InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            return data;
        }
    }
}