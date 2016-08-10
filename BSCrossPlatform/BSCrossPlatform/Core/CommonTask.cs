using System.Collections.Generic;
using System.Linq;
//using System.Net;
using System.Threading.Tasks;
//using Windows.Networking.Connectivity;

using Xamarin.Forms;

namespace BSCrossPlatform.Core
{
    public class CommonTask
    {
        //Method to Initialize the SQLite Database
        public static async Task InitializeDatabase()
        {
            await DependencyService.Get<Interfaces.IDatabase>().InitializeDatabase();
        }
        //Method to format the youtube Link
        public static string newYouTubeLink(string link)
        {
            char[] delimiter = { '=' };
            string[] linksplit = link.Split(delimiter);
            List<string> linklist = linksplit.ToList();
            string linkfile = linklist.Last();
            string finallink = "https://www.youtube.com/embed/" + linkfile;
            return finallink;
        }
        public static string httplink(string filepath)
        {
            int f = 0;
            string weblink = string.Empty;
            int l = filepath.Length;
            string link = "http";
            int http = link.Length;

            //Search for http in link 
            for (int i = 0; i < l; i++)
            {
                if (filepath[i] == link[0])
                {
                    for (int K = i + 1, j = 1; j < http; j++, K++)
                    {
                        if (filepath[K] == link[j])
                        {
                            f++;
                        }
                    }
                }
            }
            if (f == http - 1)
            {
                weblink = filepath;
            }
            else
            {
                weblink = Constant.BaseUri + filepath;
            }
            return weblink;
        }
        //Method to Get PDF File name from path
        public static string PDFFileFromPath(string link)
        {
            char[] delimiter = { '/' };
            string[] linksplit = link.Split(delimiter);
            List<string> linklist = linksplit.ToList();
            return linksplit.Last();
        }
    }
}
