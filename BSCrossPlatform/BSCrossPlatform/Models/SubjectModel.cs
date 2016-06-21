using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    class SubjectModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string thumb { get; set; }
        public List<TopicModel> topics { get; set; }
        public List<AssignmentModel> assignments { get; set; }
        public List<VideoModel> videos { get; set; }
        public List<AttachmentModel> files { get; set; }
        public SubjectModel(int _id, string _name, string _thumb, List<TopicModel> _topics, List<AssignmentModel>_assignments, List<VideoModel> _videos, List<AttachmentModel> _files)
        {
            Id = _id;
            name = _name;
            thumb = _thumb;
            topics = _topics;
            files = _files;
            assignments = _assignments;
            videos = _videos;
        }

        public SubjectModel() 
        { 

        }
    }
}
