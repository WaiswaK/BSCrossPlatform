using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("Attachment")]
    public class Attachment
    {
        [PrimaryKey]
        public int AttachmentID { get; set; }
        public int TopicID { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int SubjectId { get; set; }
        public int AssignmentID { get; set; }
        public Attachment() { }
        public Attachment(int id, int topicId, string _filename, string _filepath, int sid, int Aid)
        {
            AttachmentID = id;
            TopicID = topicId;
            FilePath = _filepath;
            FileName = _filename;
            SubjectId = sid;
            AssignmentID = Aid;
        }
    }
}
