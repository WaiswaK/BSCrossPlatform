using BSCrossPlatform.Core;
using BSCrossPlatform.Database;
using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using System;
using System.Linq;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class AssignmentAttachView : ContentPage
    {
        AssignmentModel Assignment = new AssignmentModel();
        string assignemt_content = string.Empty;
        public AssignmentAttachView(AssignmentModel assignment, string assignment_notes)
        {
            InitializeComponent();
            Assignment = assignment;
            assignemt_content = assignment_notes;
            BindingContext = new AssignmentViewModel(assignment, assignment_notes);
        }
        async void OnViewClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssignmentView(Assignment, assignemt_content));
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
                            await Navigation.PushAsync(new BrowserView(Core.Constant.BaseUri + attachment.FilePath));
                        }
                    }
                    else
                    {
                        await DisplayAlert(Message.Connection_Error_Header, Message.Connection_Error, Message.Ok);
                    }
                }
            }
        }
    }
}
