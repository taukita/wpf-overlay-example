namespace WpfApp1.ViewModels
{
	using Prism.Mvvm;

	internal class OverlayItemViewModel : BindableBase
	{
		private bool _flag;

		public OverlayItemViewModel(string title)
		{
			Title = title;
		}

		public bool Flag
		{
			get => _flag;
			set => SetProperty(ref _flag, value);
		}

		public string Title { get; }
	}
}