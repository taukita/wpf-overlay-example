using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
	using System.Collections.ObjectModel;

	class TestViewModel
    {
	    public TestViewModel()
	    {
			Strings.Add("Uno");
		    Strings.Add("Dos");
		    Strings.Add("Tres");
		}

	    public ObservableCollection<string> Strings { get; } = new ObservableCollection<string>();
    }
}
