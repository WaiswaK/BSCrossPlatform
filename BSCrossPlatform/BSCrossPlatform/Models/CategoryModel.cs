using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    class CategoryModel
    {
        public string categoryName { get; set; }
        public string categoryImage { get; set; }
        public int categorycount { get; set; }
        public List<AttachmentModel> files { get; set; }
        public List<VideoModel> videos { get; set; }
        public List<AssignmentModel> assignments { get; set; }
        public CategoryModel() { }
        public CategoryModel(string _categoryname, string _categoryImage, int _categoryCount, List<AttachmentModel> _files) 
        {
            categoryName = _categoryname;
            categoryImage = _categoryImage;
            categorycount = _categoryCount;
            files = _files;
        }
        public CategoryModel(string _categoryname, string _categoryImage, int _categoryCount, List<AssignmentModel> _assignments)
        {
            categoryName = _categoryname;
            categoryImage = _categoryImage;
            categorycount = _categoryCount;
            assignments = _assignments;
        }
        public CategoryModel(string _categoryname, string _categoryImage, int _categoryCount, List<VideoModel> _videos)
        {
            categoryName = _categoryname;
            categoryImage = _categoryImage;
            categorycount = _categoryCount;
            videos = _videos;
        }
    }
}
