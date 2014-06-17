using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using Forms			= System.Windows.Forms;
using ColorDialog	= System.Windows.Forms.ColorDialog;


namespace TestDemo01.Examples.Tools
{
	/// <summary>
	/// Interaction logic for GroupManipulator.xaml
	/// </summary>
	public partial class GroupManipulator 
		: UserControl
	{
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion


		#region Properties
		public string GroupName
		{
			get { return mn_groupName; }
			set
			{
				if (mn_groupName != value)
				{
					mn_groupName = value;
					OnPropertyChanged("GroupName");
				}
			}
		}
		private string mn_groupName;

		public ChemicalGroup Group
		{
			get { return mn_group; }
			set
			{
				if (mn_group != value)
				{
					mn_group = value;
					OnPropertyChanged("Group");

					if (value != null)
					{
						GroupName = value.GroupName;
					}
				}
			}
		}
		private ChemicalGroup mn_group;
		#endregion


		#region Constructors
		public GroupManipulator()
		{
			InitializeComponent();
			DataContext = this;
		}
		#endregion


		#region Event Handlers
		private void ChangeBackground_Click(object sender, RoutedEventArgs e)
		{
			bool changeColor;

			var brush = SelectNewBrush(out changeColor);
			if (changeColor)
				Group.SetBackground(brush);
		}

		private void ChangeForeground_Click(object sender, RoutedEventArgs e)
		{
			bool changeColor;

			var brush = SelectNewBrush(out changeColor);
			if (changeColor)
				Group.SetForeground(brush);
		}

		private void ChangeGlowBrush_Click(object sender, RoutedEventArgs e)
		{
			bool changeColor;

			var brush = SelectNewBrush(out changeColor);
			if (changeColor)
				Group.SetGlowBrush(brush);
		}
		#endregion


		#region Methods
		private Brush SelectNewBrush(out bool useIt)
		{
			var dlg = new ColorDialog();
			dlg.AllowFullOpen = true;
			dlg.AnyColor = false;
			dlg.FullOpen = true;
			dlg.SolidColorOnly = true;

			var dialogResult = dlg.ShowDialog();

			switch (dialogResult)
			{
			case Forms.DialogResult.Abort:
			case Forms.DialogResult.Cancel:
			case Forms.DialogResult.Ignore:
			case Forms.DialogResult.No:
			case Forms.DialogResult.None:
			case Forms.DialogResult.Retry:
				useIt = false;
				break;

			default:
				useIt = true;
				break;
			}

			var userColor = dlg.Color;
			var correctedColorType = new Color();
			correctedColorType.A = userColor.A;
			correctedColorType.B = userColor.B;
			correctedColorType.G = userColor.G;
			correctedColorType.R = userColor.R;

			SolidColorBrush brush = new SolidColorBrush(correctedColorType);

			return brush;
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
