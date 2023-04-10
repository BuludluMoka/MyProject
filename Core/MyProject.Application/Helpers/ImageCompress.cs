using SixLabors.ImageSharp;

namespace MyProject.Application.Helpers
{
    public class ImageCompress
    {
        public string ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Width> maxHeight || img.Height > maxHeight)
            {
                double widthRatio = (double)img.Width / (double)maxWidth;
                double heightRatio = (double)img.Height / (double)maxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + maxWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }

        }
      
    }
}
