using System.Collections.Generic;
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class StudentViewModel{
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
        //Modules
        private List<ModulesModel> _studentContent;
        public List<ModulesModel> StudentContent
        {
            get { return _studentContent; }
            set { _studentContent = value; }
        }
        //Subjects
        private List<SubjectModel> _courses;
        public List<SubjectModel> CourseList
        {
            get { return _courses; }
            set { _courses = value; }
        }
        /*private string _subjecticon;
        public string SubjectIcon
        {
            get { return _subjecticon; }
            set { _subjecticon = value; }
        }*/
        //Library
        private List<LibCategoryModel> _libCategory;
        public List<LibCategoryModel> CategoryList
        {
            get { return _libCategory; }
            set { _libCategory = value; }
        }
        /*private string _libraryicon;
        public string LibraryIcon
        {
            get { return _libraryicon; }
            set { _libraryicon = value; }
        }*/
        
        public StudentViewModel(UserModel user)
        {
            School = user.School;
            SchoolName = School.SchoolName;
            SchoolBadge = School.ImagePath;
            CourseList = user.subjects;
            CategoryList = user.Library.categories;
            StudentContent = new List<ModulesModel>()
            {
                new ModulesModel("Subjects / Units", "units.png"),
                new ModulesModel("Library (eBooks)","library.png")
            };
            //LibraryIcon = "library.png";
            //SubjectIcon = "units.png";
        }
    }
}
