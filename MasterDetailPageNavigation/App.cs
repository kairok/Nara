using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace MasterDetailPageNavigation
{

   

    public class App : Application
	{
        

        public App ()
		{
			MainPage = new NavigationPage(new MasterDetailPageNavigation.Begin()); //new MasterDetailPageNavigation.Begin ();   //MainPage
        }

		protected override void OnStart ()
		{
           
            Debug.WriteLine("OnStart!-------------------------------");
            //int ok = 0;
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
           // Stop = 0;
            Debug.WriteLine("OnSleep!-------------------------------");
           // int ok = 0;
            // Handle when your app sleeps
        }

		protected override void OnResume ()
		{
            Debug.WriteLine("OnResume!-------------------------------");
           // int ok = 0;
            // Handle when your app resumes
        }
	}
}

