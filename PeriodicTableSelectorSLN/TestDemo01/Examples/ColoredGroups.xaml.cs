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

using Unv.PeriodicTableSelectorLib;


namespace TestDemo01.Examples
{
	/// <summary>
	/// Interaction logic for ColoredGroups.xaml
	/// </summary>
	public partial class ColoredGroups 
		: UserControl
	{
		public ColoredGroups()
		{
			InitializeComponent();

			SetColors();
		}

		private void SetColors()
		{
			ChemicalGroup group = null;
			
			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Alkali Metals"; });
			group.SetBackground(Brushes.MediumAquamarine);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Alkaline Earth Metals"; });
			group.SetBackground(Brushes.Red);
			group.SetGlowBrush(Brushes.YellowGreen);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Transition Metals"; });
			group.SetBackground(Brushes.Yellow);
			group.SetGlowBrush(Brushes.MediumVioletRed);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Other Metals"; });
			group.SetBackground(Brushes.Aquamarine);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Metalloids"; });
			group.SetBackground(Brushes.Purple);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Non-Metals"; });
			group.SetBackground(Brushes.LawnGreen);
			group.SetGlowBrush(Brushes.Red);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Halogens"; });
			group.SetBackground(Brushes.Orchid);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Noble Gases"; });
			group.SetBackground(Brushes.Orange);
			group.SetGlowBrush(Brushes.Green);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Lanthanide Series"; });
			group.SetBackground(Brushes.DarkRed);

			group = Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "Actinide Series"; });
			group.SetBackground(Brushes.DarkOliveGreen);
		}
	}
}
