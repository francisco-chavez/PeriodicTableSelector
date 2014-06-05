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
		public static readonly DependencyProperty ChemicalElementFactoryProperty;

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
		#endregion


		#region Constructors
		public PeriodicTable()
		{
			ChemicalElementFactory	= new StandardTableFactory();
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
				pTable.Items.Add(chem);
		}
		#endregion


		#region Methods
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			m_drawArea = GetTemplateChild("PART_CanvasArea") as Canvas;
		}
		#endregion
	}
}
