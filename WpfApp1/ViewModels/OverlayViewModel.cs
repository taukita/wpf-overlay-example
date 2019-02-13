namespace WpfApp1.ViewModels
{
	using System.Collections.ObjectModel;
	using System.Windows.Input;
	using Prism.Commands;
	using Prism.Mvvm;

	internal class OverlayViewModel : BindableBase
	{
		private bool _isShown;

		public OverlayViewModel()
			: this(0)
		{
			IsShown = true;
		}

		public OverlayViewModel(int n)
		{
			Items.Add(new OverlayItemViewModel("Первый элемент"));
			Items.Add(new OverlayItemViewModel("Второй элемент"));
			Items.Add(new OverlayItemViewModel("Третий элемент"));
			for (var i = 0; i < n; i++)
			{
				Items.Add(new OverlayItemViewModel($"Элемент №{i + 3}"));
			}

			Hide = new DelegateCommand(() => IsShown = false);
			Toggle = new DelegateCommand(() => IsShown = !IsShown);
		}

		public ICommand Hide { get; }

		public bool IsShown
		{
			get => _isShown;
			set => SetProperty(ref _isShown, value);
		}

		public ObservableCollection<OverlayItemViewModel> Items { get; } =
			new ObservableCollection<OverlayItemViewModel>();

		public ICommand Toggle { get; }
	}
}