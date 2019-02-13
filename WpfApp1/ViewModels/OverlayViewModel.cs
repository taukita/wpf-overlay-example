namespace WpfApp1.ViewModels
{
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Input;
	using Prism.Commands;
	using Prism.Mvvm;

	internal class OverlayViewModel : BindableBase
	{
		private readonly OverlayItemViewModel _allItem;
		private bool _isShown;

		public OverlayViewModel()
			: this(0)
		{
			IsShown = true;
		}

		public OverlayViewModel(int n)
		{
			_allItem = new OverlayItemViewModel("ВСЕ!");

			Items.Add(_allItem);
			_allItem.PropertyChanged += AllItemOnPropertyChanged;

			AddItem(new OverlayItemViewModel("Первый элемент"));
			AddItem(new OverlayItemViewModel("Второй элемент"));
			AddItem(new OverlayItemViewModel("Третий элемент"));
			for (var i = 0; i < n; i++)
			{
				AddItem(new OverlayItemViewModel($"Элемент №{i + 3}"));
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

		private void AddItem(OverlayItemViewModel item)
		{
			Items.Add(item);
			item.PropertyChanged += ItemOnPropertyChanged;
		}

		private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(OverlayItemViewModel.Flag) && sender is OverlayItemViewModel item)
			{
				if (!item.Flag)
				{
					_allItem.PropertyChanged -= AllItemOnPropertyChanged;
					_allItem.Flag = false;
					_allItem.PropertyChanged += AllItemOnPropertyChanged;
				}
				else if (Items.Skip(1).All(i => i.Flag))
				{
					_allItem.PropertyChanged -= AllItemOnPropertyChanged;
					_allItem.Flag = true;
					_allItem.PropertyChanged += AllItemOnPropertyChanged;
				}
			}
		}

		private void AllItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(OverlayItemViewModel.Flag))
			{
				foreach (var item in Items.Skip(1))
				{
					item.Flag = _allItem.Flag;
				}
			}
		}
	}
}