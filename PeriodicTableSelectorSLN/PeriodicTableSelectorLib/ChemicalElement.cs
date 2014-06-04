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
	public class ChemicalElement 
		: ToggleButton
	{
		#region Attributes
		public static readonly DependencyProperty ChemicalNameProperty;
		public static readonly DependencyProperty AtomicNumberProperty;
		public static readonly DependencyProperty AtomicMassProperty;
		public static readonly DependencyProperty SymbolProperty;
		#endregion


		#region Properties
		public string ChemicalName
		{
			get { return (string) GetValue(ChemicalNameProperty); }
			set { SetValue(ChemicalNameProperty, value); }
		}

		public int AtomicNumber
		{
			get { return (int) GetValue(AtomicNumberProperty); }
			set { SetValue(AtomicNumberProperty, value); }
		}

		public double AtomicMass
		{
			get { return (double) GetValue(AtomicMassProperty); }
			set { SetValue(AtomicMassProperty, value); }
		}

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
				new PropertyMetadata(0));

			SymbolProperty = DependencyProperty.Register(
				"Symbol",
				typeof(string),
				typeof(ChemicalElement),
				new PropertyMetadata(null));
		}
		#endregion
	}
}
