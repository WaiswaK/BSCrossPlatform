using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("Assignment")]
    public class Assignment
    {
       // [PrimaryKey]
        public int AssignmentID { get; set; }
        public int SubjectId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string teacher_full_names { get; set; }
        public Assignment() { }
        public Assignment(int id, int sid, string _title, string body, string teacher)
        {
            AssignmentID = id;
            SubjectId = sid;
            title = _title;
            description = body;
            teacher_full_names = teacher;
        }
    }
}
