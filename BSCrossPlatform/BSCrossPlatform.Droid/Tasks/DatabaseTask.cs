using SQLite.Net;

[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.Droid.Tasks.DatabaseTask))]

namespace BSCrossPlatform.Droid.Tasks
{
    class DatabaseTask : Interfaces.IDatabase
    {              
        public DatabaseTask()
        {
        }
        public async System.Threading.Tasks.Task<bool> LocalDatabaseNotPresent(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
                return true;
            else
                return false;
        }
        public SQLiteConnection GetConnection()
        {
            string dbPath = System.IO.Path.Combine(NativeTask.AppFolderPath(), Core.Constant.dbName);
            var conn = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbPath, true, null, null, null, null); 
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
    }
}