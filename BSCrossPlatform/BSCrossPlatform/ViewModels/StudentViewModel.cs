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
       
        public StudentViewModel(UserModel user)
        {
            School = user.School;
            SchoolName = School.SchoolName;
            SchoolBadge = School.ImagePath;
            StudentContent = new List<ModulesModel>()
            {
                new ModulesModel("Subjects / Units", "units.png",user.subjects),
                new ModulesModel("Library (eBooks)","library.png",user.Library.categories)
            };
        }
    }
}
