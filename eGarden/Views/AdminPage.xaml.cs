using eGarden.ViewModel;

namespace eGarden.Views;

public partial class AdminPage : ContentPage
{
	private readonly AdminPageViewModel _viewModel;
	public AdminPage()
	{
		InitializeComponent();
		_viewModel = new AdminPageViewModel();
		BindingContext = _viewModel;
		_viewModel.FetchAllRequests();
    }

    private async void OnApproveClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var requestId = button.CommandParameter as int?;
        if (requestId.HasValue)
        {
            string message=await _viewModel.ApproveRequest(requestId.Value);
            DisplayAlert("Info", message, "OK");
        }
        _viewModel.FetchAllRequests();
    }

    private async void OnDenyClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var requestId = button.CommandParameter as int?;
        if (requestId.HasValue)
        {
            string message = await _viewModel.DenyRequest(requestId.Value);
            DisplayAlert("Info", message, "OK");
        }
        _viewModel.FetchAllRequests();

    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}