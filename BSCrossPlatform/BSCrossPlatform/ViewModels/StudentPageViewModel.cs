using System.Collections.Generic;
using BSCrossPlatform.Models;



namespace BSCrossPlatform.ViewModels
{
    class StudentPageViewModel{
        //School
        private SchoolModel _school;
        public SchoolModel School
        {
            get { return _school; }
            set { _school = value; }
        }
        private string _schoolname;
        public string SchoolName
        {
            get { return _schoolname; }
            set { _schoolname = value; }
        }
        private string _schoolbadge;
        public string SchoolBadge
        {
            get { return _schoolbadge; }
            set { _schoolbadge = value; }
        }
        //Subjects
        private List<SubjectModel> _courses;
        public List<SubjectModel> CourseList
        {
            get { return _courses; }
            set { _courses = value; }
        }
        private LibraryModel _library;
        public LibraryModel Library
        {
         get { return _library; }
         set { _library = value; }
        }

        private List<LibCategoryModel> _libCategory;
        public List<LibCategoryModel> CategoryList
        {
            get { return _libCategory; }
            set { _libCategory = value; }
        }


        public StudentPageViewModel(UserModel user)
        {
            School = user.School;
            SchoolName = School.SchoolName;
            SchoolBadge = School.ImagePath;
            CourseList = user.subjects;
            CategoryList = user.Library.categories;

        }
    }
}
