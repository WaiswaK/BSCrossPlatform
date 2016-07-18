using BSCrossPlatform.Models;

namespace BSCrossPlatform.ViewModels
{
    class PDFReaderViewModel
    {
        private string _pdfpath;
        public string PDFPath
        {
            get { return _pdfpath; }
            set { _pdfpath = value; }

        }
        PDFReaderViewModel(AttachmentModel attachement)
        {
            PDFPath = attachement.FilePath;
        }
        PDFReaderViewModel(BookModel book)
        {
            PDFPath = book.file_url;
        }
    }
}
