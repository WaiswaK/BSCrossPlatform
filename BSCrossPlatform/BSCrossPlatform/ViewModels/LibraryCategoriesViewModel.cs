using System.Collections.Generic;
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class LibraryCategoriesViewModel
    {
        private List<LibCategoryModel> _categories;
        public List<LibCategoryModel> CategoryList
        {
            get { return _categories; }
            set { _categories = value; }
        }
        public LibraryCategoriesViewModel(LibraryModel library)
        {
            CategoryList = library.categories;
        }
    }
}
