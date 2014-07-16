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
using Unv.PeriodicTableSelectorLib;


namespace TestDemo01.Examples
{
	/// <summary>
	/// Interaction logic for Halloween.xaml
	/// </summary>
	public partial class Halloween 
		: UserControl
	{
		#region Attributes
		public readonly static DependencyProperty GlowBrushProperty;
		public readonly static DependencyProperty TextBrushProperty;
		public readonly static DependencyProperty BackgroundBrushProperty;
		#endregion


		#region Properties
		public Brush GlowBrush
		{
			get { return (Brush) GetValue(GlowBrushProperty); }
			set { SetValue(GlowBrushProperty, value); }
		}

		public Brush TextBrush
		{
			get { return (Brush) GetValue(TextBrushProperty); }
			set { SetValue(TextBrushProperty, value); }
		}

		public Brush BackgroundBrush
		{
			get { return (Brush) GetValue(BackgroundBrushProperty); }
			set { SetValue(BackgroundBrushProperty, value); }
		}
		#endregion


		#region Constructors
		static Halloween()
		{
			GlowBrushProperty = DependencyProperty.Register(
				"GlowBrush",
				typeof(Brush),
				typeof(Halloween),
				new PropertyMetadata(
					Brushes.Orange,
					GlowBrushProperty_Changed));

			TextBrushProperty = DependencyProperty.Register(
				"TextBrush",
				typeof(Brush),
				typeof(Halloween),
				new PropertyMetadata(
					Brushes.Black,
					TextBrushProperty_Changed));

			BackgroundBrushProperty = DependencyProperty.Register(
				"BackgroundBrush",
				typeof(Brush),
				typeof(Halloween),
				new PropertyMetadata(
					Brushes.Black,
					BackgroundBrushProperty_Changed));
		}

		public Halloween()
		{
			InitializeComponent();

			var group = this.GetAllChemicalsGroup();
			group.SelectChemicals();
		}
		#endregion


		#region Dependency Property Event Handlers
		static void GlowBrushProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var me = (Halloween) d;

			var chems = me.GetAllChemicalsGroup();
			chems.SetGlowBrush((Brush) e.NewValue);
		}
		static void TextBrushProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var me = (Halloween) d;

			var chems = me.GetAllChemicalsGroup();
			chems.SetForeground((Brush) e.NewValue);

		}
		static void BackgroundBrushProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var me = (Halloween) d;

			var chems = me.GetAllChemicalsGroup();
			chems.SetBackground((Brush) e.NewValue);
		}
		#endregion


		#region Methods
		private ChemicalGroup GetAllChemicalsGroup()
		{
			var group = this.Selector.ChemicalElementFactory.ChemicalGroups.First(g => { return g.GroupName == "All Chemical Elements"; });
			return group;
		}
		#endregion
	}
}
