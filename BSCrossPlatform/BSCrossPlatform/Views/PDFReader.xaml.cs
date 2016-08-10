
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
    }
}
