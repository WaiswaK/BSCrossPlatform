using BSCrossPlatform.Core;
using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class SamplePapersView : ContentPage
    {
        public SamplePapersView(List<PastPaperModel> pastpapers)
        {
            InitializeComponent();
            BindingContext = new PastPapersViewModel(pastpapers);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
       => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var paper = ((ListView)sender).SelectedItem as PastPaperModel;
            if (paper == null)
                return;
            else
            {
                var option = await DisplayAlert(Message.OnlineAccess_Header, Message.OnlineAccess, Message.Login_Header, Message.View);
                if (option)
                {
                    await Navigation.PushAsync(new LoginView(false));
                }
                else
                {
                    var Paper_option = await DisplayAlert(Message.Pastpaper_Header, Message.Pastpaper, Message.Pastpaper_Header, Message.Marking_Guide);
                    if (Paper_option)
                        FileSelection(paper, Constant.PastPaper);
                    else
                        FileSelection(paper, Constant.MarkingGuide);
                }
            }
        }
        private async void FileSelection(PastPaperModel pastpaper, string selected)
        {
            if (selected == Constant.PastPaper)
            {
                if (await DependencyService.Get<Interfaces.ITask>().FileExists(pastpaper.pastpaper_file))
                    await Navigation.PushAsync(new PDFReader(pastpaper, selected));
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
                                //await DependencyService.Get<Interfaces.ITask>().DownloadFile(attachment.FilePath, attachment.FileName);
                                //fullydownloaded = true;
                            }
                            catch
                            {
                                //fullydownloaded = false;
                                //IsBusy = false;
                            }

                            if (fullydownloaded == true)
                            {
                                IsBusy = false;
                                using (var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection())
                                {
                                    //var query = (db.Table<Attachment>().Where(c => c.AttachmentID == attachment.AttachmentID)).Single();
                                    //string newPath = DependencyService.Get<Interfaces.ITask>().pdfPath(query.FileName + Constant.PDF_extension);
                                    //newPath = newPath.Replace(' ', '_');
                                    //Attachment fileDownloaded = new Attachment(query.AttachmentID, query.TopicID, query.FileName, newPath, query.SubjectId, query.AssignmentID);
                                    //db.Update(fileDownloaded);
                                    //attachment.FilePath = newPath;
                                }
                               // await Navigation.PushAsync(new PDFReader(attachment));
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
            if (selected == Constant.MarkingGuide)
            {
                if (await DependencyService.Get<Interfaces.ITask>().FileExists(pastpaper.markingguide_file))
                    await Navigation.PushAsync(new PDFReader(pastpaper, selected));
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
                                //await DependencyService.Get<Interfaces.ITask>().DownloadFile(attachment.FilePath, attachment.FileName);
                                //fullydownloaded = true;
                            }
                            catch
                            {
                                //fullydownloaded = false;
                                //IsBusy = false;
                            }

                            if (fullydownloaded == true)
                            {
                                IsBusy = false;
                                using (var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection())
                                {
                                    //var query = (db.Table<Attachment>().Where(c => c.AttachmentID == attachment.AttachmentID)).Single();
                                    //string newPath = DependencyService.Get<Interfaces.ITask>().pdfPath(query.FileName + Constant.PDF_extension);
                                    //newPath = newPath.Replace(' ', '_');
                                    //Attachment fileDownloaded = new Attachment(query.AttachmentID, query.TopicID, query.FileName, newPath, query.SubjectId, query.AssignmentID);
                                    //db.Update(fileDownloaded);
                                    //attachment.FilePath = newPath;
                                }
                                // await Navigation.PushAsync(new PDFReader(attachment));
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
