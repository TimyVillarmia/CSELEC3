using CSELE3_PRELIM_LAB_EX_4.View;

namespace CSELE3_PRELIM_LAB_EX_4
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();


           
        }

        void RegisterRoutes()
        {
            Routes.Add("NavigationPage", typeof(NavigationPage));
            Routes.Add("MainPage", typeof(MainPage));
            Routes.Add("FoldersPage", typeof(FoldersPage));
            Routes.Add("AddTaskPage", typeof(AddTaskPage));
            Routes.Add("MessagesPage", typeof(MessagesPage));
            Routes.Add("ProfilePage", typeof(ProfilePage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            //Cancel any back navigation
            if (e.Source == ShellNavigationSource.Pop)
            {
                e.Cancel();
            }
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            // Perform required logic
        }
    }
}
