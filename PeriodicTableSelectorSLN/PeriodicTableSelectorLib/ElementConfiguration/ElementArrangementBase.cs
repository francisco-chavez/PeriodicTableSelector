using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Unv.PeriodicTableSelectorLib.ElementCreation;


namespace Unv.PeriodicTableSelectorLib.ElementConfiguration
{
	public abstract class ElementArrangementBase
	{
		public abstract void InsertElements(Canvas displayArea, FactoryBase chemicalFactory);
		public abstract void ArrangeElements(Canvas displayArea, FactoryBase chemicalFactory);
	}
}
