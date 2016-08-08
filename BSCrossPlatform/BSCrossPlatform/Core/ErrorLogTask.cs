using BSCrossPlatform.Database;
using System.Linq;
using Xamarin.Forms;

namespace BSCrossPlatform.Core
{
    class ErrorLogTask
    {
        public string Error_title { get; set; }
        public string Error_details { get; set; }
        public string Location { get; set; }
        public static void UploadLogFile()
        {
            try
            {
                using (var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection())
                {
                    var query = (db.Table<log>().ToList());
                    System.Collections.Generic.List<ErrorLogTask> logs = new System.Collections.Generic.List<ErrorLogTask>();
                    foreach (var error in query)
                    {
                        ErrorLogTask log = new ErrorLogTask()
                        {
                            Error_details = error.Error_details,
                            Error_title = error.Error_title,
                            Location = error.Location
                        };
                        logs.Add(log);
                    }
                }
            }
            catch
            {
            }
            //Trigger upload of the errors
            //Delete errors after send
        }
        public static void LogFileSaveAsync(ErrorLogTask Errorlog)
        {
            try
            {
                var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection(); 
                db.Insert(new log() { Error_title = Errorlog.Error_title, Error_details = Errorlog.Error_details, Location = Errorlog.Location });
            }
            catch
            {
            }
        }
    }
}
