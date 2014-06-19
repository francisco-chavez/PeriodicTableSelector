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

		// Yes, I know that using Dependency Properties would take up
		// less memory, but all I'm doing is showing off some of the
		// things I can do with the ChemicalGroup class.
		// -FCT
		public Brush BackgroundBrush
		{
			get { return mn_backgroundBrush; }
			set
			{
				if (mn_backgroundBrush != value)
				{
					mn_backgroundBrush = value;
					OnPropertyChanged("BackgroundBrush");

					if (Group != null)
						Group.SetBackground(value);
				}
			}
		}
		private Brush mn_backgroundBrush;

		public Brush ForegroundBrush
		{
			get { return mn_foregroundBrush; }
			set
			{
				if (mn_foregroundBrush != value)
				{
					mn_foregroundBrush = value;
					OnPropertyChanged("ForegroundBrush");

					if (Group != null)
						Group.SetForeground(value);
				}
			}
		}
		private Brush mn_foregroundBrush;

		public Brush GlowBrush
		{
			get { return mn_glowBrush; }
			set
			{
				if (mn_glowBrush != value)
				{
					mn_glowBrush = value;
					OnPropertyChanged("GlowBrush");

					if (Group != null)
						Group.SetGlowBrush(value);
				}
			}
		}
		private Brush mn_glowBrush;
		#endregion


		#region Constructors
		public GroupManipulator()
		{
			InitializeComponent();
			DataContext = this;

			this.BackgroundBrush = Brushes.Green;
			this.ForegroundBrush = Brushes.Green;
			this.GlowBrush = Brushes.Green;
		}
		#endregion


		#region Event Handlers
		private void ChangeBackground_Click(object sender, RoutedEventArgs e)
		{
			bool changeColor;

			var brush = SelectNewBrush(out changeColor);
			if (changeColor)
				BackgroundBrush = brush;
		}

		private void ChangeForeground_Click(object sender, RoutedEventArgs e)
		{
			bool changeColor;

			var brush = SelectNewBrush(out changeColor);
			if (changeColor)
				ForegroundBrush = brush;
		}

		private void ChangeGlowBrush_Click(object sender, RoutedEventArgs e)
		{
			bool changeColor;

			var brush = SelectNewBrush(out changeColor);
			if (changeColor)
				GlowBrush = brush;
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
