using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BJtest.Common.Managers.UserManager;
using BJtest.Commons.Helpers;
using BJtest.DB;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BJtest
{
    public partial class App : Application
    {
        private readonly IUserManager _userManager = DependencyService.Get<IUserManager>();
        private static LocalDB _database;
        public static JsonSerializerSettings JsonSettings;

        public static LocalDB Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new LocalDB(Constants.DatabasePath);
                }
                return _database;
            }
        }

        public App()
        {
            Device.SetFlags(new string[] { "CarouselView_Experimental" });

            InitializeComponent();

            JsonSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            JsonConvert.DefaultSettings = () => JsonSettings;

            MessagingCenter.Subscribe<string>(this, Constants.START_FINISH_NETWORK_REQUEST, (value) =>
            {
                if (value == Constants.START)
                    UserDialogs.Instance.ShowLoading("Loading...");
                else
                    UserDialogs.Instance.HideLoading();
            });

            MessagingCenter.Subscribe<string>(this, Constants.ERROR_OCCURED, async (value) =>
            {
                await UserDialogs.Instance.AlertAsync(value, "Error", "Ok");
            });


            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Orange,
                BarTextColor = Color.White
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
