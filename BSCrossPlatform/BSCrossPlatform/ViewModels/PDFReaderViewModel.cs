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
        public PDFReaderViewModel(AttachmentModel attachement)
        {
            PDFPath = Core.CommonTask.PDFFileFromPath(attachement.FilePath);
        }
        public PDFReaderViewModel(BookModel book)
        {
            PDFPath = Core.CommonTask.PDFFileFromPath(book.file_url);
        }
        public PDFReaderViewModel(PastPaperModel pastpaper, string selection)
        {
            if (selection == Core.Constant.PastPaper)
                PDFPath = pastpaper.pastpaper_file;
            if (selection == Core.Constant.MarkingGuide)
                PDFPath = pastpaper.markingguide_file;
        }
    }
}
