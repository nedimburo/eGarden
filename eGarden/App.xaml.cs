
namespace eGarden
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Constants.ApiBaseUrl = "http://192:8081/api"; // Configure IP Address example: http://192.168.0.1:8081/api

            MainPage = new AppShell();
        }
    }
}
