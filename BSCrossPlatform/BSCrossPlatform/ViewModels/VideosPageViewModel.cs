using System.Collections.Generic;
using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class VideosPageViewModel
    {
       private List<VideoModel> _videosList;
       public List<VideoModel> VideosList
        {
            get { return _videosList; }
            set { _videosList= value; }
        }
       public VideosPageViewModel(CategoryModel videos)
        {
            VideosList = videos.videos;
        }
    }
}
