using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    public class LibraryModel
    {
        public int library_id { get; set; }
        public List<LibCategoryModel> categories { get; set; }

        public LibraryModel()
        {

        }

        public LibraryModel(int id, List<LibCategoryModel> category)
        {
            library_id = id;
            categories = category;
        }


    }
}
