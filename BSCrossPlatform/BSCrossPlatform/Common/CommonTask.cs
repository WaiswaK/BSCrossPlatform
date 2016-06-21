using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

using Xamarin.Forms;

namespace BSCrossPlatform.Common
{
    class CommonTask
    {
        //Method that checks if the App is online
        public static bool IsInternetConnectionAvailable()
        {
            ConnectionProfile connection = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connection != null && connection.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        //Method to Initialize the SQLite Database
        public static async Task InitializeDatabase()
        {
            await DependencyService.Get<Interfaces.IDatabase>().InitializeDatabase();
        }
        //Method to format the youtube Link
        public static string newYouTubeLink(string link)
        {
            char[] delimiter1 = { '=' };
            char[] delimiter2 = { '/' };
            string[] linksplit = link.Split(delimiter1);
            List<string> linklist = linksplit.ToList();
            string linkfile = linklist.Last();
            string finallink = "https://www.youtube.com/embed/" + linkfile;
            return finallink;
        }
    }
}
