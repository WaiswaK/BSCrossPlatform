using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    class AssignmentModel
    {
      public int id { get; set; }
       public string title { get; set; }
       public string description { get; set; }
       public string teacher { get; set; }
       public List<AttachmentModel> Files { get; set; }
       public AssignmentModel() { }
       public AssignmentModel(int Assignment_id, string _title, string _description, string full_names, List<AttachmentModel> _files)
       {
        id = Assignment_id;
        title = _title;
        description = _description;
        teacher = full_names;
        Files = _files;
       }
    }
}
