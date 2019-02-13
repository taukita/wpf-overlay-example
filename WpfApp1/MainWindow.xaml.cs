using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
	using WpfApp1.ViewModels;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static RoutedCommand MyCommand0 = new RoutedCommand();

		static MainWindow()
		{
			MyCommand0.InputGestures.Add(new KeyGesture(Key.D0, ModifierKeys.Shift | ModifierKeys.Control));
		}

		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}
	}
}
