//using Android.Graphics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project
{

    public partial class MainPage : ContentPage
    {
        int count = 0;

        private HttpClient _httpClient;
        private movieViewModel viewModel;


        public MainPage()
        {
            InitializeComponent();

            viewModel = new movieViewModel();
            BindingContext = viewModel;
            _httpClient = new HttpClient();
            if (count == 0)
            {
                viewModel.downloadFile();
            }

        }
        private async Task selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Movie selectedMovie) {
                var papameters = new Dictionary<string, object>
            {
                {"Movie" , selectedMovie}
        };
                await Shell.Current.GoToAsync(nameof(MovieDetailPage), parameters);
                ((CollectionView)sender).SelectedItem = null;
            }

        }
    }
}


