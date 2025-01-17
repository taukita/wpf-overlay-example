﻿namespace WpfApp1.ViewModels
{
	using Prism.Mvvm;

	internal class OverlayItemViewModel : BindableBase
	{
		private bool _flag;

		private int _number;

		private int _subflag;

		public OverlayItemViewModel(string title)
		{
			Title = title;
		}

		public bool Flag
		{
			get => _flag;
			set => SetProperty(ref _flag, value);
		}

		public int Number
		{
			get => _number;
			set => SetProperty(ref _number, value);
		}

		public int Subflag
		{
			get => _subflag;
			set => SetProperty(ref _subflag, value);
		}

		public string Title { get; }
	}
}