using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    public class FolderModel
    {
        public int folder_id { get; set; }
        public string folder_name { get; set;}
        public List<TopicModel> topics { get; set; }
        public FolderModel() { }
    }
}
