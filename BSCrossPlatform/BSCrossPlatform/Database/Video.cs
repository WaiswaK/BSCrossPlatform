using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("Video")]
    public class Video
    {
        [PrimaryKey]
        public int VideoID { get; set; }
        public int SubjectId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string description { get; set; }
        public string teacher_full_names { get; set; }
        public Video() { }
        public Video(int id, int Sid, string path, string name, string _description, string teacher) 
        {
            VideoID = id;
            SubjectId = Sid;
            FilePath = path;
            FileName = name;
            description = _description;
            teacher_full_names = teacher;
        }

    }
}
