
using BSCrossPlatform.Models;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class PDFReader : ContentPage
    {
        public PDFReader(AttachmentModel file)
        {
            InitializeComponent();
        }
        public PDFReader(BookModel book)
        {
            InitializeComponent();
        }
    }
}
