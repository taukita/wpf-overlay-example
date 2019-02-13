﻿namespace WpfApp1.ViewModels
{
	using Prism.Mvvm;

	internal class MainViewModel : BindableBase
	{
		public MainViewModel()
		{
			Overlay = new OverlayViewModel(2);
		}

		public OverlayViewModel Overlay { get; }
	}
}