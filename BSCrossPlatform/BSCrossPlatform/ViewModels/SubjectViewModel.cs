using System.Collections.Generic;
using BSCrossPlatform.Models;


namespace BSCrossPlatform.ViewModels
{
    class SubjectViewModel 
    {
        private string _subjectName;
        public string SubjectName
        {
            get { return _subjectName; }
            set { _subjectName = value; }
        }
        private List<CategoryModel> _category;
        public List<CategoryModel> CategoryList
        {
            get { return _category; }
            set { _category = value; }
        }
        private List<CategoryModel> SortedCategories(SubjectModel subject)
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            CategoryModel videos = new CategoryModel("Videos", "videolibrary.jpg", subject.videos.Count, subject.videos);
            CategoryModel files = new CategoryModel("Files", "Files.jpg", subject.files.Count, subject.files);
            CategoryModel assignments = new CategoryModel("Assignments", "assignment.jpg", subject.assignments.Count, subject.assignments);
            CategoryModel topics = new CategoryModel("Topics", "topic.png", subject.topics.Count, subject.topics);
            categories.Add(topics);
            categories.Add(assignments);
            categories.Add(files);
            categories.Add(videos);
            if(subject.topics.Count ==0)
            {
                categories.Remove(topics);
            }
            if (subject.assignments.Count == 0)
            {
                categories.Remove(assignments);
            }
            if(subject.videos.Count == 0)
            {
                categories.Remove(videos);
            }
            if(subject.files.Count == 0)
            {
                categories.Remove(files);
            }
            return categories;
        }
        public SubjectViewModel(SubjectModel subject)
        {            
            SubjectName = subject.name;
            CategoryList = SortedCategories(subject);            
        }
    }
}
