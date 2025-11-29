namespace Project;

public partial class MusicDetailPage : ContentPage
{
	public MusicDetailPage()
	{
		InitializeComponent();
	}
	private async void Button_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}