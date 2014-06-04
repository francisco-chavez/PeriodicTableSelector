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

using Unv.PeriodicTableSelectorLib.ElementConfiguration;
using Unv.PeriodicTableSelectorLib.ElementCreation;


namespace Unv.PeriodicTableSelectorLib
{
	[TemplatePart(Name = "PART_CanvasArea", Type = typeof(Canvas))]
	public class PeriodicTable 
		: Control
	{
		#region Attributes
		public static readonly DependencyProperty ChemicalElementFactoryProperty;
		public static readonly DependencyProperty ChemicalElementPlacerProperty;

		private Canvas m_drawArea;
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

		/// <summary>
		/// Gets or sets the class object that's in charge of the
		/// placement of the chemical element objects on the GUI.
		/// </summary>
		public ElementArrangementBase ChemicalElementPlacer
		{
			get { return (ElementArrangementBase) GetValue(ChemicalElementPlacerProperty); }
			set { SetValue(ChemicalElementPlacerProperty, value); }
		}
		#endregion


		#region Constructors
		public PeriodicTable()
		{
			ChemicalElementFactory	= new StandardTableFactory();
			ChemicalElementPlacer	= new StandardTableArrangement();
		}

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

			ChemicalElementPlacerProperty = DependencyProperty.Register(
				"ChemicalElementPlacer",
				typeof(ElementArrangementBase),
				typeof(PeriodicTable),
				new FrameworkPropertyMetadata(
					null,
					FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
					ChemicalElementPlacer_Changed));
		}
		#endregion


		#region Dependency Property Event Handlers
		private static void ChemicalElementFactory_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PeriodicTable pTable = (PeriodicTable) d;

			pTable.ClearCanvas();
			pTable.PopulateCanvas((FactoryBase) e.NewValue, pTable.ChemicalElementPlacer);
		}

		private static void ChemicalElementPlacer_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PeriodicTable pTable = (PeriodicTable) d;

			pTable.ClearCanvas();
			pTable.PopulateCanvas(pTable.ChemicalElementFactory, (ElementArrangementBase) e.NewValue);
		}
		#endregion


		#region Methods
		protected override Size MeasureOverride(Size constraint)
		{
			if (m_drawArea == null || ChemicalElementFactory == null || ChemicalElementPlacer == null)
				return base.MeasureOverride(constraint);

			return ChemicalElementPlacer.MeasureElements(constraint, ChemicalElementFactory);
		}

		protected override Size ArrangeOverride(Size arrangeBounds)
		{
			if (m_drawArea == null || ChemicalElementFactory == null || ChemicalElementPlacer == null)
				return base.ArrangeOverride(arrangeBounds);

			Size realSize = ChemicalElementPlacer.ArrangeElements(m_drawArea, arrangeBounds, ChemicalElementFactory);
			m_drawArea.Measure(realSize);
			m_drawArea.Arrange(new Rect(realSize));

			return realSize;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			m_drawArea = GetTemplateChild("PART_CanvasArea") as Canvas;
			PopulateCanvas(ChemicalElementFactory, ChemicalElementPlacer);
		}

		private void PopulateCanvas(FactoryBase chemicalFactory, ElementArrangementBase elementPlacer)
		{
			if (chemicalFactory == null)
				return;

			if (elementPlacer == null)
				return;

			if (m_drawArea == null)
				return;

			m_drawArea.Children.Clear();
			elementPlacer.InsertElements(m_drawArea, chemicalFactory);
		}

		private void ClearCanvas()
		{
			if (m_drawArea != null)
				m_drawArea.Children.Clear();
		}
		#endregion
	}
}
