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
		#region Attributes
		#endregion


		#region Properties
		public Dictionary<int, int> AtomicNumberToGroupMap	{ get; protected set; }
		public Dictionary<int, int> AtomicNumberToPeriodMap { get; protected set; }

		private double m_chemicalWidth = 60;
		public double ChemicalElementWidth
		{
			get { return m_chemicalWidth; }
			set 
			{ 
				foreach(UIElement child in InternalChildren)
					if(child is ChemicalElement)
					{
						((ChemicalElement) child).Width = value;
					}
				m_chemicalWidth = value; 
			}
		}
		public double				ChemicalElementHeight	{ get; set; }
		#endregion


		#region Constructors
		public StandardCanvas()
		{
			AtomicNumberToGroupMap = new Dictionary<int, int>(118);
			AtomicNumberToPeriodMap = new Dictionary<int, int>(118);

			ChemicalElementHeight = 60;

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

					size.Width = Math.Max(size.Width, ChemicalElementWidth * AtomicNumberToGroupMap[chem.AtomicNumber]);
					size.Height = Math.Max(size.Height, ChemicalElementHeight * AtomicNumberToPeriodMap[chem.AtomicNumber]);
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
				//child.Arrange(new Rect(arrangeSize));

				if (child is ChemicalElement)
				{
					var chem = (ChemicalElement) child;
					var desiredSize = chem.DesiredSize;

					Rect r = new Rect(
						new Point(
							(AtomicNumberToGroupMap[chem.AtomicNumber] - 1) * ChemicalElementWidth,
							(AtomicNumberToPeriodMap[chem.AtomicNumber] - 1) * ChemicalElementHeight),
						desiredSize);

					child.Arrange(r);
					//Canvas.SetLeft(chem, (AtomicNumberToGroupMap[chem.AtomicNumber] - 1) * chem.ActualWidth);
					//Canvas.SetTop(chem, (AtomicNumberToPeriodMap[chem.AtomicNumber] - 1) * chem.ActualHeight);
				}
			}

			return arrangeSize;
		}

		private void SetChemicalMetaData()
		{
			var agMap = AtomicNumberToGroupMap;
			var apMap = AtomicNumberToPeriodMap;

			///
			/// Elements 1 - 5
			/// 
			agMap.Add(1, 1);
			apMap.Add(1, 1);

			agMap.Add(2, 18);
			apMap.Add(2, 1);

			agMap.Add(3, 1);
			apMap.Add(3, 2);

			agMap.Add(4, 2);
			apMap.Add(4, 2);

			agMap.Add(5, 13);
			apMap.Add(5, 2);


			///
			/// Elements 6 - 10
			/// 
			agMap.Add(6, 14);
			apMap.Add(6, 2);

			agMap.Add(7, 15);
			apMap.Add(7, 2);

			agMap.Add(8, 16);
			apMap.Add(8, 2);

			agMap.Add(9, 17);
			apMap.Add(9, 2);

			agMap.Add(10, 18);
			apMap.Add(10, 2);


			///
			/// Elements 11 - 15
			/// 
			agMap.Add(11, 1);
			apMap.Add(11, 3);

			agMap.Add(12, 2);
			apMap.Add(12, 3);

			agMap.Add(13, 13);
			apMap.Add(13, 3);

			agMap.Add(14, 14);
			apMap.Add(14, 3);

			agMap.Add(15, 15);
			apMap.Add(15, 3);


			///
			/// Elements 16 - 20
			/// 
			agMap.Add(16, 16);
			apMap.Add(16, 3);

			agMap.Add(17, 17);
			apMap.Add(17, 3);

			agMap.Add(18, 18);
			apMap.Add(18, 3);

			agMap.Add(19, 1);
			apMap.Add(19, 4);

			agMap.Add(20, 2);
			apMap.Add(20, 4);

		
			///
			/// Elements 21 - 25
			/// 
			agMap.Add(21, 3);
			apMap.Add(21, 4);

			agMap.Add(22, 4);
			apMap.Add(22, 4);

			agMap.Add(23, 5);
			apMap.Add(23, 4);

			agMap.Add(24, 6);
			apMap.Add(24, 4);

			agMap.Add(25, 7);
			apMap.Add(25, 4);

			
			///
			/// Elements 26 - 30
			/// 
			agMap.Add(26, 8);
			apMap.Add(26, 4);

			agMap.Add(27, 9);
			apMap.Add(27, 4);

			agMap.Add(28, 10);
			apMap.Add(28, 4);

			agMap.Add(29, 11);
			apMap.Add(29, 4);

			agMap.Add(30, 12);
			apMap.Add(30, 4);
		}
		#endregion
	}
}
