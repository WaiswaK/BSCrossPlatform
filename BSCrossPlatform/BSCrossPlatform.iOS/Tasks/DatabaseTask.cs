using SQLite.Net;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.iOS.Database.DBConnection))]

namespace BSCrossPlatform.iOS.Database
{
    class DBConnection : Interfaces.IDatabase
    {
        static string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
        static string libraryPath = System.IO.Path.Combine(documentsPath, "..", "Library"); // Library folder
        string dbPath = System.IO.Path.Combine(libraryPath, Common.Constant.dbName);

        public DBConnection()
        {
        }
        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(null, dbPath, true, null, null, null, null); 
            return conn;
        }
        public async System.Threading.Tasks.Task InitializeDatabase()
        {
            if (await LocalDatabaseNotPresent(Common.Constant.dbName))
            {
                using (var db = GetConnection())
                {
                    db.CreateTable<BSCrossPlatform.Database.Subject>();
                    db.CreateTable<BSCrossPlatform.Database.Topic>();
                    db.CreateTable<BSCrossPlatform.Database.Assignment>();
                    db.CreateTable<BSCrossPlatform.Database.Attachment>();
                    db.CreateTable<BSCrossPlatform.Database.Video>();
                    db.CreateTable<BSCrossPlatform.Database.User>();
                    db.CreateTable<BSCrossPlatform.Database.School>();
                    db.CreateTable<BSCrossPlatform.Database.Book>();
                    db.CreateTable<BSCrossPlatform.Database.log>();
                };
            }
            else
            {
            }
        }
        public async System.Threading.Tasks.Task<bool> LocalDatabaseNotPresent(string fileName)
        {
            if (!System.IO.File.Exists(dbPath))
                return true;
            else
                return false;
        }      
    }
}
