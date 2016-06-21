using System.Collections.Generic;
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class CategoryPageViewModel
    {
        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
        private List<AttachmentModel> _bookList;
        public List<AttachmentModel> FileList
        {
            get { return _bookList; }
            set { _bookList = value; }
        }
        private List<VideoModel> _videosList;
        public List<VideoModel> VideosList
        {
            get { return _videosList; }
            set { _videosList = value; }
        }
        private List<AssignmentModel> _assignment;
        public List<AssignmentModel> AssignmentList
        {
            get { return _assignment; }
            set { _assignment = value; }
        }
        public CategoryPageViewModel(CategoryModel category)
        {
            FileList = category.files;
            VideosList = category.videos;
            AssignmentList = category.assignments;
            CategoryName = category.categoryName;
        }
    }
}
