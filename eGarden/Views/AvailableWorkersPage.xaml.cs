using eGarden.ViewModel;

namespace eGarden.Views;

public partial class AvailableWorkersPage : ContentPage
{
	private readonly AllWorkersViewModel _viewModel;
	public AvailableWorkersPage()
	{
		InitializeComponent();
		_viewModel = new AllWorkersViewModel();
		BindingContext = _viewModel;
		_viewModel.FetchAllWorkers();
	}
}