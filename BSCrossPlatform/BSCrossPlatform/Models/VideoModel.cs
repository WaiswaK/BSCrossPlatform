namespace BSCrossPlatform.Models
{
    public class VideoModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int VideoID { get; set; }
        public string description { get; set; }
        public string teacher { get; set; }
        public VideoModel(int _videoID, string _filePath, string _fileName, string _description, string _teacher)
        {
            FilePath = _filePath;
            VideoID = _videoID;
            FileName = _fileName;
            description = _description;
            teacher = _teacher;
        }
        public VideoModel() { }
    }
}
