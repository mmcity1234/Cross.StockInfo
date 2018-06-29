using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Cross.StockInfo
{
	public partial class App : Application
	{
		public App ()
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc0NUAzMTM2MmUzMjJlMzBVUGFIbFVKeWx5TTJkUnlCOGtwSi9GN1d3YndMa21PTlZIQi9kU0VyeURNPQ==");

            BootApplication.Initialize();

            InitializeComponent();

            MainPage = new MainPage();
        }

        
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
