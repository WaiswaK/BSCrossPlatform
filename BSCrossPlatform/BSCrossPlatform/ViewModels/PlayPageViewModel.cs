
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class PlayPageViewModel
    {
        private string _videotitle;
        public string VideoTitle
        {
            get { return _videotitle; }
            set { _videotitle = value; }
        }
        private string _videosource;
        public string VideoSource
        {
            get { return _videosource; }
            set { _videosource = value; }
        }
        public PlayPageViewModel(VideoModel file)
        {
            VideoTitle = file.FileName;           
            VideoSource = file.FilePath;         
        }
    }
}
