namespace eGarden.Views;

public partial class UserPage : ContentPage
{
	private string _username;
	private bool _hasCard;
	public UserPage(string username, bool hasCard)
	{
        InitializeComponent();
        _username = username;
		_hasCard = hasCard;
		BindingContext=this;
	}
	public string Username
	{
		get { return _username; }
		set { _username = value; }
	}

    private async void OnProfileClicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new UserProfile(_username));
    }

    private async void OnRegisterSubscriptionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddSubscriptionPage(_username));
    }

    private async void OnCreateRequestClicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new CreateRequestPage(_username, _hasCard));
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnViewWorkersClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AvailableWorkersPage());
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var selectedOption = button.CommandParameter;
        string option = selectedOption.ToString();
        await Navigation.PushAsync(new OfferDetailsPage(option));
    }
}