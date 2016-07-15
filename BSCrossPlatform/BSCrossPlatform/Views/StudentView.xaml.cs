using BSCrossPlatform.Core;
using BSCrossPlatform.Database;
using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class StudentView : ContentPage
    {
        /*public StudentView(UserModel user)
        {
            InitializeComponent();
            //BindingContext = new LoginPageViewModel();
            UserModel initial = user;

            char[] delimiter = { '.' };
            List<SubjectModel> subjectsNew = new List<SubjectModel>();
            var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
            var query = (db.Table<User>().Where(c => c.e_mail == user.email)).Single();

            string[] SplitSubjectId = query.subjects.Split(delimiter);
            List<string> SubjectIdList = SplitSubjectId.ToList();
            List<int> subjectids = ModelTask.SubjectIdsConvert(SubjectIdList);
            foreach (var id in subjectids)
            {
                SubjectModel subject = DBRetrievalTask.GetSubject(id);
                subjectsNew.Add(subject);
            }
            user.subjects = subjectsNew;

            LibraryModel lib = DBRetrievalTask.GetLibrary(user.School.SchoolId);
            user.Library = lib;

            BindingContext = new StudentPageViewModel(user);

            if (user.update_status == Constant.finished_update)
            {
                if (DependencyService.Get<Interfaces.ITask>().IsInternetConnectionAvailable())
                {
                    UpdateUser(initial.email, initial.password, DBRetrievalTask.SubjectIdsForUser(initial.email), user.subjects, user);
                    if (user.NotesImagesDownloading == false)
                    {
                        user.NotesImagesDownloading = true;
                        NotesTask.GetNotesImagesSubjectsAsync(user.subjects);
                    }
                }
            }
        }
        */

        //Temporary for testing UI
        public StudentView(UserModel user)
        {
            InitializeComponent();
            BindingContext = new StudentViewModel(user);
        }
        //Methods triggers
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var monkey = ((ListView)sender).SelectedItem as Monkey;
            //if (monkey == null)
              //  return;
        }
        private async void UpdateUser(string username, string password, List<int> oldIDs, List<SubjectModel> InstalledSubjects, UserModel currentUser)
        {
            UserModel userdetails = new UserModel();
            SubjectModel subject = new SubjectModel();
            List<SubjectModel> final = new List<SubjectModel>();
            LibraryModel Current_Library = new LibraryModel();
            LibraryModel Old_Library = DBRetrievalTask.GetLibrary(currentUser.School.SchoolId);
            currentUser.update_status = Constant.updating;
            try
            {
                Current_Library = await JSONTask.Current_Library(username, password, userdetails.School.SchoolId);
            }
            catch
            {
            }
            try
            {
                Newtonsoft.Json.Linq.JObject loginObject = await JSONTask.LoginJsonObject(username, password);
                LoginStatus user = JSONTask.Notification(loginObject);
                if (user.statusCode.Equals("200") && user.statusDescription.Equals("Authentication was successful"))
                {
                    userdetails.email = username;
                    userdetails.password = password;
                    userdetails.School = JSONTask.GetSchool(loginObject);
                    userdetails.full_names = JSONTask.GetUsername(loginObject);
                    try
                    {
                        Newtonsoft.Json.Linq.JArray subjects = await JSONTask.SubjectsJsonArray(username, password);
                        List<SubjectModel> courses = new List<SubjectModel>();
                        List<SubjectModel> newcourses = new List<SubjectModel>();
                        List<int> IDs = JSONTask.SubjectIds(subjects);
                        List<int> NewSubjectIds = ModelTask.newIds(oldIDs, IDs);

                        char[] delimiter = { '.' };
                        var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
                        var query = (db.Table<User>().Where(c => c.e_mail == username)).Single();
                        string[] SplitSubjectId = query.subjects.Split(delimiter);
                        List<string> SubjectIdList = SplitSubjectId.ToList();
                        List<int> subjectids = ModelTask.SubjectIdsConvert(SubjectIdList);
                        List<int> removedIds = ModelTask.newIds(IDs, subjectids);
                        List<SubjectModel> CurrentSubjects = new List<SubjectModel>();
                        List<int> remainedIDs = new List<int>();

                        if (removedIds != null)
                        {
                            remainedIDs = ModelTask.newIds(removedIds, oldIDs);
                        }
                        else
                        {
                            remainedIDs = oldIDs;
                        }
                        if (remainedIDs != null)
                        {
                            foreach (var id in remainedIDs)
                            {
                                SubjectModel subjectremoved = DBRetrievalTask.GetSubject(id);
                                CurrentSubjects.Add(subjectremoved);
                            }
                            InstalledSubjects = CurrentSubjects;
                        }

                        if (remainedIDs == null)
                        {
                            InstalledSubjects = null;
                        }

                        if (NewSubjectIds != null)
                        {
                            CurrentSubjects = await JSONTask.Get_New_Subjects(username, password, NewSubjectIds, subjects);
                            foreach (var sub in CurrentSubjects)
                            {
                                courses.Add(sub);
                                newcourses.Add(sub);
                            }

                            if (remainedIDs == null)
                            {
                                NewSubjectIds = null;
                            }
                            if (remainedIDs != null)
                            {
                                InstalledSubjects.AddRange(courses);
                                NewSubjectIds = ModelTask.newIds(IDs, remainedIDs);
                            }
                            if (NewSubjectIds != null)
                            {
                                List<SubjectModel> oldcourses = await JSONTask.Get_Subjects(username, password, remainedIDs, IDs, subjects);
                                foreach (var course in oldcourses)
                                {
                                    courses.Add(course);
                                }

                                List<SubjectModel> updateable = new List<SubjectModel>();
                                if (remainedIDs == null)
                                {
                                    updateable = null;
                                }
                                else
                                {
                                    updateable = ModelTask.UpdateableSubjects(InstalledSubjects, oldcourses);
                                }

                                if (updateable == null)
                                {
                                    List<SubjectModel> updatedTopics = new List<SubjectModel>();
                                    if (remainedIDs == null)
                                    {
                                        userdetails.subjects = CurrentSubjects;
                                    }
                                    else
                                    {
                                        userdetails.subjects = InstalledSubjects;
                                        updatedTopics = ModelTask.UpdateableSubjectsTopics(InstalledSubjects, oldcourses);
                                        if (updatedTopics != null)
                                        {
                                            DBInsertionTask.InsertSubjectsUpdateAsync(updatedTopics);
                                        }

                                    }

                                    LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                    List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);


                                    if (newContentLibrary == null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, null, currentUser, null, updatedOldContentLibrary);
                                    }
                                    else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, null, currentUser, null, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, null, currentUser, newContentLibrary, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, null, currentUser, newContentLibrary, updatedOldContentLibrary);
                                    }
                                }
                                else
                                {
                                    List<SubjectModel> updatedTopics = new List<SubjectModel>();
                                    userdetails.subjects = InstalledSubjects;
                                    updatedTopics = ModelTask.UpdateableSubjectsTopics(InstalledSubjects, oldcourses);
                                    if (updatedTopics != null)
                                    {
                                        DBInsertionTask.InsertSubjectsUpdateAsync(updatedTopics);
                                    }
                                    LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                    List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);
                                    if (newContentLibrary == null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, updateable, currentUser, null, updatedOldContentLibrary);
                                    }
                                    else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, updateable, currentUser, null, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, updateable, currentUser, newContentLibrary, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, courses, updateable, currentUser, newContentLibrary, updatedOldContentLibrary);
                                    }
                                }
                            }
                            else
                            {
                                if (remainedIDs == null)
                                {
                                    userdetails.subjects = CurrentSubjects;
                                    LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                    List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);
                                    if (newContentLibrary == null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, null, updatedOldContentLibrary);
                                    }
                                    else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, null, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, newContentLibrary, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, newContentLibrary, updatedOldContentLibrary);
                                    }
                                }
                                else
                                {
                                    List<SubjectModel> oldcourses = await JSONTask.Get_Subjects(username, password, remainedIDs, IDs, subjects);
                                    List<SubjectModel> updateable = ModelTask.UpdateableSubjects(InstalledSubjects, oldcourses);

                                    if (updateable == null)
                                    {
                                        userdetails.subjects = InstalledSubjects;
                                        List<SubjectModel> updatedTopics = new List<SubjectModel>();
                                        updatedTopics = ModelTask.UpdateableSubjectsTopics(InstalledSubjects, oldcourses);
                                        if (updatedTopics != null)
                                        {
                                            DBInsertionTask.InsertSubjectsUpdateAsync(updatedTopics);
                                        }
                                        LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                        List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);
                                        if (newContentLibrary == null && updatedOldContentLibrary != null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, null, updatedOldContentLibrary);
                                        }
                                        else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, null, null);
                                        }
                                        else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, newContentLibrary, null);
                                        }
                                        else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, null, currentUser, newContentLibrary, updatedOldContentLibrary);
                                        }
                                    }
                                    else
                                    {
                                        userdetails.subjects = InstalledSubjects;
                                        List<SubjectModel> updatedTopics = new List<SubjectModel>();
                                        updatedTopics = ModelTask.UpdateableSubjectsTopics(InstalledSubjects, oldcourses);
                                        if (updatedTopics != null)
                                        {
                                            DBInsertionTask.InsertSubjectsUpdateAsync(updatedTopics);
                                        }
                                        LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                        List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);
                                        if (newContentLibrary == null && updatedOldContentLibrary != null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, updateable, currentUser, null, updatedOldContentLibrary);
                                        }
                                        else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, updateable, currentUser, null, null);

                                        }
                                        else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, updateable, currentUser, newContentLibrary, null);
                                        }
                                        else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                        {
                                            ModelTask.UserUpdater(userdetails, newcourses, updateable, currentUser, newContentLibrary, updatedOldContentLibrary);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (remainedIDs == null)
                            {
                                userdetails.subjects = null;
                                LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);
                                if (newContentLibrary == null && updatedOldContentLibrary != null)
                                {
                                    ModelTask.UserUpdater(userdetails, null, null, currentUser, null, updatedOldContentLibrary);
                                }
                                else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                {
                                    ModelTask.UserUpdater(userdetails, null, null, currentUser, null, null);
                                }
                                else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                {
                                    ModelTask.UserUpdater(userdetails, null, null, currentUser, newContentLibrary, null);
                                }
                                else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                {
                                    ModelTask.UserUpdater(userdetails, null, null, currentUser, newContentLibrary, updatedOldContentLibrary);
                                }
                            }
                            else
                            {
                                List<SubjectModel> oldcourses = await JSONTask.Get_Subjects(username, password, remainedIDs, IDs, subjects);
                                foreach (var course in oldcourses)
                                {
                                    courses.Add(course);
                                }
                                List<SubjectModel> updateable = ModelTask.UpdateableSubjects(InstalledSubjects, oldcourses);
                                if (updateable == null)
                                {
                                    userdetails.subjects = InstalledSubjects;
                                    List<SubjectModel> updatedTopics = ModelTask.UpdateableSubjectsTopics(InstalledSubjects, oldcourses);
                                    if (updatedTopics != null)
                                    {
                                        DBInsertionTask.InsertSubjectsUpdateAsync(updatedTopics);
                                    }
                                    LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                    List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);
                                    if (newContentLibrary == null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, null, currentUser, null, updatedOldContentLibrary);
                                    }
                                    else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, null, currentUser, null, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, null, currentUser, newContentLibrary, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, null, currentUser, newContentLibrary, updatedOldContentLibrary);
                                    }
                                }
                                else
                                {
                                    userdetails.subjects = InstalledSubjects;
                                    List<SubjectModel> updatedTopics = ModelTask.UpdateableSubjectsTopics(InstalledSubjects, oldcourses);
                                    if (updatedTopics != null)
                                    {
                                        DBInsertionTask.InsertSubjectsUpdateAsync(updatedTopics);
                                    }
                                    LibraryModel newContentLibrary = ModelTask.CompareLibraries(Old_Library, Current_Library);
                                    List<LibCategoryModel> updatedOldContentLibrary = ModelTask.Categories_Update(Old_Library.categories, newContentLibrary.categories);
                                    if (newContentLibrary == null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, updateable, currentUser, null, updatedOldContentLibrary);
                                    }
                                    else if (newContentLibrary == null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, updateable, currentUser, null, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary == null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, updateable, currentUser, newContentLibrary, null);
                                    }
                                    else if (newContentLibrary != null && updatedOldContentLibrary != null)
                                    {
                                        ModelTask.UserUpdater(userdetails, null, updateable, currentUser, newContentLibrary, updatedOldContentLibrary);
                                    }
                                }
                            }
                        }
                        currentUser.update_status = Constant.finished_update;
                    }

                    catch
                    {
                    }
                }
                else
                {

                }
            }
            catch
            {

            }
        }
    }
}
