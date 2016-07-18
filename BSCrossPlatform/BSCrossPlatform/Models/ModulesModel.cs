using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    public class ModulesModel
    {
        public string Module { get; set; }
        public string ModuleImage { get; set; }
        public List<SubjectModel> Subjects { get; set; }
        public List<LibCategoryModel> LibraryCategories { get; set; }
        public ModulesModel(string _module, string _moduleimage, List<SubjectModel> _subjects)
        {
            Module = _module;
            ModuleImage = _moduleimage;
            Subjects = _subjects;
        }
        public ModulesModel(string _module, string _moduleimage, List<LibCategoryModel> _libraryCategories)
        {
            Module = _module;
            ModuleImage = _moduleimage;
            LibraryCategories = _libraryCategories;
        }
    }
}
