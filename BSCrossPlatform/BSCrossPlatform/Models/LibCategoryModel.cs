using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    public class LibCategoryModel
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public int book_count { get; set; }
        public string category_image { get; set; }
        public List<BookModel> category_books { get; set; }
        public LibCategoryModel()
        {
        }
    }
}
