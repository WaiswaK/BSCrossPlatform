namespace BSCrossPlatform.Models
{
    public class LoginStatus
    {
        public string statusCode { get; set; }
        public string statusDescription { get; set; }
        public LoginStatus(string status_code, string status_description)
        {
            statusCode = status_code;
            statusDescription = status_description;
        }
        public LoginStatus() { }
    }
}
