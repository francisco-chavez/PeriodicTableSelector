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
				new PropertyMetadata(null));

			ChemicalElementPlacerProperty = DependencyProperty.Register(
				"ChemicalElementPlacer",
				typeof(ElementArrangementBase),
				typeof(PeriodicTable),
				new PropertyMetadata(null));
		}
		#endregion


		#region Methods
		protected override Size MeasureOverride(Size constraint)
		{
			return base.MeasureOverride(constraint);
		}

		protected override Size ArrangeOverride(Size arrangeBounds)
		{
			return base.ArrangeOverride(arrangeBounds);
		}

		public override void OnApplyTemplate()
		{
			m_drawArea = GetTemplateChild("PART_CanvasArea") as Canvas;
			base.OnApplyTemplate();
		}
		#endregion
	}
}
