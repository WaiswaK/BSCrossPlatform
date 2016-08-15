using BSCrossPlatform.Core;
using BSCrossPlatform.Database;
using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class TopicView : TabbedPage
    {
        public TopicView(TopicModel topic, string notes)
        {
            InitializeComponent();
            if (topic.Files.Count == 0)
                Children.Remove(Files);
            BindingContext = new TopicViewModel(topic, notes);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var attachment = ((ListView)sender).SelectedItem as AttachmentModel;
            if (attachment == null)
                return;
            else
            {
                if (await DependencyService.Get<Interfaces.ITask>().FileExists(attachment.FilePath))
                    await Navigation.PushAsync(new PDFReader(attachment));
                else
                {
                    if (DependencyService.Get<Interfaces.ITask>().IsInternetConnectionAvailable())
                    {
                        var option = await DisplayAlert(Message.File_Access_Header, Message.File_Access_Message, Message.Yes, Message.No);
                        if (option)
                        {
                            bool fullydownloaded = false;
                            IsBusy = true;
                            try
                            {
                                await DependencyService.Get<Interfaces.ITask>().DownloadFile(attachment.FilePath, attachment.FileName);
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
                                    var query = (db.Table<Attachment>().Where(c => c.AttachmentID == attachment.AttachmentID)).Single();
                                    string newPath = DependencyService.Get<Interfaces.ITask>().pdfPath(query.FileName + Constant.PDF_extension);
                                    newPath = newPath.Replace(' ', '_');
                                    Attachment fileDownloaded = new Attachment(query.AttachmentID, query.TopicID, query.FileName, newPath, query.SubjectId, query.AssignmentID);
                                    db.Update(fileDownloaded);
                                    attachment.FilePath = newPath;
                                }
                                await Navigation.PushAsync(new PDFReader(attachment));
                            }
                            else
                            {
                                await DisplayAlert(Message.Download_Header, Message.Download_Error, Message.Ok);
                            }
                        }
                        else
                        {
                            await DisplayAlert(Message.File_Access_Header, Message.Offline_File_Unavailable, Message.Ok);
                        }
                    }
                    else
                    {
                        await DisplayAlert(Message.File_Access_Header, Message.Offline_File_Unavailable, Message.Ok);
                    }
                }
            }
        }
    }
}
