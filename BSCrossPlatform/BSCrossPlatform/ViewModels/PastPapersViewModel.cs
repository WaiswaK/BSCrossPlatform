using BSCrossPlatform.Models;
using System.Collections.Generic;

namespace BSCrossPlatform.ViewModels
{
    class PastPapersViewModel
    {
        private List<PastPaperModel> _pastpapers;
        public List<PastPaperModel> PastPapers
        {
            get { return _pastpapers; }
            set { _pastpapers = value;}
        }
        public PastPapersViewModel(List<PastPaperModel> Pastpapers)
        {
            PastPapers = Pastpapers;
        }
        public PastPapersViewModel(OnlineModel userAccount)
        {
            PastPapers = userAccount.PastPapers;
        }
    }
}
