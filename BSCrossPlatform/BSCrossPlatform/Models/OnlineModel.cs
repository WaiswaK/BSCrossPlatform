using System.Collections.Generic;

namespace BSCrossPlatform.Models
{
    public class OnlineModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public List<PastPaperModel> PastPapers { get; set; }
        public List<QuizModel> Quiz { get; set; }
        public List<ExamModel> Exams { get; set; }
    }
}
