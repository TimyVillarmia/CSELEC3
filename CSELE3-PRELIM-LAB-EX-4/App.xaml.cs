namespace CSELE3_PRELIM_LAB_EX_4
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AppShell());
        }
    }
}
