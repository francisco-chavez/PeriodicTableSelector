using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Unv.PeriodicTableSelectorLib.ElementCreation;


namespace Unv.PeriodicTableSelectorLib
{
	[TemplatePart(Name = "PART_CanvasArea", Type = typeof(StandardCanvas))]
	public class PeriodicTable 
		: ItemsControl
	{
		#region Attributes
		public static readonly DependencyProperty		ChemicalElementFactoryProperty;
		public static readonly DependencyProperty		ChemicalElementWidthProperty;
		public static readonly DependencyProperty		ChemicalElementHeightProperty;

		private static readonly DependencyPropertyKey	SelectedElementsPropertyKey;
		private static readonly DependencyProperty		SelectedElementsProperty;


		private StandardCanvas m_drawArea;
		#endregion


		#region Properties
		/// <summary>
		/// Gets or sets the factory class object that is in charge 
		/// of the creation and general managment of the chemical 
		/// element objects in this periodic table.
		/// </summary>
		public FactoryBase ChemicalElementFactory
		{
			get { return (FactoryBase) GetValue(ChemicalElementFactoryProperty); }
			set { SetValue(ChemicalElementFactoryProperty, value); }
		}

		public double ChemicalElementHeight
		{
			get { return (double) GetValue(ChemicalElementHeightProperty); }
			set { SetValue(ChemicalElementHeightProperty, value); }
		}

		public double ChemicalElementWidth
		{
			get { return (double) GetValue(ChemicalElementWidthProperty); }
			set { SetValue(ChemicalElementWidthProperty, value); }
		}

		public ObservableCollection<ChemicalElement> SelectedElements
		{
			get { return (ObservableCollection<ChemicalElement>) GetValue(SelectedElementsProperty); }
			set { SetValue(SelectedElementsPropertyKey, value); }
		}
		#endregion


		#region Constructors
		static PeriodicTable()
		{
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(PeriodicTable),
				new FrameworkPropertyMetadata(typeof(PeriodicTable)));


			ChemicalElementFactoryProperty = DependencyProperty.Register(
				"ChemicalElementFactory",
				typeof(FactoryBase),
				typeof(PeriodicTable),
				new FrameworkPropertyMetadata(
					null,
					FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
					ChemicalElementFactory_Changed));

			ChemicalElementWidthProperty = DependencyProperty.Register(
				"ChemicalElementWidth",
				typeof(double),
				typeof(PeriodicTable),
				new FrameworkPropertyMetadata(
					80.0,
					FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange,
					ChemicalWidth_Changed));

			ChemicalElementHeightProperty = DependencyProperty.Register(
				"ChemicalElementHeight",
				typeof(double),
				typeof(PeriodicTable),
				new FrameworkPropertyMetadata(
					110.0,
					ChemicalHeight_Changed));

			SelectedElementsPropertyKey = DependencyProperty.RegisterReadOnly(
				"SelectedElements",
				typeof(ObservableCollection<ChemicalElement>),
				typeof(PeriodicTable),
				new FrameworkPropertyMetadata(
					null));
			SelectedElementsProperty = SelectedElementsPropertyKey.DependencyProperty;
		}

		public PeriodicTable()
		{
			ChemicalElementFactory	= new StandardTableFactory();
		}
		#endregion


		#region Dependency Property Event Handlers
		private static void ChemicalElementFactory_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PeriodicTable pTable = (PeriodicTable) d;

			pTable.Items.Clear();

			var factory = e.NewValue as FactoryBase;
			if (factory == null)
				return;

			foreach (var chem in factory.Elements)
			{
				chem.Width = pTable.ChemicalElementWidth;
				chem.Height = pTable.ChemicalElementHeight;
				pTable.Items.Add(chem);
			}

			pTable.SelectedElements = factory.SelectedElements;
		}

		private static void ChemicalWidth_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var pTable = (PeriodicTable) d;

			if (pTable.m_drawArea == null)
				return;

			double width = (double) e.NewValue;
			pTable.m_drawArea.ChemicalElementWidth = width;
		}

		private static void ChemicalHeight_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var pTable = (PeriodicTable) d;

			if (pTable.m_drawArea == null)
				return;

			double height = (double) e.NewValue;
			pTable.m_drawArea.ChemicalElementHeight = height;
		}
		#endregion


		#region Methods
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			m_drawArea = GetTemplateChild("PART_CanvasArea") as StandardCanvas;
			if (m_drawArea != null)
			{
				m_drawArea.ChemicalElementWidth = this.ChemicalElementWidth;
				m_drawArea.ChemicalElementHeight = this.ChemicalElementHeight;
			}
		}
		#endregion
	}
}
