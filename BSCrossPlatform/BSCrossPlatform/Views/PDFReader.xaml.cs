
using BSCrossPlatform.Models;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class PDFReader : ContentPage
    {
        public PDFReader(AttachmentModel file)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PDFReaderViewModel(file);
        }
        public PDFReader(BookModel book)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PDFReaderViewModel(book);
        }
        public PDFReader(PastPaperModel pastpaper, string selection)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PDFReaderViewModel(pastpaper, selection);
        }
    }
}
