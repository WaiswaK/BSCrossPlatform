using System.Collections.Generic;
using BSCrossPlatform.Models;


namespace BSCrossPlatform.ViewModels
{
    class SubjectPageViewModel 
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
        private List<FolderModel> topics;
        public List<FolderModel> TopicList
        {
            get { return topics; }
            set { topics = value; }
        }
        public List<FolderModel> GetFolders(List<TopicModel> topics) 
        {
            List<FolderModel> folders = new List<FolderModel>();
            foreach (var topic in topics)
            {
                List<TopicModel> folderTopics = new List<TopicModel>();
                FolderModel folder = new FolderModel();
                bool finished = false;
                folder.folder_id = topic.folder_id;
                folder.folder_name = topic.folder_name;
                foreach (var comparisonTopic in topics)
                {
                    finished = false;
                    if (folder.folder_id == comparisonTopic.folder_id)
                    {
                        if (topic.TopicID == comparisonTopic.TopicID)
                        {
                            if (folderTopics.Count > 0)
                            {
                                finished = true;
                            }
                            else
                            {
                                folderTopics.Add(topic);
                            }
                        }
                        else
                        {
                            if (folderTopics.Count > 0)
                            {
                                foreach (var CheckTopic in folderTopics)
                                {
                                    if (comparisonTopic.TopicID == CheckTopic.TopicID)
                                    {
                                        finished = true;
                                    }
                                }
                            }
                            if (finished == false)
                            {
                                folderTopics.Add(comparisonTopic);
                            }
                        }
                    }
                }
                folder.topics = folderTopics;
                finished = false;
                if (folders.Count > 0) 
                {
                    foreach (var folderEntered in folders)
                    {
                        if (folderEntered.folder_id == folder.folder_id)
                        {
                            finished = true;
                        }
                        else
                        {
                            ;
                        }
                    }
                    if (finished == false)
                    {
                        folders.Add(folder);
                        finished = false;
                    }
                }
                else
                {
                    folders.Add(folder);
                }
            }
            return folders;
        }
        public SubjectPageViewModel(SubjectModel subject)
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            CategoryModel videos = new CategoryModel("Videos", "ms-appx:///Assets/icons/video-library.jpg", subject.videos.Count, subject.videos);
            CategoryModel files = new CategoryModel("Files", "ms-appx:///Assets/icons/Files.jpg", subject.files.Count, subject.files);
            CategoryModel assignments = new CategoryModel("Assignments", "ms-appx:///Assets/icons/assignment.jpg", subject.assignments.Count, subject.assignments);
            categories.Add(videos);
            categories.Add(files);
            categories.Add(assignments);
            SubjectName = subject.name;
            CategoryList = categories;
            TopicList = GetFolders(subject.topics);
        }
    }
}
