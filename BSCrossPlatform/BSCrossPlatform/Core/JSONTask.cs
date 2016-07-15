using BSCrossPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace BSCrossPlatform.Core
{
    class JSONTask
    {
        private static string Teacher(Newtonsoft.Json.Linq.JObject obj)
        {
            string teacher_names = string.Empty;
            teacher_names = obj.Value<string>("full_name");
            return teacher_names;
        }
        #region Login JSON Methods
        public static LoginStatus Notification(Newtonsoft.Json.Linq.JObject loginObject)
        {
            LoginStatus user = new LoginStatus();
            user.statusCode = loginObject.Value<string>("statusCode");
            user.statusDescription = loginObject.Value<string>("statusDescription");
            return user;
        }
        public static SchoolModel GetSchool(Newtonsoft.Json.Linq.JObject loginObject)
        {
            SchoolModel user = new SchoolModel();
            user.SchoolId = loginObject.Value<int>("school_id");
            user.SchoolName = loginObject.Value<string>("school");
            user.ImagePath = Constant.BaseUri + loginObject.Value<string>("school_logo");
            return user;
        }
        public static string GetUsername(Newtonsoft.Json.Linq.JObject loginObject)
        {
            string user = string.Empty;
            user = loginObject.Value<string>("name");
            return user;
        }
        public static async Task<Newtonsoft.Json.Linq.JObject> LoginJsonObject(string username, string password)
        {
            Newtonsoft.Json.Linq.JObject loginObject = new Newtonsoft.Json.Linq.JObject();
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("email", username));
            postData.Add(new KeyValuePair<string, string>("password", password));
            var formContent = new FormUrlEncodedContent(postData);
            var authresponse = await client.PostAsync(Constant.LoginJsonLink, formContent);
            var authresult = await authresponse.Content.ReadAsStreamAsync();
            var authstreamReader = new System.IO.StreamReader(authresult);
            var authresponseContent = authstreamReader.ReadToEnd().Trim().ToString();
            loginObject = Newtonsoft.Json.Linq.JObject.Parse(authresponseContent);
            return loginObject;
        }
        #endregion
        #region Subjects JSON Methods
        public static SubjectModel GetSubject(Newtonsoft.Json.Linq.JArray SubjectsArray, int Sub_id, Newtonsoft.Json.Linq.JArray NotesArray, Newtonsoft.Json.Linq.JArray VideosArray, Newtonsoft.Json.Linq.JArray AssignmentArray, Newtonsoft.Json.Linq.JArray FilesArray)
        {
            SubjectModel subject = new SubjectModel();
            int temp = 0;
            foreach (var item in SubjectsArray)
            {
                var obj = item as Newtonsoft.Json.Linq.JObject;
                temp = obj.Value<int>("id");
                if (temp == Sub_id)
                {
                    subject.Id = Sub_id;
                    subject.name = obj.Value<string>("name");
                    subject.thumb = "ms-appx:///Assets/Course/course.jpg";
                    var noteslist = (from i in NotesArray select i as Newtonsoft.Json.Linq.JObject).ToList();
                    subject.topics = GetTopics(noteslist);
                    var videoslist = (from i in VideosArray select i as Newtonsoft.Json.Linq.JObject).ToList();
                    subject.videos = GetVideos(videoslist);
                    var fileslist = (from i in FilesArray select i as Newtonsoft.Json.Linq.JObject).ToList();
                    subject.files = GetFiles(fileslist);
                    var assignmentlist = (from i in AssignmentArray select i as Newtonsoft.Json.Linq.JObject).ToList();
                    subject.assignments = GetAssignments(assignmentlist);
                }
            }
            return subject;
        }
        public static List<int> SubjectIds(Newtonsoft.Json.Linq.JArray SubjectsArray)
        {
            int id;
            List<int> ids = new List<int>();
            foreach (var item in SubjectsArray)
            {
                var obj = item as Newtonsoft.Json.Linq.JObject;
                id = obj.Value<int>("id");
                ids.Add(id);
            }
            return ids;
        }
        private static List<AttachmentModel> AllFiles(List<Newtonsoft.Json.Linq.JObject> AllObjects)
        {
            List<AttachmentModel> Files = new List<AttachmentModel>();
            foreach (var SingleObject in AllObjects)
            {
                AttachmentModel file = SingleFile(SingleObject);
                Files.Add(file);
            }
            return Files;
        }
        private static AttachmentModel SingleFile(Newtonsoft.Json.Linq.JObject obj)
        {
            AttachmentModel attachment = new AttachmentModel();
            attachment.AttachmentID = obj.Value<int>("id");
            attachment.FileName = obj.Value<string>("name");
            attachment.FilePath = obj.Value<string>("absolute_uri");
            return attachment;
        }        
        public static async Task <List<SubjectModel>> Get_Subjects(string username, string password,List<int> remainedIDs, List<int> IDs, Newtonsoft.Json.Linq.JArray subjects)
        {
            List<int> UpdateIds = ModelTask.oldIds(remainedIDs, IDs);
            List<SubjectModel> oldcourses = new List<SubjectModel>();

            foreach (var id in UpdateIds)
            {
                var notes_httpclient = new HttpClient();
                var notes_postData = new List<KeyValuePair<string, string>>();
                notes_postData.Add(new KeyValuePair<string, string>("email", username));
                notes_postData.Add(new KeyValuePair<string, string>("password", password));
                notes_postData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var notes_formContent = new FormUrlEncodedContent(notes_postData);
                var notes_response = await notes_httpclient.PostAsync(Constant.NotesJsonLink, notes_formContent);
                var notes_result = await notes_response.Content.ReadAsStreamAsync();
                var notes_streamReader = new System.IO.StreamReader(notes_result);
                var notes_responseContent = notes_streamReader.ReadToEnd().Trim().ToString();
                var notes = Newtonsoft.Json.Linq.JArray.Parse(notes_responseContent);

                var videos_httpclient = new HttpClient();
                var videospostData = new List<KeyValuePair<string, string>>();
                videospostData.Add(new KeyValuePair<string, string>("email", username));
                videospostData.Add(new KeyValuePair<string, string>("password", password));
                videospostData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var videosformContent = new FormUrlEncodedContent(videospostData);
                var videosresponse = await videos_httpclient.PostAsync(Constant.VideosJsonLink, videosformContent);
                var videosresult = await videosresponse.Content.ReadAsStreamAsync();
                var videosstreamReader = new System.IO.StreamReader(videosresult);
                var videosresponseContent = videosstreamReader.ReadToEnd().Trim().ToString();
                var videos = Newtonsoft.Json.Linq.JArray.Parse(videosresponseContent);

                var assgnmt_httpclient = new HttpClient();
                var assgnmt_postData = new List<KeyValuePair<string, string>>();
                assgnmt_postData.Add(new KeyValuePair<string, string>("email", username));
                assgnmt_postData.Add(new KeyValuePair<string, string>("password", password));
                assgnmt_postData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var assgnmt_formContent = new FormUrlEncodedContent(assgnmt_postData);
                var assgnmt_response = await assgnmt_httpclient.PostAsync(Constant.AssignmentJsonLink, assgnmt_formContent);
                var assgnmt_result = await assgnmt_response.Content.ReadAsStreamAsync();
                var assgnmt_streamReader = new System.IO.StreamReader(assgnmt_result);
                var assgnmt_responseContent = assgnmt_streamReader.ReadToEnd().Trim().ToString();
                var assignments = Newtonsoft.Json.Linq.JArray.Parse(assgnmt_responseContent);

                var file_httpclient = new HttpClient();
                var file_postData = new List<KeyValuePair<string, string>>();
                file_postData.Add(new KeyValuePair<string, string>("email", username));
                file_postData.Add(new KeyValuePair<string, string>("password", password));
                file_postData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var file_formContent = new FormUrlEncodedContent(file_postData);
                var file_response = await file_httpclient.PostAsync(Constant.FilesJsonLink, file_formContent);
                var file_result = await file_response.Content.ReadAsStreamAsync();
                var file_streamReader = new System.IO.StreamReader(file_result);
                var file_responseContent = file_streamReader.ReadToEnd().Trim().ToString();
                var files = Newtonsoft.Json.Linq.JArray.Parse(file_responseContent);

                SubjectModel subject = GetSubject(subjects, id, notes, videos, assignments, files);
                oldcourses.Add(subject);
            }
            return oldcourses;
        }
        public static async Task<List<SubjectModel>> Get_New_Subjects(string username, string password, List<int> NewSubjectIds, Newtonsoft.Json.Linq.JArray subjects)
        {
            List<SubjectModel> CurrentSubjects = new List<SubjectModel>();
            foreach (var id in NewSubjectIds)
            {
                var notes_httpclient = new HttpClient();
                var notes_postData = new List<KeyValuePair<string, string>>();
                notes_postData.Add(new KeyValuePair<string, string>("email", username));
                notes_postData.Add(new KeyValuePair<string, string>("password", password));
                notes_postData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var notes_formContent = new FormUrlEncodedContent(notes_postData);
                var notes_response = await notes_httpclient.PostAsync(Constant.NotesJsonLink, notes_formContent);
                var notes_result = await notes_response.Content.ReadAsStreamAsync();
                var notes_streamReader = new System.IO.StreamReader(notes_result);
                var notes_responseContent = notes_streamReader.ReadToEnd().Trim().ToString();
                var notes = Newtonsoft.Json.Linq.JArray.Parse(notes_responseContent);

                var videos_httpclient = new HttpClient();
                var videospostData = new List<KeyValuePair<string, string>>();
                videospostData.Add(new KeyValuePair<string, string>("email", username));
                videospostData.Add(new KeyValuePair<string, string>("password", password));
                videospostData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var videosformContent = new FormUrlEncodedContent(videospostData);
                var videosresponse = await videos_httpclient.PostAsync(Constant.VideosJsonLink, videosformContent);
                var videosresult = await videosresponse.Content.ReadAsStreamAsync();
                var videosstreamReader = new System.IO.StreamReader(videosresult);
                var videosresponseContent = videosstreamReader.ReadToEnd().Trim().ToString();
                var videos = Newtonsoft.Json.Linq.JArray.Parse(videosresponseContent);

                var assgnmt_httpclient = new HttpClient();
                var assgnmt_postData = new List<KeyValuePair<string, string>>();
                assgnmt_postData.Add(new KeyValuePair<string, string>("email", username));
                assgnmt_postData.Add(new KeyValuePair<string, string>("password", password));
                assgnmt_postData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var assgnmt_formContent = new FormUrlEncodedContent(assgnmt_postData);
                var assgnmt_response = await assgnmt_httpclient.PostAsync(Constant.AssignmentJsonLink, assgnmt_formContent);
                var assgnmt_result = await assgnmt_response.Content.ReadAsStreamAsync();
                var assgnmt_streamReader = new System.IO.StreamReader(assgnmt_result);
                var assgnmt_responseContent = assgnmt_streamReader.ReadToEnd().Trim().ToString();
                var assignments = Newtonsoft.Json.Linq.JArray.Parse(assgnmt_responseContent);

                var file_httpclient = new HttpClient();
                var file_postData = new List<KeyValuePair<string, string>>();
                file_postData.Add(new KeyValuePair<string, string>("email", username));
                file_postData.Add(new KeyValuePair<string, string>("password", password));
                file_postData.Add(new KeyValuePair<string, string>("id", id.ToString()));
                var file_formContent = new FormUrlEncodedContent(file_postData);
                var file_response = await file_httpclient.PostAsync(Constant.FilesJsonLink, file_formContent);
                var file_result = await file_response.Content.ReadAsStreamAsync();
                var file_streamReader = new System.IO.StreamReader(file_result);
                var file_responseContent = file_streamReader.ReadToEnd().Trim().ToString();
                var files = Newtonsoft.Json.Linq.JArray.Parse(file_responseContent);

                SubjectModel subject = GetSubject(subjects, id, notes, videos, assignments, files);
                CurrentSubjects.Add(subject);
            }
            return CurrentSubjects;
        }        
        public static async Task<Newtonsoft.Json.Linq.JArray> SubjectsJsonArray(string username, string password)
        {
            Newtonsoft.Json.Linq.JArray subjects = new Newtonsoft.Json.Linq.JArray();
            var units_http_client = new HttpClient();
            var units_postData = new List<KeyValuePair<string, string>>();
            units_postData.Add(new KeyValuePair<string, string>("email", username));
            units_postData.Add(new KeyValuePair<string, string>("password", password));
            var units_formContent = new FormUrlEncodedContent(units_postData);
            var courseresponse = await units_http_client.PostAsync(Constant.CourseJsonLink, units_formContent);
            var coursesresult = await courseresponse.Content.ReadAsStreamAsync();
            var coursestreamReader = new System.IO.StreamReader(coursesresult);
            var courseresponseContent = coursestreamReader.ReadToEnd().Trim().ToString();
            subjects = Newtonsoft.Json.Linq.JArray.Parse(courseresponseContent);
            return subjects;
        }
        #endregion
        #region Topics JSON Methods
        private static List<TopicModel> GetTopics(List<Newtonsoft.Json.Linq.JObject> AllObjects)
        {
            List<TopicModel> Topics = new List<TopicModel>();
            foreach (var SingleObject in AllObjects)
            {
                TopicModel topic = SingleTopic(SingleObject);
                Topics.Add(topic);
            }
            return Topics;
        }
        private static TopicModel SingleTopic(Newtonsoft.Json.Linq.JObject obj)
        {
            TopicModel topic = new TopicModel();
            topic.TopicID = obj.Value<int>("id");
            topic.TopicTitle = obj.Value<string>("title");
            topic.Updated_at = obj.Value<string>("updated_at");
            topic.body = obj.Value<string>("body");
            topic.folder_name = obj.Value<string>("folder_name");
            topic.folder_id = obj.Value<int>("folder_id");
            var teacherObject = obj.Value<Newtonsoft.Json.Linq.JObject>("teacher");
            var attachmentArray = obj.Value<Newtonsoft.Json.Linq.JArray>("attachments");
            var list = (from i in attachmentArray select i as Newtonsoft.Json.Linq.JObject).ToList();
            topic.Files = AllFiles(list); topic.teacher = Teacher(teacherObject);
            return topic;
        }
        private static List<VideoModel> GetVideos(List<Newtonsoft.Json.Linq.JObject> AllObjects)
        {
            List<VideoModel> videos = new List<VideoModel>();
            foreach (var SingleObject in AllObjects)
            {
                VideoModel video = SingleVideo(SingleObject);
                videos.Add(video);
            }
            return videos;
        }
        private static VideoModel SingleVideo(Newtonsoft.Json.Linq.JObject obj)
        {
            VideoModel video = new VideoModel();
            video.VideoID = obj.Value<int>("id");
            video.FileName = obj.Value<string>("title");
            video.FilePath = obj.Value<string>("link");
            video.description = obj.Value<string>("description");
            var teacherObject = obj.Value <Newtonsoft.Json.Linq.JObject>("teacher");
            video.teacher = Teacher(teacherObject);
            return video;
        }
        private static List<AssignmentModel> GetAssignments(List<Newtonsoft.Json.Linq.JObject> AllObjects)
        {
            List<AssignmentModel> Assignments = new List<AssignmentModel>();
            foreach (var SingleObject in AllObjects)
            {
                AssignmentModel Assignment = SingleAssignment(SingleObject);
                Assignments.Add(Assignment);
            }
            return Assignments;
        }
        private static AssignmentModel SingleAssignment(Newtonsoft.Json.Linq.JObject obj)
        {
            AssignmentModel assignment = new AssignmentModel();
            assignment.id = obj.Value<int>("id");
            assignment.title = obj.Value<string>("title");
            assignment.description = obj.Value<string>("description");
            var teacherObject = obj.Value<Newtonsoft.Json.Linq.JObject>("teacher");
            assignment.teacher = Teacher(teacherObject);
            var attachmentArray = obj.Value<Newtonsoft.Json.Linq.JArray>("attachments");
            var list = (from i in attachmentArray select i as Newtonsoft.Json.Linq.JObject).ToList();
            assignment.Files = AllFiles(list);
            return assignment;
        }
        private static List<AttachmentModel> GetFiles(List<Newtonsoft.Json.Linq.JObject> AllObjects)
        {
            List<AttachmentModel> files = new List<AttachmentModel>();
            foreach (var SingleObject in AllObjects)
            {
                AttachmentModel file = AFile(SingleObject);
                files.Add(file);
            }
            return files;
        }
        private static AttachmentModel AFile(Newtonsoft.Json.Linq.JObject obj)
        {
            AttachmentModel attachment = new AttachmentModel();
            attachment.AttachmentID = obj.Value<int>("id");
            attachment.FileName = obj.Value<string>("name");
            attachment.FilePath = obj.Value<string>("url");
            return attachment;
        }
        #endregion
        #region Library JSON Methods
        private static List<LibCategoryModel> GetLibraryCategories(List<Newtonsoft.Json.Linq.JObject> AllObjects, int library_id)
        {
            List<LibCategoryModel> categories = new List<LibCategoryModel>();
            foreach (var SingleObject in AllObjects)
            {
                LibCategoryModel category = LibraryCategory(SingleObject, library_id);
                categories.Add(category);
            }
            return categories;
        }
        private static LibCategoryModel LibraryCategory(Newtonsoft.Json.Linq.JObject obj, int Library_id)
        {
            List<BookModel> tempbooks = new List<BookModel>();
            LibCategoryModel category = new LibCategoryModel();
            category.category_id = obj.Value<int>("id");
            category.category_name = obj.Value<string>("name");
            category.book_count = obj.Value<int>("book_count");
            var BooksArray = obj.Value<Newtonsoft.Json.Linq.JArray>("books");
            var BookList = (from i in BooksArray select i as Newtonsoft.Json.Linq.JObject).ToList();
            category.category_books = GetBooks(BookList, Library_id, category.category_id, category.category_name);
            return category;
        }
        private static List<BookModel> GetBooks(List<Newtonsoft.Json.Linq.JObject> AllObjects, int lib_id, int category_id, string category_name)
        {
            List<BookModel> books = new List<BookModel>();
            foreach (var SingleObject in AllObjects)
            {
                BookModel Book = SingleBook(SingleObject, lib_id, category_id, category_name);
                books.Add(Book);
            }
            return books;
        }
        private static BookModel SingleBook(Newtonsoft.Json.Linq.JObject obj, int lib_id, int category_id, string category_name)
        {
            BookModel book = new BookModel();
            book.Category_id = category_id;
            book.Library_id = lib_id;
            book.Category_name = category_name;
            book.book_id = obj.Value<int>("id");
            book.book_title = obj.Value<string>("title");
            book.book_author = obj.Value<string>("author");
            book.book_description = obj.Value<string>("description");
            book.thumb_url = obj.Value<string>("thumb_url");
            book.file_url = obj.Value<string>("file_url");
            book.file_size = obj.Value<int>("file_size");
            return book;
        }
        public static LibraryModel GetLibrary(Newtonsoft.Json.Linq.JArray LibraryArray, int id)
        {
            LibraryModel library = new LibraryModel();
            var CategoryList = (from i in LibraryArray select i as Newtonsoft.Json.Linq.JObject).ToList();
            library.library_id = id;
            library.categories = GetLibraryCategories(CategoryList, id);
            return library;
        }
        public static async Task<LibraryModel> Current_Library(string username, string password, int school_id)
        {
            LibraryModel Current = new LibraryModel();
            try
            {
                var library_httpclient = new HttpClient();
                var library_postData = new List<KeyValuePair<string, string>>();
                library_postData.Add(new KeyValuePair<string, string>("email", username));
                library_postData.Add(new KeyValuePair<string, string>("password", password));
                library_postData.Add(new KeyValuePair<string, string>("id", school_id.ToString()));
                var library_formContent = new FormUrlEncodedContent(library_postData);
                var library_response = await library_httpclient.PostAsync(Constant.BooksJsonLink, library_formContent);
                var library_result = await library_response.Content.ReadAsStreamAsync();
                var library_streamReader = new System.IO.StreamReader(library_result);
                var library_responseContent = library_streamReader.ReadToEnd().Trim().ToString();
                var library = Newtonsoft.Json.Linq.JArray.Parse(library_responseContent);
                Current = GetLibrary(library, school_id);
            }
            catch 
            {
                
            }
            return Current;
        }     
        #endregion
    }
}
