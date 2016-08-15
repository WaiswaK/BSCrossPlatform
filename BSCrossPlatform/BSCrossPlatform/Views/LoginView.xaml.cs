using BSCrossPlatform.Core;
using BSCrossPlatform.Database;
using BSCrossPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }
        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            IsBusy = true;
            #region Deactivate Controls
            email_tb.IsEnabled = false;
            password_tb.IsEnabled = false;
            login_btn.IsEnabled = false;
            create_btn.IsEnabled = false;
            forget_btn.IsEnabled = false;
            #endregion
            LoadingMsg.Text = Message.User_Validation;
            LoadingMsg.IsVisible = true;
            Login();
        }
        void ReActivate_Controls()
        {
            email_tb.IsEnabled = true;
            password_tb.IsEnabled = true;
            login_btn.IsEnabled = true;
            create_btn.IsEnabled = true;
            forget_btn.IsEnabled = true;
        }        
        async void OnCreateButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BrowserView(Core.Constant.RegisterUri));
        }
        void OnForgetClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BrowserView(Core.Constant.PasswordUri));
        }
        private async void Login()
        {
            await CommonTask.InitializeDatabase();
            try
            {
                if (email_tb.Text.Equals(null) || password_tb.Text.Equals(null))
                {
                    
                }
                else
                {
                    if (DependencyService.Get<Interfaces.ITask>().IsInternetConnectionAvailable())
                    {
                        OnlineExperience();
                    }
                    else
                    {
                        OfflineExperience();
                    }
                }                    
            }
            catch
            {
                await DisplayAlert(Message.Login_Header, Message.NoLoginDetails, Message.Ok);
                LoadingMsg.IsVisible = false;
                IsBusy = false;
                ReActivate_Controls();
            }
        }
        private async void OfflineExperience()
        {
            List<User> users = DBRetrievalTask.SelectAllUsers();
            if (users == null)
            {              
                await DisplayAlert(Message.Login_Header, Message.Login_Message_Fail, Message.Ok);
                IsBusy = false;
                LoadingMsg.IsVisible = false;
                ReActivate_Controls();
            }
            else
            {
                bool found = false;
                bool success = false;
                List<SubjectModel> UserSubjects = new List<SubjectModel>();
                UserModel loggedIn = new UserModel();
                char[] delimiter = { '.' };

                foreach (var user in users)
                {
                    if (user.e_mail.Equals(email_tb.Text) && user.password.Equals(password_tb.Text))
                    {
                        loggedIn.email = user.e_mail;
                        loggedIn.password = user.password;
                        loggedIn.School = DBRetrievalTask.GetSchool(user.School_id);
                        loggedIn.full_names = user.profileName;
                        loggedIn.Library = DBRetrievalTask.GetLibrary(loggedIn.School.SchoolId);
                        string[] SplitSubjectId = user.subjects.Split(delimiter);
                        List<string> SubjectIdList = SplitSubjectId.ToList();
                        List<int> subjectids = ModelTask.SubjectIdsConvert(SubjectIdList);
                        foreach (var id in subjectids)
                        {
                            SubjectModel subject = DBRetrievalTask.GetSubject(id);
                            UserSubjects.Add(subject);
                        }
                        loggedIn.subjects = UserSubjects;
                        success = true;
                        found = true;
                        AuthenticateUser(loggedIn);
                    }
                    else if (user.e_mail.Equals(email_tb.Text) && !user.password.Equals(password_tb.Text))
                    {
                        found = true;
                    }
                }

                if (found == true && success == false)
                {
                    await DisplayAlert(Message.Login_Header, Message.Wrong_User_details, Message.Ok);
                    IsBusy = false;
                    LoadingMsg.IsVisible = false;
                    ReActivate_Controls();
                }
                if (found == false)
                {
                    await DisplayAlert(Message.Login_Header, Message.Login_Message_Fail, Message.Ok);
                    IsBusy = false;
                    LoadingMsg.IsVisible = false;
                    ReActivate_Controls();
                }
            }
        }
        private async void OnlineExperience()
        {
            List<User> users = DBRetrievalTask.SelectAllUsers();
            bool found = false;
            List<SubjectModel> UserSubjects = new List<SubjectModel>();
            UserModel loggedIn = new UserModel();
            char[] delimiter = { '.' };

            if (users == null)
            {
                OnlineLogin();
            }
            else
            {
                foreach (var user in users)
                {
                    if (user.e_mail.Equals(email_tb.Text) && user.password.Equals(password_tb.Text))
                    {
                        LoadingMsg.Text = Message.Update_Checking;
                        LoadingMsg.IsVisible = true;
                        loggedIn.email = user.e_mail;
                        loggedIn.password = user.password;
                        loggedIn.School = DBRetrievalTask.GetSchool(user.School_id);
                        loggedIn.full_names = user.profileName;
                        loggedIn.Library = DBRetrievalTask.GetLibrary(loggedIn.School.SchoolId);
                        string[] SplitSubjectId = user.subjects.Split(delimiter);
                        List<string> SubjectIdList = SplitSubjectId.ToList();
                        List<int> subjectids = ModelTask.SubjectIdsConvert(SubjectIdList);
                        foreach (var id in subjectids)
                        {
                            SubjectModel subject = DBRetrievalTask.GetSubject(id);
                            UserSubjects.Add(subject);
                        }
                        loggedIn.subjects = UserSubjects;
                        found = true;
                        loggedIn.update_status = Constant.finished_update;
                        loggedIn.NotesImagesDownloading = false;
                        await Navigation.PushAsync(new StudentView(loggedIn));
                    }
                }
                if (found == false)
                {
                    OnlineLogin();
                }
            }
        }
        private async void OnlineLogin()
        {
            try
            {
                CreateUser(await JSONTask.LoginJsonObject(email_tb.Text, password_tb.Text), email_tb.Text, password_tb.Text);
            }
            catch
            {
                IsBusy = false;
                LoadingMsg.IsVisible = false;
                ReActivate_Controls();
            }
        }
        private async void CreateUser(Newtonsoft.Json.Linq.JObject loginObject, string username, string password)
        {
            LoginStatus user = JSONTask.Notification(loginObject);
            UserModel userdetails = new UserModel();
            SubjectModel subject = new SubjectModel();
            LibraryModel Library = new LibraryModel();
            if (user.statusCode.Equals("200") && user.statusDescription.Equals("Authentication was successful"))
            {
                userdetails.email = username;
                userdetails.password = password;
                userdetails.School = JSONTask.GetSchool(loginObject);
                userdetails.full_names = JSONTask.GetUsername(loginObject);

                try
                {
                    Library = await JSONTask.Current_Library(username, password, userdetails.School.SchoolId);
                }
                catch
                {

                }
                userdetails.Library = Library;
                LoadingMsg.Text = Message.Syncronization;
                LoadingMsg.IsVisible = true;
                try
                {
                    Newtonsoft.Json.Linq.JArray subjects = await JSONTask.SubjectsJsonArray(username, password);
                    List<int> IDs = JSONTask.SubjectIds(subjects);
                    userdetails.subjects = await JSONTask.Get_New_Subjects(username, password, IDs, subjects);
                    DBInsertionTask.InsertLibAsync(userdetails.Library); //Library add here
                    AuthenticateUser(userdetails);
                }
                catch
                {
                    IsBusy = false;
                    LoadingMsg.IsVisible = false;
                    ReActivate_Controls();
                }

            }
            else
            {
               await DisplayAlert(Message.Login_Header, Message.Wrong_User_details, Message.Ok);
               IsBusy = false;
               LoadingMsg.IsVisible = false;
                ReActivate_Controls();
            }
        }
        private async void AuthenticateUser(UserModel user)
        {
            List<SubjectModel> subs = new List<SubjectModel>();
            List<User> users = DBRetrievalTask.SelectAllUsers();
            bool found = false;
            LibraryModel lib = user.Library;
            subs = user.subjects;
            if (subs.Count > 0)
            {
                if (DependencyService.Get<Interfaces.ITask>().IsInternetConnectionAvailable())
                {
                    if (users == null)
                    {
                        await DBInsertionTask.InsertUserAsync(user);
                        DBInsertionTask.InsertSubjectsAsync(user.subjects);
                        user.update_status = Constant.finished_update;
                        user.NotesImagesDownloading = false;
                        await Navigation.PushAsync(new StudentView(user));
                    }
                    else
                    {
                        foreach (var profile in users)
                        {
                            if (profile.e_mail.Equals(user.email))
                            {
                                found = true;
                            }
                        }
                        if (ModelTask.oldSubjects() != null)
                        {
                            subs = ModelTask.new_subjects(user.subjects);
                            if (subs == null) { }
                            else
                            {
                                DBInsertionTask.InsertSubjectsAsync(user.subjects);
                                if (found == false)
                                {
                                    try
                                    {
                                        await DBInsertionTask.InsertUserAsync(user);
                                    }
                                    catch //(Exception ex)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            await DBInsertionTask.InsertUserAsync(user);
                            DBInsertionTask.InsertSubjectsAsync(user.subjects);
                        }
                        user.update_status = Constant.finished_update;
                        user.NotesImagesDownloading = false;
                        await Navigation.PushAsync(new StudentView(user));
                    }
                }
                else
                {
                    if (ModelTask.oldSubjects() == null)
                    {
                        await DisplayAlert(Message.Content_Header, Message.Offline_Message, Message.Ok);
                    }
                    else
                    {
                        user.update_status = Constant.finished_update;
                        user.NotesImagesDownloading = false;
                        await Navigation.PushAsync(new StudentView(user));
                    }
                }
            }
            else
            {
                await DisplayAlert(Message.No_Subject_Header, Message.No_Subject, Message.Ok);
                IsBusy = false;
                LoadingMsg.IsVisible = false;
                ReActivate_Controls();
            }
        }
    }
}
