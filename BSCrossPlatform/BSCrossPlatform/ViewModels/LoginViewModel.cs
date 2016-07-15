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
        public LoginViewModel()
        {
            RegisterLink = Core.Constant.RegisterUri;
            PasswordLink = Core.Constant.PasswordUri;
        }
    }
}
