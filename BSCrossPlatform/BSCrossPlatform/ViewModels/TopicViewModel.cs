using System.Collections.Generic;
using BSCrossPlatform.Models;


namespace BSCrossPlatform.ViewModels
{
    class TopicViewModel 
    {
        private string _topicTitle;
        public string TopicName
        {
            get { return _topicTitle; }
            set { _topicTitle = value; }
        }
        private List<AttachmentModel> _attachments;
        public List<AttachmentModel> TopicFiles
        {
            get { return _attachments; }
            set { _attachments = value; }
        }
        private string _notes;
        public string TopicNotes
        {
            get { return _notes; }
            set { _notes = value; }
        }
   
        public TopicViewModel(TopicModel Topic)
        {
            TopicName = Topic.TopicTitle;
            TopicFiles = Topic.Files;
            TopicNotes = Topic.notes;
        }

    }
}
