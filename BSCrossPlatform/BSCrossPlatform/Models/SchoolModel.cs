namespace BSCrossPlatform.Models
{
    public class SchoolModel
    {
        public string SchoolName;
        public string ImagePath;
        public int SchoolId;
        public SchoolModel(string _schoolName, string _imagePath, int _id)
        {
            SchoolName = _schoolName;
            ImagePath = _imagePath;
            SchoolId = _id;
        }
        public SchoolModel() { }
    }
}
