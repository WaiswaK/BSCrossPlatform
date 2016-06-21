using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("Topic")]
    public class log
    {
        public string Error_title { get; set; }
        public string Error_details { get; set; }
        public string Location { get; set; }
        public log()
        {
        }
    }
}
