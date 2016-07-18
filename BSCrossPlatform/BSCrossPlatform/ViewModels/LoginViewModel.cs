namespace BSCrossPlatform.ViewModels
{
    class LoginViewModel
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
        private string _createlink;
        public string CreateLink
        {
            get { return _createlink; }
            set { _createlink = value; }
        }
        public LoginViewModel()
        {
            RegisterLink = Core.Constant.RegisterUri;
            PasswordLink = Core.Constant.PasswordUri;
            CreateLink = Core.Constant.FullBaseUri;
        }
    }
}
