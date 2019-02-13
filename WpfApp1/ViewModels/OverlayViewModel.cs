namespace WpfApp1.ViewModels
{
	using System;
	using System.Collections.ObjectModel;
	using System.Windows.Input;
	using Prism.Commands;
	using Prism.Mvvm;

	internal class OverlayViewModel : BindableBase
	{
		private readonly Action _hideCallback;

		public OverlayViewModel()
			:this(0)
		{
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
			Hide = new DelegateCommand(() => _hideCallback?.Invoke());
		}

		public OverlayViewModel(Action hideCallback, int n)
			: this(n)
		{
			_hideCallback = hideCallback;
		}

		public ICommand Hide { get; }

		public ObservableCollection<OverlayItemViewModel> Items { get; } =
			new ObservableCollection<OverlayItemViewModel>();
	}
}