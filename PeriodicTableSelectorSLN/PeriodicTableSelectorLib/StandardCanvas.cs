using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Unv.PeriodicTableSelectorLib.ElementCreation;


namespace Unv.PeriodicTableSelectorLib
{
	public class StandardCanvas
		: Canvas
	{
		#region Properties
		public Dictionary<int, int> AtomicNumberToGroupMap	{ get; protected set; }
		public Dictionary<int, int> AtomicNumberToPeriodMap { get; protected set; }

		public FactoryBase			ChemicalElementFactory	{ get; set; }
		#endregion


		#region Constructors
		public StandardCanvas()
		{
			AtomicNumberToGroupMap = new Dictionary<int, int>(118);
			AtomicNumberToPeriodMap = new Dictionary<int, int>(118);

			SetChemicalMetaData();
		}
		#endregion


		#region Methods
		protected override Size MeasureOverride(Size constraint)
		{
			Size size = new Size();

			foreach (UIElement child in this.InternalChildren)
			{
				child.Measure(constraint);
				var desiredSize = child.DesiredSize;

				if (child is ChemicalElement)
				{
					var chem = (ChemicalElement) child;

					size.Width = Math.Max(size.Width, desiredSize.Width * AtomicNumberToGroupMap[chem.AtomicNumber]);
					size.Height = Math.Max(size.Height, desiredSize.Height * AtomicNumberToPeriodMap[chem.AtomicNumber]);
				}
				else
				{
					size.Width = Math.Max(size.Width, desiredSize.Width);
					size.Height = Math.Max(size.Height, desiredSize.Height);
				}
			}

			return size;
		}

		protected override Size ArrangeOverride(Size arrangeSize)
		{
			foreach (UIElement child in InternalChildren)
			{
				child.Arrange(new Rect(arrangeSize));

				if (child is ChemicalElement)
				{
					var chem = (ChemicalElement) child;

					Canvas.SetLeft(chem, (AtomicNumberToGroupMap[chem.AtomicNumber] - 1) * chem.ActualWidth);
					Canvas.SetTop(chem, (AtomicNumberToPeriodMap[chem.AtomicNumber] - 1) * chem.ActualHeight);
				}
			}

			return arrangeSize;
		}

		private void SetChemicalMetaData()
		{
			var aToGMap = AtomicNumberToGroupMap;
			var aToPMap = AtomicNumberToPeriodMap;

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


			///
			/// Elements 11 - 15
			/// 
			aToGMap.Add(11, 1);
			aToPMap.Add(11, 3);

			aToGMap.Add(12, 2);
			aToPMap.Add(12, 3);

			aToGMap.Add(13, 13);
			aToPMap.Add(13, 3);

			aToGMap.Add(14, 14);
			aToPMap.Add(14, 3);

			aToGMap.Add(15, 15);
			aToPMap.Add(15, 3);


			///
			/// Elements 16 - 20
			/// 
			aToGMap.Add(16, 16);
			aToPMap.Add(16, 3);

			aToGMap.Add(17, 17);
			aToPMap.Add(17, 3);

			aToGMap.Add(18, 18);
			aToPMap.Add(18, 3);

			aToGMap.Add(19, 1);
			aToPMap.Add(19, 4);

			aToGMap.Add(20, 2);
			aToPMap.Add(20, 4);
		}
		#endregion
	}
}
