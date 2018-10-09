using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common.Localization;
using System;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Cross.StockInfo
{
	public partial class App : Application
	{
		public App ()
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzI2MzFAMzEzNjJlMzMyZTMwZkVVaVozZ1ZYZVNkMkYzREFMUVdIRlNRa3d6ekZBbjJHcDlZOUZDanBnTT0=");

            BootApplication.Initialize();

            // 多語系初始設定
            InitializeLocalization();

            InitializeComponent();            
        
            MainPage = new NavigationPage(new MainPage());
        }

        private void InitializeLocalization()
        {
            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                // determine the correct, supported .NET culture
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            // Enable CJK encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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
