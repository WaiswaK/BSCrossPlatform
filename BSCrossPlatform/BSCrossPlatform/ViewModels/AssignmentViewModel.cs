using System.Collections.Generic;
using BSCrossPlatform.Models;


namespace BSCrossPlatform.ViewModels
{
    class AssignmentViewModel 
    {
        private string _topicTitle;
        public string AssignmentTitle
        {
            get { return _topicTitle; }
            set { _topicTitle = value; }
        }
        private List<AttachmentModel> _attachments;
        public List<AttachmentModel> AssignmentFiles
        {
            get { return _attachments; }
            set { _attachments = value; }
        }
        private string _notes;
        public string AssignmentNotes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        public AssignmentViewModel(AssignmentModel assignment)
        {
            AssignmentTitle = assignment.title;
            AssignmentFiles = assignment.Files;
            AssignmentNotes = assignment.description;
        }

    }
}
