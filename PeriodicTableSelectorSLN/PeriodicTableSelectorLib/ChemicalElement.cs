using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Unv.PeriodicTableSelectorLib
{
	/// <summary>
	/// This class represents an interactive chemical element from the
	/// periodic table.
	/// </summary>
	public class ChemicalElement
		: ToggleButton
	{
		#region Attributes
		public static readonly DependencyProperty ChemicalNameProperty;
		public static readonly DependencyProperty AtomicNumberProperty;
		public static readonly DependencyProperty AtomicMassProperty;
		public static readonly DependencyProperty SymbolProperty;

		public static readonly DependencyProperty GlowBrushProperty;
		#endregion


		#region Properties
		/// <summary>
		/// Gets or sets the Brush that is used to generate a glow when
		/// selected (IsChecked == True).
		/// </summary>
		public Brush GlowBrush
		{
			get { return (Brush) GetValue(GlowBrushProperty); }
			set { SetValue(GlowBrushProperty, value); }
		}

		/// <summary>
		/// Gets or sets the name of the chemical element.
		/// </summary>
		public string ChemicalName
		{
			get { return (string) GetValue(ChemicalNameProperty); }
			set { SetValue(ChemicalNameProperty, value); }
		}

		/// <summary>
		/// Gets or sets the atomic number of the chemical element.
		/// </summary>
		public int AtomicNumber
		{
			get { return (int) GetValue(AtomicNumberProperty); }
			set { SetValue(AtomicNumberProperty, value); }
		}

		/// <summary>
		/// Gets or sets the mean atomic mass of the chemical element.
		/// </summary>
		public double AtomicMass
		{
			get { return (double) GetValue(AtomicMassProperty); }
			set { SetValue(AtomicMassProperty, value); }
		}

		/// <summary>
		/// Gets or sets the atomic symbol of the chemical element.
		/// </summary>
		public string Symbol
		{
			get { return (string) GetValue(SymbolProperty); }
			set { SetValue(SymbolProperty, value); }
		}
		#endregion


		#region Constructors
		static ChemicalElement()
		{
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(ChemicalElement),
				new FrameworkPropertyMetadata(typeof(ChemicalElement)));


			ChemicalNameProperty = DependencyProperty.Register(
				"ChemicalName",
				typeof(string),
				typeof(ChemicalElement),
				new PropertyMetadata(null));

			AtomicNumberProperty = DependencyProperty.Register(
				"AtomicNumber",
				typeof(int),
				typeof(ChemicalElement),
				new PropertyMetadata(0));

			AtomicMassProperty = DependencyProperty.Register(
				"AtomicMass",
				typeof(double),
				typeof(ChemicalElement),
				new PropertyMetadata(0.0));

			SymbolProperty = DependencyProperty.Register(
				"Symbol",
				typeof(string),
				typeof(ChemicalElement),
				new PropertyMetadata(null));

			GlowBrushProperty = DependencyProperty.Register(
				"GlowBrush",
				typeof(Brush),
				typeof(ChemicalElement),
				new PropertyMetadata(Brushes.Gold));
		}
		#endregion
	}
}
