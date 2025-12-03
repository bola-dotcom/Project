//using AndroidX.Core.View;

namespace Project;
[QueryProperty(nameof(MovieProperty), "Movie")]
public partial class MovieDetailPage : ContentPage
{

	private Movie movieProperty;
	public Movie MovieProperty
	{
		get => movieProperty;
		set
		{
			movieProperty = value;
			BindingContext = value;
		}
	}
    public MovieDetailPage()
	{
        InitializeComponent();
    }

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
	
	
}