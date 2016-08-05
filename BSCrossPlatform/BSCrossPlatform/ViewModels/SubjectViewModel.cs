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
        /*private List<CategoryModel> SortedCategories(SubjectModel subject)
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            CategoryModel videos = new CategoryModel("Videos", "videolibrary.jpg", subject.videos.Count, subject.videos);
            //CategoryModel files = new CategoryModel("Files", "Files.jpg", subject.files.Count, subject.files);
            CategoryModel assignments = new CategoryModel("Assignments", "assignment.jpg", subject.assignments.Count, subject.assignments);
            //CategoryModel topics = new CategoryModel("Topics", "topic.png", subject.topics.Count, subject.topics);
            //categories.Add(topics);
            categories.Add(assignments);
            //categories.Add(files);
            categories.Add(videos);
            /*if(subject.topics.Count ==0)
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
        }*/
        private List<FolderModel> GetFolders(List<TopicModel> topics)
        {
            List<FolderModel> folders = new List<FolderModel>();
            if (topics == null)
            {
                folders = null;
            }
            else
            {
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
            }
            return folders;
        }
        private List<FolderModel> topics;
        public List<FolderModel> TopicList
        {
            get { return topics; }
            set { topics = value; }
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
        public SubjectViewModel(SubjectModel subject)
        {            
            SubjectName = subject.name;
            //CategoryList = SortedCategories(subject);
            FileList = subject.files;
            TopicList = GetFolders(subject.topics);
            VideosList = subject.videos;
            AssignmentList = subject.assignments;
        }
    }
}
