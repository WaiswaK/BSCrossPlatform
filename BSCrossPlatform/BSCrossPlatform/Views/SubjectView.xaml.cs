using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class SubjectView : TabbedPage
    {
        public SubjectView(SubjectModel subject)
        {
            InitializeComponent();
            if (subject.topics.Count == 0)
                Children.Remove(Topics);
            if (subject.files.Count == 0)
                Children.Remove(Files);
            if (subject.videos.Count == 0)
                Children.Remove(Videos);
            if (subject.assignments.Count == 0)
                Children.Remove(Assignments);
            BindingContext = new SubjectViewModel(subject);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_VideoSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var videofile = ((ListView)sender).SelectedItem as VideoModel;
            if (videofile == null)
                return;
            else
            {
                if (DependencyService.Get<Interfaces.ITask>().IsInternetConnectionAvailable())
                    await Navigation.PushAsync(new PlayView(videofile));
                else
                {
                    await DisplayAlert(Core.Message.File_Access_Header, Core.Message.Offline_Video_Unavailable, Core.Message.Ok);
                }
            }
        }
        async void Handle_TopicSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var notes = ((ListView)sender).SelectedItem as FolderModel;
            if (notes == null)
                return;
            else await Navigation.PushAsync(new TopicsView(notes));
        }
        async void Handle_FileSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var file = ((ListView)sender).SelectedItem as AttachmentModel;
            if (file == null)
                return;
            else
            {
                if (await DependencyService.Get<Interfaces.ITask>().FileExists(file.FilePath))
                    await Navigation.PushAsync(new PDFReader(file));
                else
                {
                    ;
                    if (DependencyService.Get<Interfaces.ITask>().IsInternetConnectionAvailable())
                    {
                        var option = await DisplayAlert(Core.Message.File_Access_Header, Core.Message.File_Access_Message, Core.Message.Yes, Core.Message.No);
                        if (option)
                        {
                            bool fullydownloaded = false;
                            IsBusy = true;
                            try
                            {
                                await DependencyService.Get<Interfaces.ITask>().DownloadFile(file.FilePath, file.FileName);
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
                                    var query = (db.Table<Database.Attachment>().Where(c => c.AttachmentID == file.AttachmentID)).Single();
                                    string newPath = query.FileName + Core.Constant.PDF_extension;
                                    Database.Attachment fileDownloaded = new Database.Attachment(query.AttachmentID, query.TopicID, query.FileName, newPath, query.SubjectId, query.AssignmentID);
                                    db.Update(fileDownloaded);
                                    file.FilePath = newPath;
                                }
                                await Navigation.PushAsync(new PDFReader(file));
                            }
                            else
                            {
                                await DisplayAlert(Core.Message.Download_Header, Core.Message.Download_Error, Core.Message.Ok);
                            }
                        }
                        else
                        {
                            await DisplayAlert(Core.Message.File_Access_Header, Core.Message.Offline_File_Unavailable, Core.Message.Ok);
                        }
                    }
                    else
                    {
                        await DisplayAlert(Core.Message.File_Access_Header, Core.Message.Offline_File_Unavailable, Core.Message.Ok);
                    }
                }
            }
        }
        async void Handle_AssignmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var assignment = ((ListView)sender).SelectedItem as AssignmentModel;
            if (assignment == null)
                return;
            else
            {
                string new_notes = await Core.NotesTask.Notes_loader(assignment);
                string content = Core.WebViewContentHelper.WrapHtml(new_notes, 100, 100);
                await Navigation.PushAsync(new AssignmentView(assignment, content));
            }
        }
    }
}
