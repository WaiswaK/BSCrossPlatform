using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    class LibCategoryModel
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public int book_count { get; set; }
        public List<BookModel> category_books { get; set; }
        public LibCategoryModel()
        {
        }
        public LibCategoryModel(int id, string name, int count, List<BookModel> books)
        {
            category_id = id;
            category_name = name;
            book_count = count;
            category_books = books;

        }
    }
}
