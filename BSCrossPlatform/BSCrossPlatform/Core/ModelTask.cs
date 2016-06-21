using BSCrossPlatform.Database;
using BSCrossPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSCrossPlatform.Core
{
    class ModelTask
    {
        #region Subjects Methods
        public static List<string> oldSubjects()
        {
            List<Subject> subjects = DBRetrievalTask.SelectAllSubjects();
            List<string> subs = new List<string>();
            List<string> temp = null;
            if (subjects == null)
            {
                return temp;
            }
            else
            {
                foreach (Subject subject in subjects)
                {
                    string sub = subject.name;
                    subs.Add(sub);
                }
                return subs;
            }
        }
        public static List<SubjectModel> new_subjects(List<SubjectModel> Gotsubjects)
        {
            List<string> oldsubs = oldSubjects();
            List<string> subjects = new List<string>();
            List<string> newsubs = new List<string>();
            int x = 0;
            int y = 0;
            List<SubjectModel> final = new List<SubjectModel>();
            foreach (var subject in Gotsubjects)
            {
                string sub = subject.name;
                subjects.Add(sub);
            }
            foreach (string newsubject in subjects)
            {
                x++;
                foreach (string oldsubject in oldsubs)
                {
                    string sub;
                    if (oldsubject.Equals(newsubject))
                    {
                        y++;
                    }
                    else
                    {
                        sub = newsubject;
                        newsubs.Add(sub);
                    }
                }
            }
            if (x == y)
            {
                return null;
            }
            else
            {
                foreach (var sub in Gotsubjects)
                {
                    foreach (var substring in newsubs)
                    {
                        if (sub.name.Equals(substring))
                        {
                            final.Add(sub);
                        }
                    }
                }
                return final;
            }
        }
        public static List<SubjectModel> UpdateableSubjects(List<SubjectModel> oldSubjects, List<SubjectModel> newSubjects)
        {
            List<SubjectModel> final = new List<SubjectModel>();
            List<TopicModel> TopicsTemp = new List<TopicModel>();
            List<AssignmentModel> AssignmentsTemp = new List<AssignmentModel>();
            List<VideoModel> VideosTemp = new List<VideoModel>();
            List<AttachmentModel> FileTemp = new List<AttachmentModel>();
            SubjectModel Temp = new SubjectModel();
            bool got = false;
            foreach (var oldsubject in oldSubjects)
            {
                foreach (var newsubject in newSubjects)
                {
                    if (oldsubject.Id == newsubject.Id)
                    {
                        TopicsTemp = UpdateableTopics(oldsubject.topics, newsubject.topics);
                        AssignmentsTemp = UpdateableAssignments(oldsubject.assignments, newsubject.assignments);
                        VideosTemp = GetNewVideos(newsubject.videos, oldsubject.videos);
                        FileTemp = GetNewFiles(newsubject.files, oldsubject.files);

                        if (TopicsTemp == null && AssignmentsTemp == null && VideosTemp == null && FileTemp == null)
                        {
                        }
                        else
                        {
                            Temp = new SubjectModel(oldsubject.Id, newsubject.name, oldsubject.thumb, TopicsTemp, AssignmentsTemp, VideosTemp, FileTemp);
                            final.Add(Temp);
                            got = true;
                        }
                    }
                }
            }

            if (got == false)
            {
                return null;
            }
            else
            {
                return final;
            }
        }
        public static List<SubjectModel> UpdateableSubjectsTopics(List<SubjectModel> oldSubjects, List<SubjectModel> newSubjects)
        {
            List<SubjectModel> final = new List<SubjectModel>();
            SubjectModel Temp = new SubjectModel();
            bool got = false;
            foreach (var oldsubject in oldSubjects)
            {
                foreach (var newsubject in newSubjects)
                {
                    if (oldsubject.Id == newsubject.Id)
                    {
                        var newtopics = GetNewTopics(newsubject.topics, oldsubject.topics);
                        if (newtopics != null)
                        {
                            Temp = new SubjectModel(oldsubject.Id, newsubject.name, oldsubject.thumb, newtopics, null, null, null);
                            final.Add(Temp);
                            got = true;
                        }
                    }
                }
            }

            if (got == false)
            {
                return null;
            }
            else
            {
                return final;
            }
        }
        public static List<int> newIds(List<int> oldIds, List<int> GotIDs)
        {
            List<int> final = new List<int>();
            int temp = new int();
            bool found = false;
            bool something = false;
            foreach (var nId in GotIDs)
            {
                foreach (var oId in oldIds)
                {
                    if (nId == oId)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = nId;
                    }
                }
                if (found == false)
                {
                    final.Add(temp);
                    something = true;
                }
                found = false;
            }
            if (something == false)
            {
                final = null;
            }
            return final;
        }
        public static List<int> oldIds(List<int> oldIds, List<int> GotIDs)
        {
            List<int> final = new List<int>();
            int temp = new int();
            bool found = false;
            bool something = false;
            foreach (var nId in GotIDs)
            {
                foreach (var oId in oldIds)
                {
                    if (nId == oId)
                    {
                        temp = nId;
                        found = true;
                    }
                    else
                    {
                        ;
                    }
                }
                if (found == true)
                {
                    final.Add(temp);
                    something = true;
                }
                found = false;
            }
            if (something == false)
            {
                final = null;
            }
            return final;
        }
        public static List<int> SubjectIdsConvert(List<string> subjectIDString)
        {
            List<int> numbers = new List<int>();
            foreach (var id in subjectIDString)
            {
                int number = Int32.Parse(id);
                numbers.Add(number);
            }
            return numbers;
        }
        public static List<string> SubjectNames(List<SubjectModel> subjects)
        {
            List<string> subjectnames = new List<string>();
            string sub;
            foreach (var subject in subjects)
            {
                sub = subject.Id.ToString();
                subjectnames.Add(sub);
            }
            return subjectnames;
        }
        public static string JoinedSubjects(List<string> subjects)
        {
            string joined = string.Empty;
            foreach (var subject in subjects)
            {
                if (joined.Equals(string.Empty))
                {
                    joined = subject;
                }
                else
                {
                    joined = joined + "." + subject;
                }
            }
            return joined;
        }
        public static string UserSubjects(string string_input)
        {
            char[] delimiter = { '.' };
            string final = string.Empty;
            List<int> subjectids = new List<int>();
            string[] SplitSubjectId = string_input.Split(delimiter);
            List<string> SubjectIdList = SplitSubjectId.ToList();
            subjectids = SubjectIdsConvert(SubjectIdList);
            List<int> finalids = RemoveRepitions(subjectids);
            foreach (var id in finalids)
            {
                if (final.Equals(string.Empty))
                {
                    final = string.Empty + id;
                }
                else
                {
                    final = final + "." + id;
                }
            };
            return final;
        }
        private static List<int> RemoveRepitions(List<int> numbers)
        {
            List<int> final = new List<int>();
            List<int> compare = numbers;

            bool done = false;
            foreach (var number in numbers)
            {
                foreach (var second in compare)
                {
                    if (number == second)
                    {
                        if (final.Count > 0)
                        {
                            foreach (var test in final)
                            {
                                if (test == second)
                                {
                                    done = true;
                                }
                            }
                            if (done == false)
                            {
                                final.Add(second);
                            }
                        }
                        else
                        {
                            final.Add(number);
                        }
                        done = false;
                    }
                }
            }
            return final;
        }
        #endregion
        #region Topics Methods
        private static TopicModel TopicChange(TopicModel newTopic, TopicModel oldTopic)
        {
            string newNotes = newTopic.body;
            string oldNotes = oldTopic.body;
            List<AttachmentModel> newFiles = newTopic.Files;
            List<AttachmentModel> oldFiles = oldTopic.Files;
            List<AttachmentModel> Files = new List<AttachmentModel>();
            TopicModel Topic = new TopicModel();
            if (newNotes.Equals(oldNotes))
            {
                Files = GetNewFiles(newFiles, oldFiles);
                if (Files == null)
                {
                    Topic = null;
                }
                else
                {
                    Topic = new TopicModel(oldTopic.TopicID, null, null, oldTopic.TopicTitle, Files, oldTopic.teacher, newTopic.Updated_at, oldTopic.folder_id, oldTopic.folder_name);
                }
            }
            else
            {
                Files = GetNewFiles(newFiles, oldFiles);
                if (Files == null)
                {
                    Topic = new TopicModel(oldTopic.TopicID, newTopic.body, NotesTask.NotesChanger(newTopic.body), oldTopic.TopicTitle,
                        null, oldTopic.teacher, newTopic.Updated_at, oldTopic.folder_id, oldTopic.folder_name);
                }
                else
                {
                    Topic = new TopicModel(oldTopic.TopicID, newTopic.body, NotesTask.NotesChanger(newTopic.body),
                        oldTopic.TopicTitle, Files, oldTopic.teacher, newTopic.Updated_at, oldTopic.folder_id, oldTopic.folder_name);
                }
            }
            return Topic;
        }
        private static List<TopicModel> UpdateableTopics(List<TopicModel> oldTopics, List<TopicModel> newTopics)
        {
            List<TopicModel> final = new List<TopicModel>();
            List<TopicModel> ntopics = new List<TopicModel>();
            List<TopicModel> otopics = new List<TopicModel>();
            TopicModel temp = new TopicModel();
            bool found = false;
            foreach (var oldtopic in oldTopics)
            {
                foreach (var newtopic in newTopics)
                {
                    if (oldtopic.TopicID == newtopic.TopicID)
                    {
                        temp = TopicChange(newtopic, oldtopic);
                        if (temp == null) { }
                        else
                        {
                            final.Add(newtopic);
                            found = true;
                        }
                    }
                }
            }
            if (found == true)
            {
                return final;
            }
            else
            {
                return null;
            }
        }
        private static List<AssignmentModel> UpdateableAssignments(List<AssignmentModel> oldAssignments, List<AssignmentModel> newAssignments)
        {
            List<AssignmentModel> final = new List<AssignmentModel>();
            List<AssignmentModel> nassignments = new List<AssignmentModel>();
            List<AssignmentModel> oassignments = new List<AssignmentModel>();
            AssignmentModel temp = new AssignmentModel();
            bool found = false;
            foreach (var oldassignment in oldAssignments)
            {
                foreach (var newassignment in newAssignments)
                {
                    if (oldassignment.id == newassignment.id)
                    {
                        temp = AssignmentChange(newassignment, oldassignment);
                        if (temp == null) { }
                        else
                        {
                            final.Add(newassignment);
                            found = true;
                        }
                    }
                }
            }
            if (found == true)
            {
                return final;
            }
            else
            {
                return null;
            }
        }
        private static AssignmentModel AssignmentChange(AssignmentModel newAssignment, AssignmentModel oldAssignment)
        {
            string newNotes = newAssignment.description;
            string oldNotes = oldAssignment.description;
            List<AttachmentModel> newFiles = newAssignment.Files;
            List<AttachmentModel> oldFiles = oldAssignment.Files;
            List<AttachmentModel> Files = new List<AttachmentModel>();
            AssignmentModel Assignment = new AssignmentModel();
            if (newNotes.Equals(oldNotes))
            {
                Files = GetNewFiles(newFiles, oldFiles);
                if (Files == null)
                {
                    Assignment = null;
                }
                else
                {
                    Assignment = new AssignmentModel(oldAssignment.id, oldAssignment.title, null, oldAssignment.teacher, Files);
                }
            }
            else
            {
                Files = GetNewFiles(newFiles, oldFiles);
                if (Files == null)
                {
                    Assignment = new AssignmentModel(oldAssignment.id, oldAssignment.title, newAssignment.description, oldAssignment.teacher, null);
                }
                else
                {
                    Assignment = new AssignmentModel(oldAssignment.id, oldAssignment.title, newAssignment.description, oldAssignment.teacher, Files);
                }
            }
            return Assignment;
        }
        private static List<VideoModel> GetNewVideos(List<VideoModel> newVideos, List<VideoModel> oldVideos)
        {
            List<VideoModel> files = new List<VideoModel>();
            VideoModel temp = new VideoModel();
            bool found = false;
            bool something = false;

            foreach (var nvideo in newVideos)
            {
                foreach (var ovideo in oldVideos)
                {

                    if (ovideo.VideoID == nvideo.VideoID)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = nvideo;
                    }
                }

                if (found == false)
                {
                    files.Add(temp);
                    something = true;
                }
                found = false;
            }
            if (something == false)
            {
                files = null;
            }
            return files;
        }
        private static List<TopicModel> GetNewTopics(List<TopicModel> newTopics, List<TopicModel> oldTopics)
        {
            List<TopicModel> files = new List<TopicModel>();
            TopicModel temp = new TopicModel();
            bool found = false;
            bool something = false;

            foreach (var ntopic in newTopics)
            {
                foreach (var otopic in oldTopics)
                {

                    if (otopic.TopicID == ntopic.TopicID)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = ntopic;
                    }
                }

                if (found == false)
                {
                    files.Add(temp);
                    something = true;
                }
                found = false;
            }
            if (something == false)
            {
                files = null;
            }
            return files;
        }
        public static List<AttachmentModel> GetNewFiles(List<AttachmentModel> newFiles, List<AttachmentModel> oldFiles)
        {
            List<AttachmentModel> files = new List<AttachmentModel>();
            AttachmentModel temp = new AttachmentModel();
            bool found = false;
            bool something = false;

            foreach (var nfile in newFiles)
            {
                foreach (var ofile in oldFiles)
                {

                    if (nfile.AttachmentID == ofile.AttachmentID)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = nfile;
                    }
                }

                if (found == false)
                {
                    files.Add(temp);
                    something = true;
                }
                found = false;
            }
            if (something == false)
            {
                files = null;
            }
            return files;
        }
        #endregion        
        #region Library Methods
        public static LibCategoryModel Category_Update(List<BookModel> oldbooks, List<BookModel> newbooks)
        {
            LibCategoryModel category = new LibCategoryModel();
            List<BookModel> books = new List<BookModel>();
            bool found = false;
            bool something = false;
            BookModel temp = new BookModel();
            int x = 0;
            foreach (var newbook in newbooks)
            {
                foreach (var oldbook in oldbooks)
                {
                    if (oldbook.book_id == newbook.book_id)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = newbook;
                    }
                }
                if (found == false)
                {
                    books.Add(temp);
                    something = true;
                    category.category_id = temp.Category_id;
                    category.category_name = temp.Category_name;
                    x++;
                }
                found = false;
            }
            if (something == false)
            {
                category = null;
            }
            else
            {
                category.category_books = books;
                category.book_count = x;
            }
            return category;
        }
        public static List<LibCategoryModel> Categories_Update(List<LibCategoryModel> oldcategories, List<LibCategoryModel> newcategories)
        {
            List<LibCategoryModel> final = new List<LibCategoryModel>();
            foreach (var newcategory in newcategories)
            {
                foreach (var oldcategory in oldcategories)
                {
                    if (newcategory.category_id == oldcategory.category_id)
                    {
                        LibCategoryModel category = Category_Update(oldcategory.category_books, newcategory.category_books);
                        if (category != null)
                        {
                            final.Add(category);
                        }
                    }
                }
            }
            return final;
        }
        public static LibraryModel CompareLibraries(LibraryModel oldlib, LibraryModel newlib)
        {
            bool found = false;
            bool something = false;
            LibCategoryModel temp = new LibCategoryModel();
            List<LibCategoryModel> final_categories = new List<LibCategoryModel>();
            LibraryModel lib = new LibraryModel();
            lib.library_id = oldlib.library_id;
            List<LibCategoryModel> new_libCategories = new List<LibCategoryModel>();
            new_libCategories = newlib.categories;

            List<LibCategoryModel> old_libCategories = new List<LibCategoryModel>();
            old_libCategories = oldlib.categories;

            foreach (var new_libCategory in new_libCategories)
            {
                foreach (var old_libCategory in old_libCategories)
                {
                    if (old_libCategory.category_id == new_libCategory.category_id)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = new_libCategory;
                    }
                }
                if (found == false)
                {
                    final_categories.Add(temp);
                }
                found = false;
            }
            if (something == false)
            {
                lib = null;
            }
            else
            {
                lib.categories = final_categories;
            }
            return lib;
        }
        public static LibCategoryModel Category_Update_Removal(List<BookModel> oldbooks, List<BookModel> newbooks)
        {
            LibCategoryModel category = new LibCategoryModel();
            List<BookModel> books = new List<BookModel>();
            bool found = false;
            bool something = false;
            BookModel temp = new BookModel();
            int x = 0;
            foreach (var oldbook in oldbooks)
            {
                foreach (var newbook in newbooks)
                {
                    if (oldbook.book_id == newbook.book_id)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = oldbook;
                    }
                }
                if (found == false)
                {
                    books.Add(temp);
                    something = true;
                    category.category_id = temp.Category_id;
                    category.category_name = temp.Category_name;
                    x++;
                }
                found = false;
            }
            if (something == false)
            {
                category = null;
            }
            else
            {
                category.category_books = books;
                category.book_count = x;
            }
            return category;
        }
        public static List<LibCategoryModel> Categories_Update_Removal(List<LibCategoryModel> oldcategories, List<LibCategoryModel> newcategories)
        {
            List<LibCategoryModel> final = new List<LibCategoryModel>();
            foreach (var newcategory in newcategories)
            {
                foreach (var oldcategory in oldcategories)
                {
                    if (newcategory.category_id == oldcategory.category_id)
                    {
                        LibCategoryModel category = Category_Update_Removal(oldcategory.category_books, newcategory.category_books);
                        if (category != null)
                        {
                            final.Add(category);
                        }
                    }
                }
            }
            return final;
        }
        public static LibraryModel CompareLibraries_Removal(LibraryModel oldlib, LibraryModel newlib)
        {
            bool found = false;
            bool something = false;
            LibCategoryModel temp = new LibCategoryModel();
            List<LibCategoryModel> final_categories = new List<LibCategoryModel>();
            LibraryModel lib = new LibraryModel();
            lib.library_id = oldlib.library_id;
            List<LibCategoryModel> new_libCategories = new List<LibCategoryModel>();
            new_libCategories = newlib.categories;

            List<LibCategoryModel> old_libCategories = new List<LibCategoryModel>();
            old_libCategories = oldlib.categories;

            foreach (var new_libCategory in new_libCategories)
            {
                foreach (var old_libCategory in old_libCategories)
                {
                    if (old_libCategory.category_id == new_libCategory.category_id)
                    {
                        found = true;
                    }
                    else
                    {
                        temp = new_libCategory;
                    }
                }
                if (found == false)
                {
                    final_categories.Add(temp);
                }
                found = false;
            }
            if (something == false)
            {
                lib = null;
            }
            else
            {
                lib.categories = final_categories;
            }
            return lib;
        }
        #endregion      
    }
}
