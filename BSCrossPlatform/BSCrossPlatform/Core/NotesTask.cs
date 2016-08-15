using BSCrossPlatform.Database;
using BSCrossPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BSCrossPlatform.Core
{
    class NotesTask
    {
        //Offline notes
        private static async Task<string> Offline_Notes(string notes, string subject, string topic)
        {
            string http_start = "http://";
            string https_start = "https://";
            int notes_tiff = 0;
            int notes_bmp = 0;
            int notes_gif = 0;
            int notes_png = 0;
            int notes_jpg = 0;

            #region http images
            string expression_png = http_start + @"\S*" + Constant.PNG_extension;
            string expression_jpg = http_start + @"\S*" + Constant.JPG_extension;
            string expression_gif = http_start + @"\S*" + Constant.GIF_extension;
            string expression_bmp = http_start + @"\S*" + Constant.BMP_extension;
            string expression_tiff = http_start + @"\S*" + Constant.TIFF_extension;

            //Upper Case
            string expression_PNG = http_start + @"\S*" + Constant.PNG_extension.ToUpper();
            string expression_JPG = http_start + @"\S*" + Constant.JPG_extension.ToUpper();
            string expression_GIF = http_start + @"\S*" + Constant.GIF_extension.ToUpper();
            string expression_BMP = http_start + @"\S*" + Constant.BMP_extension.ToUpper();
            string expression_TIFF = http_start + @"\S*" + Constant.TIFF_extension.ToUpper();
            #endregion

            #region https images
            string expressions_png = https_start + @"\S*" + Constant.PNG_extension;
            string expressions_jpg = https_start + @"\S*" + Constant.JPG_extension;
            string expressions_gif = https_start + @"\S*" + Constant.GIF_extension;
            string expressions_bmp = https_start + @"\S*" + Constant.BMP_extension;
            string expressions_tiff = https_start + @"\S*" + Constant.TIFF_extension;

            //Upper Case
            string expressions_PNG = https_start + @"\S*" + Constant.PNG_extension.ToUpper();
            string expressions_JPG = https_start + @"\S*" + Constant.JPG_extension.ToUpper();
            string expressions_GIF = https_start + @"\S*" + Constant.GIF_extension.ToUpper();
            string expressions_BMP = https_start + @"\S*" + Constant.BMP_extension.ToUpper();
            string expressions_TIFF = https_start + @"\S*" + Constant.TIFF_extension.ToUpper();
            #endregion

            #region Links with jpg
            List<string> jpg_links = Links(notes, expression_jpg);
            List<string> jpg_https = Links(notes, expressions_jpg);
            foreach (var link in jpg_https)
            {
                jpg_links.Add(link);
            }
            #region Upper Case
            List<string> JPG_links = Links(notes, expression_JPG);
            foreach (var link in JPG_links)
            {
                jpg_links.Add(link);
            }
            List<string> JPG_https = Links(notes, expressions_JPG);
            foreach (var link in JPG_https)
            {
                jpg_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with png
            List<string> png_links = Links(notes, expression_png);
            List<string> png_https = Links(notes, expressions_png);
            foreach (var link in png_https)
            {
                png_links.Add(link);
            }
            #region Upper Case
            List<string> PNG_links = Links(notes, expression_PNG);
            foreach (var link in PNG_links)
            {
                png_links.Add(link);
            }
            List<string> PNG_https = Links(notes, expressions_PNG);
            foreach (var link in PNG_https)
            {
                png_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with gif
            List<string> gif_links = Links(notes, expression_gif);
            List<string> gif_https = Links(notes, expressions_gif);
            foreach (var link in gif_https)
            {
                gif_links.Add(link);
            }
            #region Upper Case
            List<string> GIF_links = Links(notes, expression_GIF);
            foreach (var link in GIF_links)
            {
                gif_links.Add(link);
            }
            List<string> GIF_https = Links(notes, expressions_GIF);
            foreach (var link in GIF_https)
            {
                gif_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with bmp
            List<string> bmp_links = Links(notes, expression_bmp);
            List<string> bmp_https = Links(notes, expressions_bmp);
            foreach (var link in bmp_https)
            {
                bmp_links.Add(link);
            }
            #region Upper Case
            List<string> BMP_links = Links(notes, expression_BMP);
            foreach (var link in BMP_links)
            {
                bmp_links.Add(link);
            }
            List<string> BMP_https = Links(notes, expressions_BMP);
            foreach (var link in BMP_https)
            {
                bmp_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with tiff
            List<string> tiff_links = Links(notes, expression_tiff);
            List<string> tiff_https = Links(notes, expressions_tiff);
            foreach (var link in tiff_https)
            {
                tiff_links.Add(link);
            }
            #region Upper Case
            List<string> TIFF_links = Links(notes, expression_TIFF);
            foreach (var link in TIFF_links)
            {
                tiff_links.Add(link);
            }
            List<string> TIFF_https = Links(notes, expressions_TIFF);
            foreach (var link in TIFF_https)
            {
                tiff_links.Add(link);
            }
            #endregion
            #endregion

            string new_notes = notes;
            
            foreach (string _string in jpg_links)
            {
                notes_jpg++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_jpg.ToString();
                string imageName = "data:image/jpg;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.JPG_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in png_links)
            {
                notes_png++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_png.ToString();
                string imageName = "data:image/png;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.PNG_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in gif_links)
            {
                notes_gif++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_gif.ToString();
                string imageName = "data:image/gif;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.GIF_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in bmp_links)
            {
                notes_bmp++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_bmp.ToString();
                string imageName = "data:image/bmp;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.BMP_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in tiff_links)
            {
                notes_tiff++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_tiff.ToString();
                string imageName = "data:image/tiff;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.TIFF_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            return new_notes;
        }
        //Method to change notes by updating links
        public static string NotesChanger(string notes)
        {
            string start_string = "/assets/content_images/";
            string expression = start_string + @"\S*\d{10}"; //Ending with 10 digits and starting with /assets/content_images/
            List<string> downloadLinks = Links(notes, expression); //First expression
            string new_notes = notes;
            foreach (string _string in downloadLinks)
            {
                string _newString = _string.Replace(ImageTask.imageNumbers(_string), string.Empty); //Removing the 10 
                _newString = _newString.Replace("?", string.Empty); //Removing the ? Mark
                _newString = Constant.BaseUri + _newString; //New link to be added in notes
                _newString = _newString.Trim();
                new_notes = new_notes.Replace(_string, _newString); //Replace with link that is full
            }
            return new_notes;
        }
        //Method to get all image link locations in notes
        private static List<string> Links(string text, string expr)
        {
            List<string> collection = new List<string>();
            List<string> links = new List<string>();
            MatchCollection mc = Regex.Matches(text, expr);
            foreach (Match m in mc)
            {
                collection.Add(m.ToString());
            }
            return collection;
        }
        #region Notes Loader
        public static async Task<string> Notes_loader(TopicModel topic)
        {
            string subject_name = string.Empty;
            string topic_name = topic.folder_name;
            try
            {
                using (var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection())
                {
                    var query_topic = (db.Table<Topic>().Where(c => c.TopicID == topic.TopicID)).Single();
                    var query_subject = (db.Table<Subject>().Where(c => c.SubjectId == query_topic.SubjectId)).Single();
                    subject_name = query_subject.name;
                }
            }
            catch (Exception ex)
            {
                ErrorLogTask Logfile = new ErrorLogTask();
                Logfile.Error_details = ex.ToString();
                Logfile.Error_title = "Notes_Loader Method";
                Logfile.Location = "NotesTask";
                ErrorLogTask.LogFileSaveAsync(Logfile);
            }
            string _notes = await Offline_Notes(topic.notes, subject_name, topic_name);
            return _notes;
        }
        public static async Task<string> Notes_loader(AssignmentModel assignment)
        {
            string subject_name = string.Empty;
            string assignment_title = assignment.title;
            try
            {
                using (var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection())
                {
                    var query_assignment = (db.Table<Assignment>().Where(c => c.AssignmentID == assignment.id)).Single();
                    var query_subject = (db.Table<Subject>().Where(c => c.SubjectId == query_assignment.SubjectId)).Single();
                    subject_name = query_subject.name;
                }
            }
            catch (Exception ex)
            {
                ErrorLogTask Logfile = new ErrorLogTask();
                Logfile.Error_details = ex.ToString();
                Logfile.Error_title = "Notes_Loader Method";
                Logfile.Location = "NotesTask";
                ErrorLogTask.LogFileSaveAsync(Logfile);
            }
            string _notes = await Offline_Notes(assignment.description, subject_name, assignment_title);
            return _notes;
        }
        #endregion       
        #region Notes image download methods
        private static async void NotesImageDownloader(string notes, string subject, string topic)
        {
            string http_start = "http://";
            string https_start = "https://";
            int notes_tiff = 0;
            int notes_bmp = 0;
            int notes_gif = 0;
            int notes_png = 0;
            int notes_jpg = 0;

            #region http images
            string expression_png = http_start + @"\S*" + Constant.PNG_extension;
            string expression_jpg = http_start + @"\S*" + Constant.JPG_extension;
            string expression_gif = http_start + @"\S*" + Constant.GIF_extension;
            string expression_bmp = http_start + @"\S*" + Constant.BMP_extension;
            string expression_tiff = http_start + @"\S*" + Constant.TIFF_extension;

            //Upper Case
            string expression_PNG = http_start + @"\S*" + Constant.PNG_extension.ToUpper();
            string expression_JPG = http_start + @"\S*" + Constant.JPG_extension.ToUpper();
            string expression_GIF = http_start + @"\S*" + Constant.GIF_extension.ToUpper();
            string expression_BMP = http_start + @"\S*" + Constant.BMP_extension.ToUpper();
            string expression_TIFF = http_start + @"\S*" + Constant.TIFF_extension.ToUpper();
            #endregion

            #region https images
            string expressions_png = https_start + @"\S*" + Constant.PNG_extension;
            string expressions_jpg = https_start + @"\S*" + Constant.JPG_extension;
            string expressions_gif = https_start + @"\S*" + Constant.GIF_extension;
            string expressions_bmp = https_start + @"\S*" + Constant.BMP_extension;
            string expressions_tiff = https_start + @"\S*" + Constant.TIFF_extension;

            //Upper Case
            string expressions_PNG = https_start + @"\S*" + Constant.PNG_extension.ToUpper();
            string expressions_JPG = https_start + @"\S*" + Constant.JPG_extension.ToUpper();
            string expressions_GIF = https_start + @"\S*" + Constant.GIF_extension.ToUpper();
            string expressions_BMP = https_start + @"\S*" + Constant.BMP_extension.ToUpper();
            string expressions_TIFF = https_start + @"\S*" + Constant.TIFF_extension.ToUpper();
            #endregion

            #region Links with jpg
            List<string> jpg_links = Links(notes, expression_jpg);
            List<string> jpg_https = Links(notes, expressions_jpg);
            foreach (var link in jpg_https)
            {
                jpg_links.Add(link);
            }
            #region Upper Case
            List<string> JPG_links = Links(notes, expression_JPG);
            foreach (var link in JPG_links)
            {
                jpg_links.Add(link);
            }
            List<string> JPG_https = Links(notes, expressions_JPG);
            foreach (var link in JPG_https)
            {
                jpg_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with png
            List<string> png_links = Links(notes, expression_png);
            List<string> png_https = Links(notes, expressions_png);
            foreach (var link in png_https)
            {
                png_links.Add(link);
            }
            #region Upper Case
            List<string> PNG_links = Links(notes, expression_PNG);
            foreach (var link in PNG_links)
            {
                png_links.Add(link);
            }
            List<string> PNG_https = Links(notes, expressions_PNG);
            foreach (var link in PNG_https)
            {
                png_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with gif
            List<string> gif_links = Links(notes, expression_gif);
            List<string> gif_https = Links(notes, expressions_gif);
            foreach (var link in gif_https)
            {
                gif_links.Add(link);
            }
            #region Upper Case
            List<string> GIF_links = Links(notes, expression_GIF);
            foreach (var link in GIF_links)
            {
                gif_links.Add(link);
            }
            List<string> GIF_https = Links(notes, expressions_GIF);
            foreach (var link in GIF_https)
            {
                gif_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with bmp
            List<string> bmp_links = Links(notes, expression_bmp);
            List<string> bmp_https = Links(notes, expressions_bmp);
            foreach (var link in bmp_https)
            {
                bmp_links.Add(link);
            }
            #region Upper Case
            List<string> BMP_links = Links(notes, expression_BMP);
            foreach (var link in BMP_links)
            {
                bmp_links.Add(link);
            }
            List<string> BMP_https = Links(notes, expressions_BMP);
            foreach (var link in BMP_https)
            {
                bmp_links.Add(link);
            }
            #endregion
            #endregion
            #region Links with tiff
            List<string> tiff_links = Links(notes, expression_tiff);
            List<string> tiff_https = Links(notes, expressions_tiff);
            foreach (var link in tiff_https)
            {
                tiff_links.Add(link);
            }
            #region Upper Case
            List<string> TIFF_links = Links(notes, expression_TIFF);
            foreach (var link in TIFF_links)
            {
                tiff_links.Add(link);
            }
            List<string> TIFF_https = Links(notes, expressions_TIFF);
            foreach (var link in TIFF_https)
            {
                tiff_links.Add(link);
            }
            #endregion
            #endregion

            #region Download JPG Image
            foreach (string _string in jpg_links)
            {
                notes_jpg++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_jpg.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch (Exception ex)
                {
                    ErrorLogTask Logfile = new ErrorLogTask();
                    Logfile.Error_details = ex.ToString();
                    Logfile.Error_title = "NotesImagedownloader Method";
                    Logfile.Location = "NotesTask";
                    ErrorLogTask.LogFileSaveAsync(Logfile);
                    try
                    {
                        await DependencyService.Get<Interfaces.ITask>().ForceImageDownloader(_string, imageName, Constant.JPG_extension);
                    }
                    catch (Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            #endregion
            #region Download PNG Image
            foreach (string _string in png_links)
            {
                notes_png++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_png.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch (Exception ex)
                {
                    ErrorLogTask Logfile = new ErrorLogTask();
                    Logfile.Error_details = ex.ToString();
                    Logfile.Error_title = "NotesImagedownloader Method";
                    Logfile.Location = "NotesTask";
                    ErrorLogTask.LogFileSaveAsync(Logfile);
                    try
                    {
                        await DependencyService.Get<Interfaces.ITask>().ForceImageDownloader(_string, imageName, Constant.PNG_extension);
                    }
                    catch (Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            #endregion
            #region Download GIF Image
            foreach (string _string in gif_links)
            {
                notes_gif++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_gif.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch (Exception ex)
                {
                    ErrorLogTask Logfile = new ErrorLogTask();
                    Logfile.Error_details = ex.ToString();
                    Logfile.Error_title = "NotesImagedownloader Method";
                    Logfile.Location = "NotesTask";
                    ErrorLogTask.LogFileSaveAsync(Logfile);

                    try
                    {
                        await DependencyService.Get<Interfaces.ITask>().ForceImageDownloader(_string, imageName, Constant.GIF_extension);
                    }
                    catch (Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            #endregion
            #region Download BMP Image
            foreach (string _string in bmp_links)
            {
                notes_bmp++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_bmp.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch (Exception ex)
                {
                    ErrorLogTask Logfile = new ErrorLogTask();
                    Logfile.Error_details = ex.ToString();
                    Logfile.Error_title = "NotesImagedownloader Method";
                    Logfile.Location = "NotesTask";
                    ErrorLogTask.LogFileSaveAsync(Logfile);
                    try
                    {
                        await DependencyService.Get<Interfaces.ITask>().ForceImageDownloader(_string, imageName, Constant.BMP_extension);
                    }
                    catch (Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            #endregion
            #region Download TIFF Image
            foreach (string _string in tiff_links)
            {
                notes_tiff++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_tiff.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch (Exception ex)
                {
                    ErrorLogTask Logfile = new ErrorLogTask();
                    Logfile.Error_details = ex.ToString();
                    Logfile.Error_title = "NotesImagedownloader Method";
                    Logfile.Location = "NotesTask";
                    ErrorLogTask.LogFileSaveAsync(Logfile);
                    try
                    {
                        await DependencyService.Get<Interfaces.ITask>().ForceImageDownloader(_string, imageName, Constant.TIFF_extension);
                    }
                    catch (Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            #endregion
        }
        public static void GetNotesImagesSubjectsAsync(List<SubjectModel> subjects)
        {
            GetAssignmentImagesSubjectAsync(subjects);
            GetTopicImagesSubjectAsync(subjects);
        }
        private static void GetTopicImagesSubjectAsync(List<SubjectModel> subjects)
        {
            List<TopicModel> topics = new List<TopicModel>();
            try
            {
                var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
                foreach (var subject in subjects)
                {
                    topics = subject.topics;
                    if (topics != null)
                    {
                        foreach (var topic in topics)
                        {
                            string new_notes = NotesChanger(topic.body);
                            try
                            {
                                NotesImageDownloader(new_notes, subject.name, topic.folder_name);
                            }
                            catch
                            {

                            }
                        }
                    }

                }
            }
            catch
            {

            }
        }
        private static void GetAssignmentImagesSubjectAsync(List<SubjectModel> subjects)
        {
            List<AssignmentModel> assignments = new List<AssignmentModel>();
            try
            {
                var db = DependencyService.Get<Interfaces.IDatabase>().GetConnection();
                foreach (var subject in subjects)
                {
                    assignments = subject.assignments;
                    if (assignments != null)
                    {
                        foreach (var assignment in assignments)
                        {
                            string new_notes = NotesChanger(assignment.description);
                            try
                            {
                                NotesImageDownloader(new_notes, subject.name, assignment.title);
                            }
                            catch
                            {

                            }
                        }
                    }

                }
            }
            catch
            {

            }
        }
        #endregion     
    }
}
