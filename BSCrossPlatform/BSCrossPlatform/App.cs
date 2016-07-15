
using Xamarin.Forms;

namespace BSCrossPlatform
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            /*MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    /*VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            XAlign = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    } 
                }
            };*/
            //MainPage = new Views.LoginView();
            Models.UserModel user = new Models.UserModel();
            user.email = "wswkenneth7@gmail.com";
            user.full_names = "Waiswa Kenneth";
            user.Library = new Models.LibraryModel();
            user.subjects = new System.Collections.Generic.List<Models.SubjectModel>();
            user.School = new Models.SchoolModel("", "", 1);


           // MainPage = new Views.LoginView();
            //MainPage = new Views.SubjectsView(user);
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
