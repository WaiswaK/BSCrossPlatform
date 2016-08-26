using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSCrossPlatform.Models
{
    public class PastPaperModel
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public string school { get; set; }
        public string pastpaper_file { get; set; }
        public string markingguide_file { get; set; }
        public PastPaperModel()
        {

        }
    }
}
