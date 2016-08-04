using System.Collections.Generic;
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class StudentViewModel{
        #region School
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
        #endregion
        #region Modules
        private List<ModulesModel> _studentContent;
        public List<ModulesModel> StudentContent
        {
            get { return _studentContent; }
            set { _studentContent = value; }
        }
        public List<ModulesModel> Final_List(List<SubjectModel> subjects, List<LibCategoryModel> categories)
        {
            ModulesModel subject_module = new ModulesModel("Subjects / Units", "units.png", subjects);
            ModulesModel library_module = new ModulesModel("Library (eBooks)", "library.png", categories);
            List<ModulesModel> final = new List<ModulesModel>()
            {
                subject_module,
                library_module
            };
            if(subjects.Count == 0 )
            {
                final.Remove(subject_module);
            }
            if(categories.Count == 0)
            {
                final.Remove(library_module);
            }
            return final;
        }
        #endregion
        public StudentViewModel(UserModel user)
        {
            School = user.School;
            SchoolName = School.SchoolName;
            SchoolBadge = School.ImagePath;
            StudentContent = Final_List(user.subjects, user.Library.categories);
        }
    }
}
