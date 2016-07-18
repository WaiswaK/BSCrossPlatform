using BSCrossPlatform.Models;
using System.Collections.Generic;

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
        public ModulesViewModel(ModulesModel models)
        {
            CourseList = models.Subjects;
            CategoryList = models.LibraryCategories;
        }
    }
}
