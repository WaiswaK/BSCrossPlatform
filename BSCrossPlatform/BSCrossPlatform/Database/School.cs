using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("School")]
    public class School
    {
        [PrimaryKey]
        public int School_id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolBadge { get; set; }
        public School() { }
        public School(int _id, string _schoolname, string _schoolbadge) 
        {
            SchoolName = _schoolname;
            SchoolBadge = _schoolbadge;
            School_id = _id;
        }
    }
}
