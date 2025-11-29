namespace Project
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //This to register the rout to MovieDetailPage to the appshell
            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
        }
    }
}
