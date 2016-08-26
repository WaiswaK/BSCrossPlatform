
using Xamarin.Forms;

namespace BSCrossPlatform
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new Views.WelcomeView());
            //MainPage = new NavigationPage(new Views.LoginView());
            //MainPage = new Views.LoginView();          
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
