using System.Collections.Generic;
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class TopicsViewModel
    {
        private string _topicTitle;
        public string TopicTitle
        {
            get { return _topicTitle; }
            set { _topicTitle = value; }

        }
        private List<TopicModel> _topics;
        public List<TopicModel> TopicList
        {
            get { return _topics; }
            set { _topics = value; }
        }
        public TopicsViewModel(FolderModel MainTopic)
        {
            TopicTitle = MainTopic.folder_name;
            TopicList = MainTopic.topics;
        }
    }
}
