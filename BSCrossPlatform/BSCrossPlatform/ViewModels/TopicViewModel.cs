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
        /*private string _notes;
        public string TopicNotes
        {
            get { return _notes; }
            set { _notes = value; }
        }*/

        //Method to get notes that will be displayed in view model
        /*public async System.Threading.Tasks.Task<string> Load_Notes(TopicModel topic)
        {
            string new_notes = await Core.NotesTask.Notes_loader(topic);
            string content = Core.WebViewContentHelper.WrapHtml(new_notes, 100, 100); //Sizes to be worked on
            return content;
        }
        */
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

        /*public string notes(string notes)
        {
            var htmlSource = new Xamarin.Forms.HtmlWebViewSource();
            //string new_notes = await Core.NotesTask.Notes_loader(Current_Topic);
            //var WebView = (WebView)sender;
            //string content = WebViewContentHelper.WrapHtml(all_notes, WebView.ActualWidth, WebView.ActualHeight);
            string content = Core.WebViewContentHelper.WrapHtml(notes, 100, 100);
            htmlSource.Html = content;
            return htmlSource.Html;
            //WebView.NavigateToString(content);
            //return htmlSource;
        }
        */
        /*
        public Xamarin.Forms.HtmlWebViewSource notes(string notes)
        {
            var htmlSource = new Xamarin.Forms.HtmlWebViewSource();
            string content = Core.WebViewContentHelper.WrapHtml(notes, 100, 100);
            htmlSource.Html = content;
            return htmlSource;
        }
        */
        public TopicViewModel(TopicModel Topic, string new_notes)
        {
            TopicName = Topic.TopicTitle;
            TopicFiles = Topic.Files;
            Xamarin.Forms.HtmlWebViewSource temp = new Xamarin.Forms.HtmlWebViewSource();
            temp.Html = new_notes;
            NotesHtmlWebViewSource = temp;
        }
        /*
        public TopicViewModel(TopicModel Topic)
        {
            TopicName = Topic.TopicTitle;
            TopicFiles = Topic.Files;
            //TopicNotes = notes(Topic.notes);//Topic.notes;
            //TopicNotes = await Load_Notes(Topic);
            //TopicNotes = Load_Notes(Topic;

            NotesHtmlWebViewSource = notes(Topic.notes);



        }
        */
    }
}
