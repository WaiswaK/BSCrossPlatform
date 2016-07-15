using BSCrossPlatform.Database;
using BSCrossPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BSCrossPlatform.Core
{
    class DBInsertionTask
    {
        #region Insert Methods
        //Method to insert Subjects into the SQLite Database
        public static void InsertSubjectsAsync(List<SubjectModel> subjects)
        {
            List<TopicModel> topics = new List<TopicModel>();
            List<AttachmentModel> files = new List<AttachmentModel>();
            List<VideoModel> videos = new List<VideoModel>();
            List<AssignmentModel> assignments = new List<AssignmentModel>();
            bool proceed = true;
            try
            {
                var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();

                foreach (var subject in subjects)
                {
                    try
                    {
                        var query = (db.Table<Subject>().Where(c => c.SubjectId == subject.Id)).Single();
                        proceed = false;
                    }
                    catch
                    {
                        proceed = true;
                    }
                    if (proceed == true)
                    {
                        try
                        {
                            db.Insert(new Subject() { SubjectId = subject.Id, name = subject.name, thumb = subject.thumb });
                        }
                        catch
                        {

                        }
                        topics = subject.topics;
                        if (topics.Count > 0)
                        {
                            foreach (var topic in topics)
                            {
                                string Updated_notes = NotesTask.NotesChanger(topic.body); //Update the notes
                                try
                                {
                                    db.Insert(new Topic() { TopicID = topic.TopicID, Notes = topic.body, Updated_Notes = Updated_notes, SubjectId = subject.Id, teacher_full_names = topic.teacher, TopicTitle = topic.TopicTitle, Updated_at = topic.Updated_at, Folder_Id = topic.folder_id, Folder_Name = topic.folder_name });
                                }
                                catch
                                {

                                }
                                files = topic.Files;
                                if (files.Count > 0)
                                {
                                    foreach (var file in files)
                                    {
                                        try
                                        {
                                            db.Insert(new Attachment() { AttachmentID = file.AttachmentID, FileName = file.FileName, FilePath = file.FilePath, TopicID = topic.TopicID, SubjectId = 0, AssignmentID = 0 });
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                            }
                        }

                        videos = subject.videos;
                        if (videos.Count > 0)
                        {
                            foreach (var video in videos)
                            {
                                try
                                {
                                    db.Insert(new Video() { VideoID = video.VideoID, description = video.description, FileName = video.FileName, FilePath = video.FilePath, teacher_full_names = video.teacher, SubjectId = subject.Id });
                                }
                                catch
                                {

                                }
                            }
                        }

                        files = subject.files;
                        if (files.Count > 0)
                        {
                            foreach (var file in files)
                            {
                                try
                                {
                                    db.Insert(new Attachment() { AttachmentID = file.AttachmentID, FileName = file.FileName, FilePath = file.FilePath, TopicID = 0, SubjectId = subject.Id, AssignmentID = 0 });
                                }
                                catch
                                {

                                }
                            }
                        }

                        assignments = subject.assignments;
                        if (assignments.Count > 0)
                        {
                            foreach (var assignment in assignments)
                            {
                                try
                                {
                                    db.Insert(new Assignment() { AssignmentID = assignment.id, description = assignment.description, teacher_full_names = assignment.teacher, title = assignment.title, SubjectId = subject.Id });
                                }
                                catch
                                {

                                }
                                files = assignment.Files;
                                if (files.Count > 0)
                                {
                                    foreach (var file in files)
                                    {
                                        try
                                        {
                                            db.Insert(new Attachment() { AttachmentID = file.AttachmentID, FileName = file.FileName, FilePath = file.FilePath, TopicID = 0, SubjectId = 0, AssignmentID = assignment.id });
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        //Method to Update Subjects in the SQLite Database
        public static void InsertSubjectsUpdateAsync(List<SubjectModel> subjects)
        {
            List<TopicModel> topics = new List<TopicModel>();
            List<AttachmentModel> files = new List<AttachmentModel>();
            try
            {
                var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
                foreach (var subject in subjects)
                {
                    topics = subject.topics;
                    if (topics.Count > 0)
                    {
                        foreach (var topic in topics)
                        {
                            try
                            {
                                db.Insert(new Topic() { TopicID = topic.TopicID, Notes = topic.body, Updated_Notes = NotesTask.NotesChanger(topic.body), SubjectId = subject.Id, teacher_full_names = topic.teacher, TopicTitle = topic.TopicTitle, Updated_at = topic.Updated_at, Folder_Id = topic.folder_id, Folder_Name = topic.folder_name });
                            }
                            catch
                            {

                            }
                            files = topic.Files;
                            if (files.Count > 0)
                            {
                                foreach (var file in files)
                                {
                                    try
                                    {
                                        db.Insert(new Attachment() { AttachmentID = file.AttachmentID, FileName = file.FileName, FilePath = file.FilePath, TopicID = topic.TopicID, SubjectId = 0, AssignmentID = 0 });
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch
            {

            }
        }
        //Task to Insert a User into the database
        public static async Task InsertUserAsync(UserModel user)
        {
            var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
            List<string> subjectsnames = ModelTask.SubjectNames(user.subjects);
            string ConcatSubjects = ModelTask.JoinedSubjects(subjectsnames);
            SchoolModel school = user.School;
            try
            {
                db.Insert(new User() { e_mail = user.email, password = user.password, School_id = school.SchoolId, subjects = ConcatSubjects, profileName = user.full_names });
            }
            catch
            {
            }
            try
            {
                await DependencyService.Get<Interfaces.ITask>().ImageDownloader(school.ImagePath, school.SchoolName);
            }
            catch
            {

            }

            string image_extension = ImageTask.imageFormat(school.ImagePath);
            string newPath = DependencyService.Get<Interfaces.ITask>().imagePath(school.SchoolName + image_extension);
            try
            {
                db.Insert(new School() { SchoolName = school.SchoolName, SchoolBadge = newPath, School_id = school.SchoolId });
            }
            catch
            {

            }
        }
        public static async void InsertLibAsync(LibraryModel lib)
        {
            List<LibCategoryModel> categories = lib.categories;
            var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
            if (categories == null)
            {

            }
            else
            {
                foreach (var category in categories)
                {
                    List<BookModel> books = category.category_books;
                    foreach (var book in books)
                    {
                        try
                        {
                            await DependencyService.Get<Interfaces.ITask>().ImageDownloader(book.thumb_url, book.book_title);
                            string image_extension = ImageTask.imageFormat(book.thumb_url);
                            string newPath = DependencyService.Get<Interfaces.ITask>().imagePath(book.book_title + image_extension);
                            //Insert here book if success here
                            db.Insert(new Book()
                            {
                                Book_id = book.book_id,
                                Book_author = book.book_author,
                                Book_description = book.book_description,
                                Book_title = book.book_title,
                                Category_id = category.category_id,
                                Category_name = category.category_name,
                                file_size = book.file_size,
                                file_url = book.file_url,
                                Library_id = lib.library_id,
                                thumb_url = newPath,
                                updated_at = book.updated_at
                            });

                        }
                        catch
                        {
                            db.Insert(new Book()
                            {
                                Book_id = book.book_id,
                                Book_author = book.book_author,
                                Book_description = book.book_description,
                                Book_title = book.book_title,
                                Category_id = category.category_id,
                                Category_name = category.category_name,
                                file_size = book.file_size,
                                file_url = book.file_url,
                                Library_id = lib.library_id,
                                thumb_url = book.thumb_url,
                                updated_at = book.updated_at
                            });
                        }
                    }
                }
            }
        }
        #endregion
        #region Update Methods
        //Task to Update User
        public static async Task UpdateUserAsync(UserModel user)
        {
            var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
            List<string> subjectsnames = ModelTask.SubjectNames(user.subjects);
            string ConcatSubjects = ModelTask.JoinedSubjects(subjectsnames);
            ConcatSubjects = ModelTask.UserSubjects(ConcatSubjects);
            SchoolModel school = user.School;
            string image_extension = ImageTask.imageFormat(school.ImagePath);
            string newPath = DependencyService.Get<Interfaces.ITask>().imagePath(school.SchoolName + image_extension);
            if (school.ImagePath.Equals(newPath)) { } //Checking if image was downloaded
            else
            {
                await DependencyService.Get<Interfaces.ITask>().ImageDownloader(school.ImagePath, school.SchoolName);
                School sch = new School(school.SchoolId, school.SchoolName, newPath);
                try
                {
                    db.Update(sch);
                }
                catch
                {

                }
            }

            User userInfo = new User(user.email, user.password, user.full_names, ConcatSubjects, school.SchoolId);
            try
            {
                db.Update(userInfo);
            }
            catch
            {

            }
        }
        //Task to Update Subjects
        public static void UpdateSubjectsAsync(List<SubjectModel> subjects)
        {
            List<TopicModel> topics = new List<TopicModel>();
            List<AttachmentModel> files = new List<AttachmentModel>();
            List<VideoModel> videos = new List<VideoModel>();
            List<AssignmentModel> assignments = new List<AssignmentModel>();
            try
            {
                var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
                foreach (var subject in subjects)
                {
                    topics = subject.topics;
                    files = subject.files;
                    assignments = subject.assignments;
                    videos = subject.videos;
                    if (topics != null)
                    {
                        foreach (var topic in topics)
                        {
                            Topic newTopic = new Topic(topic.TopicID, subject.Id, topic.TopicTitle, topic.body, NotesTask.NotesChanger(topic.body), topic.Updated_at, topic.teacher, topic.folder_id, topic.folder_name);
                            try
                            {
                                db.Update(newTopic);
                            }
                            catch
                            {

                            }
                            List<AttachmentModel> topicfiles = topic.Files;
                            List<AttachmentModel> oldfiles = DBRetrievalTask.OldGetFiles(topic.TopicID, 0);
                            List<AttachmentModel> newfiles = ModelTask.GetNewFiles(topicfiles, oldfiles);
                            if (newfiles == null) { }
                            else
                            {
                                foreach (var file in newfiles)
                                {
                                    try
                                    {
                                        db.Insert(new Attachment() { AttachmentID = file.AttachmentID, FileName = file.FileName, FilePath = file.FilePath, TopicID = topic.TopicID, SubjectId = 0, AssignmentID = 0 });
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                    if (videos != null)
                    {
                        foreach (var video in videos)
                        {
                            try
                            {
                                db.Insert(new Video() { VideoID = video.VideoID, description = video.description, FileName = video.FileName, FilePath = video.FilePath, teacher_full_names = video.teacher, SubjectId = subject.Id });
                            }
                            catch
                            {

                            }
                        }
                    }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                db.Insert(new Attachment() { AttachmentID = file.AttachmentID, FileName = file.FileName, FilePath = file.FilePath, TopicID = 0, SubjectId = subject.Id, AssignmentID = 0 });
                            }
                            catch
                            {

                            }
                        }
                    }
                    if (assignments != null)
                    {
                        foreach (var assignment in assignments)
                        {
                            Assignment work = new Assignment(assignment.id, subject.Id, assignment.title, assignment.description, assignment.teacher);
                            db.Update(work);
                            List<AttachmentModel> assignmentfiles = assignment.Files;
                            List<AttachmentModel> oldfiles = DBRetrievalTask.OldGetFiles(0, assignment.id);
                            List<AttachmentModel> newfiles = ModelTask.GetNewFiles(assignmentfiles, oldfiles);
                            if (newfiles == null) { }
                            else
                            {
                                foreach (var file in newfiles)
                                {
                                    try
                                    {
                                        db.Insert(new Attachment() { AttachmentID = file.AttachmentID, FileName = file.FileName, FilePath = file.FilePath, TopicID = 0, SubjectId = 0, AssignmentID = assignment.id });
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }    
        public static async void UpdateLibAsync(LibraryModel lib)
        {
            List<LibCategoryModel> categories = lib.categories;
            var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
            if (categories == null)
            {

            }
            else
            {
                foreach (var category in categories)
                {
                    List<BookModel> books = category.category_books;
                    foreach (var book in books)
                    {
                        await DependencyService.Get<Interfaces.ITask>().ImageDownloader(book.thumb_url, book.book_title);
                        string image_extension = ImageTask.imageFormat(book.thumb_url);
                        string newPath = DependencyService.Get<Interfaces.ITask>().imagePath(book.book_title + image_extension);
                        db.Insert(new Book()
                        {
                            Book_id = book.book_id,
                            Book_author = book.book_author,
                            Book_description = book.book_description,
                            Book_title = book.book_title,
                            Category_id = category.category_id
                            ,
                            Category_name = category.category_name,
                            file_size = book.file_size,
                            file_url = book.file_url,
                            Library_id = lib.library_id,
                            thumb_url = newPath,
                            updated_at = book.updated_at
                        });
                    }
                }
            }
        }
        #endregion
    }
}
