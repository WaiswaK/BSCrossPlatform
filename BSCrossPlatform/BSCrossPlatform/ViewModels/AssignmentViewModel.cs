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
        private Xamarin.Forms.HtmlWebViewSource _htmlWebViewSource;
        public Xamarin.Forms.HtmlWebViewSource NotesHtmlWebViewSource
        {
            get
            {
                return _htmlWebViewSource;
            }
            set
            {
                _htmlWebViewSource = value;
            }
        }
        public AssignmentViewModel(AssignmentModel assignment, string assignment_notes)
        {
            AssignmentTitle = assignment.title;
            AssignmentFiles = assignment.Files;
            Xamarin.Forms.HtmlWebViewSource temp = new Xamarin.Forms.HtmlWebViewSource();
            temp.Html = assignment_notes;
            NotesHtmlWebViewSource = temp;
        }
    }
}
