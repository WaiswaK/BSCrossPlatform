﻿namespace BSCrossPlatform.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new BSCrossPlatform.App());
        }
    }
}
