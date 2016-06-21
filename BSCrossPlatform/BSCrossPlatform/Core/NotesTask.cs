using BSCrossPlatform.Common;
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
        public static async Task<string> Offline_Notes(string notes, string subject, string topic)
        {
            int notes_image = 0;
            string start_string_two = "http://";
            string expression_png = start_string_two + @"\S*" + Constant.PNG_extension;
            string expression_jpg = start_string_two + @"\S*" + Constant.JPG_extension;
            string expression_gif = start_string_two + @"\S*" + Constant.GIF_extension;
            string expression_bmp = start_string_two + @"\S*" + Constant.BMP_extension;
            string expression_tiff = start_string_two + @"\S*" + Constant.TIFF_extension;

            //Upper Case
            string expression_PNG = start_string_two + @"\S*" + Constant.PNG_extension.ToUpper();
            string expression_JPG = start_string_two + @"\S*" + Constant.JPG_extension.ToUpper();
            string expression_GIF = start_string_two + @"\S*" + Constant.GIF_extension.ToUpper();
            string expression_BMP = start_string_two + @"\S*" + Constant.BMP_extension.ToUpper();
            string expression_TIFF = start_string_two + @"\S*" + Constant.TIFF_extension.ToUpper();

            List<string> jpg_links = Links(notes, expression_jpg); //Links with png
            List<string> png_links = Links(notes, expression_png); //Links with jpg
            List<string> gif_links = Links(notes, expression_gif); //Links with gif
            List<string> bmp_links = Links(notes, expression_bmp); //Links with bmp
            List<string> tiff_links = Links(notes, expression_tiff); //Links with tiff

            //Upper Case
            List<string> JPG_links = Links(notes, expression_JPG); //Links with png
            List<string> PNG_links = Links(notes, expression_PNG); //Links with jpg
            List<string> GIF_links = Links(notes, expression_GIF); //Links with gif
            List<string> BMP_links = Links(notes, expression_BMP); //Links with bmp
            List<string> TIFF_links = Links(notes, expression_TIFF); //Links with tiff

            string new_notes = notes;

            foreach (string _string in JPG_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/jpg;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.JPG_extension);
                new_notes = new_notes.Replace(_string, imageName);
                //notes = new_notes; //Carry the new value in notes
            }
            foreach (string _string in PNG_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/png;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.PNG_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in GIF_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/gif;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.GIF_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in BMP_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/bmp;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.BMP_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in TIFF_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/tiff;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.TIFF_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in jpg_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/jpg;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.JPG_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in png_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/png;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.PNG_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in gif_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/gif;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.GIF_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in bmp_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/bmp;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.BMP_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            foreach (string _string in tiff_links)
            {
                notes_image++;
                string _generatedName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                string imageName = "data:image/tiff;base64, " + await DependencyService.Get<Interfaces.ITask>().LocalBase64(_generatedName, Constant.TIFF_extension);
                new_notes = new_notes.Replace(_string, imageName);
            }
            return new_notes;
        }
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
            catch(Exception ex)
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
        //Method to change notes
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
        //Notes image download methods
        public static async void NotesImageDownloader(string notes, string subject, string topic)
        {
            int notes_image = 0;
            string start_string = "http://";
            string expression_png = start_string + @"\S*" + Constant.PNG_extension;
            string expression_jpg = start_string + @"\S*" + Constant.JPG_extension;
            string expression_gif = start_string + @"\S*" + Constant.GIF_extension;
            string expression_bmp = start_string + @"\S*" + Constant.BMP_extension;
            string expression_tiff = start_string + @"\S*" + Constant.TIFF_extension;

            //Upper Case
            string expression_PNG = start_string + @"\S*" + Constant.PNG_extension.ToUpper();
            string expression_JPG = start_string + @"\S*" + Constant.JPG_extension.ToUpper();
            string expression_GIF = start_string + @"\S*" + Constant.GIF_extension.ToUpper();
            string expression_BMP = start_string + @"\S*" + Constant.BMP_extension.ToUpper();
            string expression_TIFF = start_string + @"\S*" + Constant.TIFF_extension.ToUpper();

            List<string> jpg_links = Links(notes, expression_jpg); //Links with jpg
            List<string> png_links = Links(notes, expression_png); //Links with png
            List<string> gif_links = Links(notes, expression_gif); //Links with gif
            List<string> bmp_links = Links(notes, expression_bmp); //Links with bmp
            List<string> tiff_links = Links(notes, expression_tiff); //Links with tiff

            //Upper Case
            List<string> JPG_links = Links(notes, expression_JPG); //Links with jpg
            List<string> PNG_links = Links(notes, expression_PNG); //Links with png
            List<string> GIF_links = Links(notes, expression_GIF); //Links with gif
            List<string> BMP_links = Links(notes, expression_BMP); //Links with bmp
            List<string> TIFF_links = Links(notes, expression_TIFF); //Links with tiff

            foreach (string _string in jpg_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with png 
            foreach (string _string in png_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with gif 
            foreach (string _string in gif_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with bmp 
            foreach (string _string in bmp_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with tiff 
            foreach (string _string in tiff_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Upper Cases
            //Search for jpg
            foreach (string _string in JPG_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with png 
            foreach (string _string in PNG_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with gif 
            foreach (string _string in GIF_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with bmp 
            foreach (string _string in BMP_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
            //Search for links with Second Regular Expression with tiff 
            foreach (string _string in TIFF_links)
            {
                notes_image++;
                string imageName = subject + "-" + topic + "_" + "notes_image" + notes_image.ToString();
                try
                {
                    await DependencyService.Get<Interfaces.ITask>().ImageDownloader(_string, imageName);
                }
                catch(Exception ex)
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
                    catch(Exception exc)
                    {
                        Logfile.Error_details = exc.ToString();
                        Logfile.Error_title = "NotesImagedownloader Method";
                        Logfile.Location = "NotesTask";
                        ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }

        }
        public static void GetNotesImagesSubjectsAsync(List<SubjectModel> subjects)
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
                                Topic newTopic = new Topic(topic.TopicID, subject.Id, topic.TopicTitle, topic.body,
                                    new_notes, topic.Updated_at, topic.teacher, topic.folder_id, topic.folder_name);
                                try
                                {
                                    db.Update(newTopic);
                                }
                                catch
                                {

                                }
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
    }
}
