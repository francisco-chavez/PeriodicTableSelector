using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using TestDemo01.Examples.Tools;


namespace TestDemo01.Examples
{
	/// <summary>
	/// Interaction logic for SelectableColors.xaml
	/// </summary>
	public partial class SelectableColors 
		: UserControl
	{
		#region Attributes
		public static readonly DependencyProperty GroupListProperty =
			DependencyProperty.Register(
				"GroupList", 
				typeof(ObservableCollection<GroupManipulator>), 
				typeof(SelectableColors), 
				new PropertyMetadata(null));
		#endregion


		#region Properties
		public ObservableCollection<GroupManipulator> GroupList
		{
			get { return (ObservableCollection<GroupManipulator>) GetValue(GroupListProperty); }
			set { SetValue(GroupListProperty, value); }
		}
		#endregion


		#region Constructors
		public SelectableColors()
		{
			InitializeComponent();
			this.DataContext = this;

			GroupList = new ObservableCollection<GroupManipulator>();

			this.Loaded += SelectableColors_Loaded;
		}
		#endregion


		#region Event Handlers
		void SelectableColors_Loaded(object sender, RoutedEventArgs e)
		{
			foreach (var group in Selector.ChemicalElementFactory.ChemicalGroups)
				GroupList.Add(new GroupManipulator() { Group = group });
		}
		#endregion
	}
}
