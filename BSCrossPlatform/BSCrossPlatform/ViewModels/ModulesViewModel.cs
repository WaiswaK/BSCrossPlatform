using BSCrossPlatform.Models;
using System.Collections.Generic;
using System.Linq;

namespace BSCrossPlatform.ViewModels
{
    class ModulesViewModel
    {
        //Subjects
        private List<SubjectModel> _courses;
        public List<SubjectModel> CourseList
        {
            get { return _courses; }
            set { _courses = value; }
        }
        //Library
        private List<LibCategoryModel> _libCategory;
        public List<LibCategoryModel> CategoryList
        {
            get { return _libCategory; }
            set { _libCategory = value; }
        }
        private List<LibCategoryModel> LibraryCategories(List<LibCategoryModel> initial_categories)
        {
            List<LibCategoryModel> categories = new List<LibCategoryModel>();
            try
            {
                //List<LibCategoryModel> categories = new List<LibCategoryModel>();
                List<BookModel> books = new List<BookModel>();
                foreach (var category in initial_categories)
                {
                    category.category_image = category.category_books.First().thumb_url;
                    categories.Add(category);
                }

                //return categories;
            }
            catch 
            {

            }
            return categories;
        }
        public ModulesViewModel(ModulesModel models)
        {
            CourseList = models.Subjects;
            CategoryList = LibraryCategories(models.LibraryCategories);//models.LibraryCategories;
        }
    }
}
