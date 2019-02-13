namespace WpfApp1.ViewModels
{
	using Prism.Mvvm;

	internal class MainViewModel : BindableBase
	{
		public MainViewModel()
		{
			Overlay = new OverlayViewModel(20);
		}

		public OverlayViewModel Overlay { get; }
	}
}