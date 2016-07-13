namespace BSCrossPlatform.Models
{
    class AttachmentModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int AttachmentID { get; set; }
        public AttachmentModel(int _attachmentID, string _filePath, string _fileName)
        {
            FilePath = _filePath;
            AttachmentID = _attachmentID;
            FileName = _fileName;
        }
        public AttachmentModel() { }
    }
}
