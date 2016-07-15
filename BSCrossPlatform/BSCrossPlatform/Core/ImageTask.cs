using System.Collections.Generic;
using System.Linq;

namespace BSCrossPlatform.Core
{
    public class ImageTask
    {
        //Getting image format from a string 
        public static string imageFormat(string filepath)
        {
            int f = 0;
            string imageformat = string.Empty;
            filepath = filepath.ToLower(); //Changing to lower-case
            int l = filepath.Length;
            int JPG_extension = Constant.JPG_extension.Length;
            int PNG_extension = Constant.PNG_extension.Length;
            int GIF_extension = Constant.GIF_extension.Length;
            int TIFF_extension = Constant.TIFF_extension.Length;
            int BMP_extension = Constant.BMP_extension.Length;

            //Search for jpg 
            for (int i = 0; i < l; i++)
            {
                if (filepath[i] == Constant.JPG_extension[0])
                {
                    for (int K = i + 1, j = 1; j < JPG_extension; j++, K++)
                    {
                        if (filepath[K] == Constant.JPG_extension[j])
                        {
                            f++;
                        }
                    }
                }
            }
            if (f == JPG_extension - 1)
            {
                imageformat = Constant.JPG_extension;
            }

            if (imageformat == string.Empty)
            {
                //Search for png 
                f = 0;
                for (int i = 0; i < l; i++)
                {
                    if (filepath[i] == Constant.PNG_extension[0])
                    {
                        for (int K = i + 1, j = 1; j < PNG_extension; j++, K++)
                        {
                            if (filepath[K] == Constant.PNG_extension[j])
                            {
                                f++;
                            }
                        }
                    }
                }
                if (f == PNG_extension - 1)
                {
                    imageformat = Constant.PNG_extension;
                }
                if (imageformat == string.Empty)
                {
                    //Search for gif 
                    f = 0;
                    for (int i = 0; i < l; i++)
                    {
                        if (filepath[i] == Constant.GIF_extension[0])
                        {
                            for (int K = i + 1, j = 1; j < GIF_extension; j++, K++)
                            {
                                if (filepath[K] == Constant.GIF_extension[j])
                                {
                                    f++;
                                }
                            }
                        }
                    }
                    if (f == GIF_extension - 1)
                    {
                        imageformat = Constant.GIF_extension;
                    }
                    if (imageformat == string.Empty)
                    {
                        //Search for bmp 
                        f = 0;
                        for (int i = 0; i < l; i++)
                        {
                            if (filepath[i] == Constant.BMP_extension[0])
                            {
                                for (int K = i + 1, j = 1; j < BMP_extension; j++, K++)
                                {
                                    if (filepath[K] == Constant.BMP_extension[j])
                                    {
                                        f++;
                                    }
                                }
                            }
                        }
                        if (f == BMP_extension - 1)
                        {
                            imageformat = Constant.BMP_extension;
                        }
                        if (imageformat == string.Empty)
                        {
                            //Search for tiff
                            f = 0;
                            for (int i = 0; i < l; i++)
                            {
                                if (filepath[i] == Constant.TIFF_extension[0])
                                {
                                    for (int K = i + 1, j = 1; j < TIFF_extension; j++, K++)
                                    {
                                        if (filepath[K] == Constant.TIFF_extension[j])
                                        {
                                            f++;
                                        }
                                    }
                                }
                            }
                            if (f == TIFF_extension - 1)
                            {
                                imageformat = Constant.TIFF_extension;
                            }
                        }
                    }
                }
            }
            return imageformat;
        }
        //Method to get the 10 Digit numbers after a link
        public static string imageNumbers(string fileName)
        {
            string imagename = string.Empty;
            char[] delimiter = { '?' };
            string[] linksplit = fileName.Split(delimiter);
            List<string> linklist = linksplit.ToList();
            imagename = linklist.Last();
            return imagename;
        }
        //Getting image name from a string
        public static string imageName(string filepath)
        {
            string imagename = string.Empty;
            char[] delimiter = { '/' };
            string[] linksplit = filepath.Split(delimiter);
            List<string> linklist = linksplit.ToList();
            imagename = linklist.Last();
            return imagename;
        }
    }
}
