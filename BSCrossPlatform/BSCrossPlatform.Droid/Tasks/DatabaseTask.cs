using SQLite.Net;

[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.Droid.Tasks.DatabaseTask))]

namespace BSCrossPlatform.Droid.Tasks
{
    class DatabaseTask : Interfaces.IDatabase
    {
        private static string StorageFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
        private string dbPath = System.IO.Path.Combine(StorageFolder, Common.Constant.dbName);

        public DatabaseTask()
        {
        }
        public async System.Threading.Tasks.Task<bool> LocalDatabaseNotPresent(string fileName)
        {
            if (!System.IO.File.Exists(dbPath))
                return true;
            else
                return false;
        }
        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(null, dbPath, true, null, null, null, null); //(path);
            // Return the database connection 
            return conn;
        }
        public async System.Threading.Tasks.Task InitializeDatabase()
        {
            if (await LocalDatabaseNotPresent(Common.Constant.dbName))
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