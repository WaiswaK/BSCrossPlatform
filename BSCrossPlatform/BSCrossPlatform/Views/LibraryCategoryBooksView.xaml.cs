using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class LibraryCategoryBooksView : ContentPage
    {
        public LibraryCategoryBooksView(LibCategoryModel category)
        {
            InitializeComponent();
            BindingContext = new LibraryCategoryBooksViewModel(category);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var book = ((ListView)sender).SelectedItem as BookModel;
            if (book == null)
                return;
            else 
            {
                if (await DependencyService.Get<Interfaces.ITask>().FileExists(book.file_url))
                    await Navigation.PushAsync(new PDFReader(book));
                else
                {
                    var option = await DisplayAlert("Question?", "Would you like to download the file", "Download", "View");
                    if (DependencyService.Get<Interfaces.ITask>().IsInternetConnectionAvailable())
                    {
                        if (option)
                        {
                            //downloadProgress.IsVisible = true;
                            bool fullydownloaded = false;
                            IsBusy = true;
                            try
                            {
                                await DependencyService.Get<Interfaces.ITask>().DownloadFile(book.file_url, book.book_title);
                                fullydownloaded = true;
                            }
                            catch
                            {
                                fullydownloaded = false;
                                IsBusy = false;
                            }

                            if (fullydownloaded == true)
                            {
                                IsBusy = false;
                                using (var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection())
                                {
                                    var query = (db.Table<Database.Book>().Where(c => c.Book_id == book.book_id)).Single();
                                    string newPath = DependencyService.Get<Interfaces.ITask>().pdfPath(query.Book_title + Core.Constant.PDF_extension);
                                    newPath = newPath.Replace(' ', '_');
                                    Database.Book fileDownloaded = new Database.Book(query.Book_id, query.Book_title, query.Book_author, query.Book_description,
                                        query.updated_at, query.thumb_url, query.file_size, query.Library_id, query.Category_id, query.Category_name, newPath);
                                    db.Update(fileDownloaded);
                                    book.file_url = newPath;
                                }
                                await Navigation.PushAsync(new PDFReader(book));
                            }
                            else
                            {
                                await DisplayAlert(Core.Message.Download_Header, Core.Message.Download_Error, Core.Message.Ok);
                            }
                        }
                        else
                        {
                            await Navigation.PushAsync(new BrowserView(Core.Constant.BaseUri + book.file_url));
                        }
                    }
                    else
                    {
                        await DisplayAlert(Core.Message.Connection_Error_Header, Core.Message.Connection_Error, Core.Message.Ok);
                    }
                }
            }
        }
    }
}
