//using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project
{
    public class movieViewModel : INotifyPropertyChanged
    {
        //this creates a httpClient object
        private HttpClient _httpClient = new HttpClient();
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //this is the list of movies
        private List<Movie> allMovies = new List<Movie>();
        //it adds it to the collectionView
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        private string searchMovie;
        public string SearchMovie
        {
            get => searchMovie;
            set
            {
                searchMovie = value;
                OnPropertyChanged(nameof(SearchMovie));
                FilterMovie();
            }
        }

        public Command<string> SearchCommand { get; set; }

        public movieViewModel()
        {
            SearchCommand = new Command<string>((text) =>
        {
            SearchMovie = text;
        });
        }
        //this stores the selected movie by the user
        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }
        //this stores the name of the user
        private string _Name = "Your name";
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
                FilterMovie();
            }
        }
        public movieViewModel()
        {
            SearchCommand = new Command<string>((text) =>
            {
                SearchMovie = text;
            });
        }
        public void LoadMovies(List<Movie> movieList)
        {
            allMovies = movieList;
            Movies.Clear();
            foreach (var movie in allMovies)
                Movies.Add(movie);
        }


        public async void downloadFile()
        {
            //if this is the first time the application is opened

            //gets the link into the response variable
            var response = await _httpClient.GetAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json");



            if (response == null)
            {

            }
            //if the response is successful
            if (response.IsSuccessStatusCode)
            {
                //reads the content of the json into the text variable
                String text = await response.Content.ReadAsStringAsync();
                //deserializes the content into the texts variable
                var texts = JsonSerializer.Deserialize<List<Movie>>(text);
                Movies.Clear();
                EnterMovies(texts);
                //each film in the texts list and call it film
                foreach (var film in texts)
                    // Console.WriteLine(film.title);
                    Movies.Add(new Movie
                    {
                        title = film.title,
                        year = film.year,
                        genre = film.genre,
                        director = film.director,
                        rating = film.rating,
                        emoji = film.emoji
                    });

            }
        }
        private void FilterMovies()
        {
            if(string.IsNullOrWhiteSpace(SearchText))
            {
                Movies.Clear();
                foreach (var movie in allMovies)
                    Movies.Add(movie);

                return;
            }
            var filtered = allMovies.Where(m => 
            m.title.Contains(SearchMovie, StringComparison.OrdinalIgnoreCase) ||
            m.year.ToString().Contains(SearchMovie)  ||
            m.director.Contains(SearchMovie, StringComparison.OrdinalIgnoreCase) ||
            m.genre.Any(g=> g.Contains(SearchMovie, StringComparison.OrdinalIgnoreCase))
            ).ToList();
            Movies.Clear();
            foreach (var movie in filtered)
                Movies.Add(movie);
        }

        public void EnterMovies(List<Movie> movieList)
        {
            allMovies = movieList;
            Movies.Clear();
            foreach (var movie in allMovies)
                Movies.Add(movie);
        }
        private void FilterMovie()
        {

            if (string.IsNullOrWhiteSpace(SearchMovie))
            {
                Movies.Clear();
                foreach (var movie in allMovies)
                    Movies.Add(movie);
                return;
            }
            var filtered = allMovies
 .Where(m =>
m.title.Contains(SearchMovie, StringComparison.OrdinalIgnoreCase) ||
             (m.genre != null && m.genre.Any(g => g.Contains(SearchMovie, StringComparison.OrdinalIgnoreCase))) ||
               m.year.ToString().Contains(SearchMovie)
               )
               . ToList();

            Movies.Clear();
            foreach (var movie in filtered)
                Movies.Add(movie);

        }
    }

     
    }


    

