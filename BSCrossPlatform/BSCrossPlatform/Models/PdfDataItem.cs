using System;

namespace BSCrossPlatform.Models
{
    class PdfDataItem
    {
        public PdfDataItem(String uniqueId, String pageNumber, String imagePath)
        {
            UniqueId = uniqueId;
            PageNumber = pageNumber;
            ImagePath = imagePath;
        }
        public string UniqueId { get; private set; }
        public string PageNumber { get; private set; }
        public string ImagePath { get; private set; }
        public override string ToString()
        {
            return PageNumber;
        }
    }    
}
