using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TestDemo01.Examples
{
	/// <summary>
	/// Interaction logic for Halloween.xaml
	/// </summary>
	public partial class Halloween : UserControl
	{
		public Halloween()
		{
			InitializeComponent();

			var group = this.Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "All Chemical Elements"; });
			group.SetBackground(Brushes.Black);
			group.SetForeground(Brushes.Black);
			group.SetGlowBrush(Brushes.Orange);

			group.SelectChemicals();
		}
	}
}
