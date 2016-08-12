using System.Collections.Generic;
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class LibraryCategoryBooksViewModel
    {
        private List<BookModel> _books;
        public List<BookModel> BookList
        {
            get { return _books; }           
            set { _books = value; }
        }
        private string _categoryname;
        public string CategoryName
        {
            get
            {
                return _categoryname;
            }
            set
            {
                _categoryname = value;
            }
        }
        public LibraryCategoryBooksViewModel(LibCategoryModel category)
        {
            BookList = category.category_books;
            CategoryName = category.category_name;
        }
    }
}
