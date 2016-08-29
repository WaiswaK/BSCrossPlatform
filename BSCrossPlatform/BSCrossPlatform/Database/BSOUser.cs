using SQLite.Net.Attributes;

namespace BSCrossPlatform.Database
{
    [Table("BSOUser")]
    public class BSOUser
    {
        [PrimaryKey]
        public string e_mail { get; set; }
        public string password { get; set; }
        public string profileName { get; set; }
        public BSOUser(string mail, string pass, string profile, string subs, int school)
        {
            e_mail = mail;
            password = pass;
            profileName = profile;
        }
        public BSOUser() { }
    }
}
