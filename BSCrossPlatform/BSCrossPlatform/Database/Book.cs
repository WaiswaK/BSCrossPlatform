using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    public class Book
    {
        [PrimaryKey]
        public int Book_id { get; set; }
        public string Book_title { get; set; }
        public string Book_author { get; set; }
        public string Book_description { get; set; }
        public string updated_at { get; set; }
        public string thumb_url { get; set; }
        public string file_url { get; set; }
        public int file_size { get; set; }
        public int Library_id { get; set; }
        public int Category_id { get; set; }
        public string Category_name { get; set; }
        public Book(int _book_id, string _book_title, string _book_author, string _book_description, string _updated_at, 
            string _thumb_url, int _file_size, int _library_id, int _category_id, string _category_name, string _file_url)
        {
            Book_id = _book_id;
            Book_title = _book_title;
            Book_author = _book_author;
            Book_description = _book_description;
            updated_at = _updated_at;
            thumb_url = _thumb_url;
            file_url = _file_url;
            Library_id = _library_id;
            Category_id = _category_id;
            Category_name = _category_name;
            file_size = _file_size;
        }
        public Book()
        {
        }
    }
}
