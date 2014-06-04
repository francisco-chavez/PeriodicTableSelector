using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Unv.PeriodicTableSelectorLib.ElementCreation;


namespace Unv.PeriodicTableSelectorLib.ElementConfiguration
{
	public class StandardTableArrangement
		: ElementArrangementBase
	{
		public override void InsertElements(Canvas displayArea, FactoryBase chemicalFactory)
		{
			foreach (var element in chemicalFactory.Elements)
				displayArea.Children.Add(element);
		}

		public override Size MeasureElements(Size constraint, FactoryBase chemicalFactory)
		{
			return new Size(50, 50);
		}

		public override Size ArrangeElements(Canvas displayArea, Size arrangeBounds, FactoryBase chemicalFactory)
		{
			return new Size(50, 50);
		}
	}
}
