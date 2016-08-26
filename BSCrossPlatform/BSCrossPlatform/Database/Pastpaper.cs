using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("Pastpaper")]
    public class Pastpaper
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string School { get; set; }
        public string Pastpaper_file { get; set; }
        public string Markingguide_file { get; set; }
    }
}
