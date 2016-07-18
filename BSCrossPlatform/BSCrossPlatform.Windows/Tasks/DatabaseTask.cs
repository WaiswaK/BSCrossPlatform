using SQLite.Net;
using System;
using Windows.Storage;


[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.Windows.Tasks.DatabaseTask))]

namespace BSCrossPlatform.Windows.Tasks
{
    class DatabaseTask : Interfaces.IDatabase
    {
        private static StorageFolder appFolder = ApplicationData.Current.LocalFolder;
        private static string dbPath = System.IO.Path.Combine(appFolder.Path, Core.Constant.dbName);

        public DatabaseTask()
        {
            //conn = new SQLiteConnection(dbPath);
        }

        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT()
                , dbPath, true, null, null, null, null); 
            return conn;
        }

        public async System.Threading.Tasks.Task InitializeDatabase()
        {
            if (await LocalDatabaseNotPresent(Core.Constant.dbName))
            {
                using (var db = GetConnection())
                {
                    db.CreateTable<Database.Subject>();
                    db.CreateTable<Database.Topic>();
                    db.CreateTable<Database.Assignment>();
                    db.CreateTable<Database.Attachment>();
                    db.CreateTable<Database.Video>();
                    db.CreateTable<Database.User>();
                    db.CreateTable<Database.School>();
                    db.CreateTable<Database.Book>();
                    db.CreateTable<Database.log>();
                };
            }
            else
            {
            }
        }
        public async System.Threading.Tasks.Task<bool> LocalDatabaseNotPresent(string fileName)
        {
            try
            {
                await appFolder.TryGetItemAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
