using eGarden.ViewModel;

namespace eGarden.Views;

public partial class UsersRequestsPage : ContentPage
{
	private readonly UsersRequestPageViewModel _viewModel;
	public UsersRequestsPage(string username)
	{
		InitializeComponent();
		_viewModel=new UsersRequestPageViewModel();
		BindingContext = _viewModel;
		_viewModel.FetchAllUsersRequests(username);
	}
}