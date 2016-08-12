using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        public string e_mail { get; set; }
        public string password { get; set; }
        public string profileName { get; set; }
        public string subjects { get; set; }
        public int School_id { get; set; }
        public User(string mail, string pass, string profile, string subs, int school) 
        {
            e_mail = mail;
            password = pass;
            profileName = profile;
            subjects = subs;
            School_id = school;
        }
        public User() { }
    }
}
