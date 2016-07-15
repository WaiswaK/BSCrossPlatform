﻿using SQLite.Net;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.WinPhone.Tasks.DatabaseTask))]

namespace BSCrossPlatform.WinPhone.Tasks
{
    class DatabaseTask : Interfaces.IDatabase
    {
        private static Windows.Storage.StorageFolder appFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        private static string dbPath = System.IO.Path.Combine(appFolder.Path, Core.Constant.dbName);

        public DatabaseTask()
        {
            //conn = new SQLiteConnection(dbPath);
        }

        public SQLiteConnection GetConnection()
        {
            SQLiteConnection conn = null;
            try
            {
                conn = new SQLiteConnection(null, dbPath, true, null, null, null, null); //(path);
            }
            catch
            {

            }
            // Return the database connection 
            //var check = new SQLiteConnection()
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
                await appFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
