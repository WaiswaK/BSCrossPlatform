namespace BSCrossPlatform.ViewModels
{
    class LoginPageViewModel
    {
        private string _registerLink;
        public string RegisterLink
        {
            get { return _registerLink; }
            set { _registerLink = value; }
        }
        private string _passwordLink;
        public string PasswordLink
        {
            get { return _passwordLink; }
            set { _passwordLink = value; }
        }
        public LoginPageViewModel()
        {
            RegisterLink = Common.Constant.RegisterUri;
            PasswordLink = Common.Constant.PasswordUri;
        }
    }
}
