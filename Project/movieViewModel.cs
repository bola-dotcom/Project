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
        //it adds it to the collectionView
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        //this stores the selected movie by the user
        private Movie _selectedMovie;
        public Movie selectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                OnPropertyChanged();
            }
        }
        //this stores the name of the user
        private string _Name = "Your namea";
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
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
                //each film in the texts list and call it film
                foreach (var film in texts)
                    // Console.WriteLine(film.title);
                    Movies.Add(new Movie
                    {
                        title = film.title,
                        year = film.year,
                        genre =film.genre,
                        director = film.director,
                        rating = film.rating,
                        emoji = film.emoji
                    });
               
            }
        }


     
    }
}

    

