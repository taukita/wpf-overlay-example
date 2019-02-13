namespace WpfApp1.ViewModels
{
	using System.Windows.Input;
	using Prism.Commands;
	using Prism.Mvvm;

	internal class MainViewModel : BindableBase
	{
		private bool _isOverlayShown;

		public MainViewModel()
		{
			ToggleOverlay = new DelegateCommand(ExecuteToggleOverlay);
			Overlay = new OverlayViewModel(() => IsOverlayShown = false, 20);
		}

		public bool IsOverlayShown
		{
			get => _isOverlayShown;
			set => SetProperty(ref _isOverlayShown, value);
		}

		public ICommand ToggleOverlay { get; }

		public OverlayViewModel Overlay { get; }

		private void ExecuteToggleOverlay()
		{
			IsOverlayShown = !IsOverlayShown;
		}
	}
}