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
		#region Attributes
		private static readonly Dictionary<int, int> m_atomicNumberToGroupMap;
		private static readonly Dictionary<int, int> m_atomicNumberToPeriodMap;
		#endregion


		#region Constructors
		static StandardTableArrangement()
		{
			var aToGMap = new Dictionary<int, int>(118);
			var aToPMap = new Dictionary<int, int>(118);

			///
			/// Elements 1 - 5
			/// 
			aToGMap.Add(1, 1);
			aToPMap.Add(1, 1);

			aToGMap.Add(2, 18);
			aToPMap.Add(2, 1);

			aToGMap.Add(3, 1);
			aToPMap.Add(3, 2);

			aToGMap.Add(4, 2);
			aToPMap.Add(4, 2);

			aToGMap.Add(5, 13);
			aToPMap.Add(5, 2);


			///
			/// Elements 6 - 10
			/// 
			aToGMap.Add(6, 14);
			aToPMap.Add(6, 2);

			aToGMap.Add(7, 15);
			aToPMap.Add(7, 2);

			aToGMap.Add(8, 16);
			aToPMap.Add(8, 2);

			aToGMap.Add(9, 17);
			aToPMap.Add(9, 2);

			aToGMap.Add(10, 18);
			aToPMap.Add(10, 2);


			m_atomicNumberToGroupMap = aToGMap;
			m_atomicNumberToPeriodMap = aToPMap;
		}

		public StandardTableArrangement() { }
		#endregion


		#region Methods
		public override void InsertElements(Canvas displayArea, FactoryBase chemicalFactory)
		{
			foreach (var element in chemicalFactory.Elements)
				displayArea.Children.Add(element);
		}

		public override Size MeasureElements(Size constraint, FactoryBase chemicalFactory)
		{
			Size neededSize = new Size(0, 0);

			foreach (var element in chemicalFactory.Elements)
			{
				element.Measure(constraint);
				var desiredSize = element.DesiredSize;

				neededSize.Width = Math.Max(neededSize.Width, desiredSize.Width);
				neededSize.Height = Math.Max(neededSize.Height, desiredSize.Height);

				neededSize.Width = Math.Max(neededSize.Width, desiredSize.Width * (m_atomicNumberToGroupMap[element.AtomicNumber] - 1));
				neededSize.Height = Math.Max(neededSize.Height, desiredSize.Height * (m_atomicNumberToPeriodMap[element.AtomicNumber] - 1));
			}

			return neededSize;
		}

		public override Size ArrangeElements(Canvas displayArea, Size arrangeBounds, FactoryBase chemicalFactory)
		{
			var size = new Size(0, 0);

			foreach (var element in chemicalFactory.Elements)
			{
				var elementSize = element.DesiredSize;

				double x = elementSize.Width * (m_atomicNumberToGroupMap[element.AtomicNumber] - 1);
				double y = elementSize.Height * (m_atomicNumberToPeriodMap[element.AtomicNumber] - 1);
				
				Canvas.SetLeft(element, x);
				Canvas.SetTop(element, y);

				size.Width = Math.Max(size.Width, x + elementSize.Width);
				size.Height = Math.Max(size.Height, y + elementSize.Height);

				element.Arrange(new Rect(elementSize));
			}

			return size;
		}
		#endregion
	}
}
