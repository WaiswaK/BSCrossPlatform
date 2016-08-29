namespace BSCrossPlatform.Core
{
    public class Constant
    {
        public static string dbName = "DB.sqlite";
        private static string http = "http://";
        private static string www = "www.";
        private static string Base = "brainshare.ug";
        public static string BaseUri = http + Base;
        public static string PDF_extension = ".pdf";
        public static string JPG_extension = ".jpg";
        public static string PNG_extension = ".png";
        public static string GIF_extension = ".gif";
        public static string TIFF_extension = ".tiff";
        public static string BMP_extension = ".bmp";
        public static int updating = 1;
        public static int finished_update = 0;
        public static string LoginJsonLink = BaseUri + "/liveapis/authenticate.json";
        public static string NotesJsonLink = BaseUri + "/liveapis/uni_notes.json";
        public static string VideosJsonLink = BaseUri + "/liveapis/uni_videos.json";
        public static string AssignmentJsonLink = BaseUri + "/liveapis/assignments.json";
        public static string FilesJsonLink = BaseUri + "/liveapis/uni_files.json";
        public static string CourseJsonLink = BaseUri + "/liveapis/course_units.json";
        public static string BooksJsonLink = BaseUri + "/liveapis/books.json";
        public static string FullBaseUri = http + www + Base + "/";
        public static string PrivacyPolicyUri = http + "learn." + Base + "/privacy_policy";
        public static string RegisterUri = BaseUri + "/pages/register";
        public static string PasswordUri = BaseUri + "/users/password/new";
        public static string PastPaper = "Past Paper";
        public static string MarkingGuide = "Marking Guide";
    }
}
