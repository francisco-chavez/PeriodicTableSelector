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

		public double ChemicalElementWidth
		{
			get { return m_chemicalWidth; }
			set 
			{ 
				foreach (UIElement child in InternalChildren)
					if (child is ChemicalElement)
					{
						((ChemicalElement) child).Width = value;
					}
				m_chemicalWidth = value; 
			}
		}
		private double m_chemicalWidth = 60;

		public double ChemicalElementHeight
		{
			get { return m_chemicalHeight; }
			set
			{
				foreach (UIElement child in InternalChildren)
					if (child is ChemicalElement)
					{
						((ChemicalElement) child).Height = value;
					}

				m_chemicalHeight = value;
			}
		}
		private double m_chemicalHeight = 60;
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

			int maxGroup = 0;
			int maxPeriod = 0;
			foreach (UIElement child in InternalChildren)
			{
				child.Measure(constraint);

				if (child is ChemicalElement)
				{
					ChemicalElement chem = (ChemicalElement) child;
					maxGroup = Math.Max(maxGroup, AtomicNumberToGroupMap[chem.AtomicNumber]);
					maxPeriod = Math.Max(maxPeriod, AtomicNumberToPeriodMap[chem.AtomicNumber]);
					continue;
				}

				var desiredSize = child.DesiredSize;
				size.Width = Math.Max(size.Width, desiredSize.Width + Canvas.GetLeft(child));
				size.Height = Math.Max(size.Height, desiredSize.Height + Canvas.GetTop(child));
			}

			size.Width = Math.Max(size.Width, maxGroup * ChemicalElementWidth);
			size.Height = Math.Max(size.Height, maxPeriod * ChemicalElementHeight + ChemicalElementHeight * 2.5);

			var margin = this.Margin;
			size.Width += margin.Left + margin.Right;
			size.Height += margin.Top + margin.Bottom;

			return size;
		}

		protected override Size ArrangeOverride(Size arrangeSize)
		{
			List<ChemicalElement> rareEarths = new List<ChemicalElement>(28);
			double bottom = 0;
			Size elementSize = new Size(ChemicalElementWidth, ChemicalElementHeight);
			double extraTop = Margin.Top;
			double extraLeft = Margin.Left;

			foreach (UIElement child in InternalChildren)
			{
				if (child is ChemicalElement)
				{
					var chem = (ChemicalElement) child;

					int group = AtomicNumberToGroupMap[chem.AtomicNumber];
					if (group < 0)
					{
						rareEarths.Add(chem);
					}
					else
					{
						Rect r = new Rect(
							new Point(
								(group - 1) * ChemicalElementWidth + extraLeft,
								(AtomicNumberToPeriodMap[chem.AtomicNumber] - 1) * ChemicalElementHeight + extraTop),
							elementSize);

						bottom = Math.Max(bottom, r.Bottom);
						child.Arrange(r);
					}
				}	// End child portion
			}	// End loop

			bottom += elementSize.Height / 2;

			for (int i = 0; i < rareEarths.Count; i++)
			{
				Rect r = new Rect(
					new Point(
						i * elementSize.Width + extraLeft,
						bottom + extraTop),
					elementSize);

				rareEarths[i].Arrange(r);
			}

			rareEarths.Clear();
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


			///
			/// Elements 31 - 35
			/// 
			agMap.Add(31, 13);
			apMap.Add(31, 4);

			agMap.Add(32, 14);
			apMap.Add(32, 4);

			agMap.Add(33, 15);
			apMap.Add(33, 4);

			agMap.Add(34, 16);
			apMap.Add(34, 4);

			agMap.Add(35, 17);
			apMap.Add(35, 4);

			///
			/// Elements 36 - 40
			/// 
			AddMeta(36, 18, 4);
			AddMeta(37, 1, 5);
			AddMeta(38, 2, 5);
			AddMeta(39, 3, 5);
			AddMeta(40, 4, 5);

			///
			/// Elements 41 - 45
			/// 
			AddMeta(41, 5, 5);
			AddMeta(42, 6, 5);
			AddMeta(43, 7, 5);
			AddMeta(44, 8, 5);
			AddMeta(45, 9, 5);

			///
			/// Elements 46 - 50
			///
 			AddMeta(46, 10, 5);
			AddMeta(47, 11, 5);
			AddMeta(48, 12, 5);
			AddMeta(49, 13, 5);
			AddMeta(50, 14, 5);

			///
			/// Elements 51 - 55
			/// 
			AddMeta(51, 15, 5);
			AddMeta(52, 16, 5);
			AddMeta(53, 17, 5);
			AddMeta(54, 18, 5);
			AddMeta(55, 1, 6);

			///
			/// Elements 56 - 60
			/// 
			AddMeta(56, 2, 6);
			AddMeta(57, -1, 6);
			AddMeta(58, -1, 6);
			AddMeta(59, -1, 6);
			AddMeta(60, -1, 6);

			///
			/// Elements 61 - 65
			///
			AddMeta(61, -1, 6);
			AddMeta(62, -1, 6);
			AddMeta(63, -1, 6);
			AddMeta(64, -1, 6);
			AddMeta(65, -1, 6);

			///
			/// Elements 66 - 70
			/// 
			AddMeta(66, -1, 6);
			AddMeta(67, -1, 6);
			AddMeta(68, -1, 6);
			AddMeta(69, -1, 6);
			AddMeta(70, -1, 6);

			///
			/// Elements 71 - 75
			/// 
			AddMeta(71, 3, 6);
			AddMeta(72, 4, 6);
			AddMeta(73, 5, 6);
			AddMeta(74, 6, 6);
			AddMeta(75, 7, 6);

			///
			/// Elements 76, - 80
			/// 
			AddMeta(76, 8, 6);
			AddMeta(77, 9, 6);
			AddMeta(78, 10, 6);
			AddMeta(79, 11, 6);
			AddMeta(80, 12, 6);

		}

		private void AddMeta(int key, int group, int period)
		{
			AtomicNumberToGroupMap.Add(key, group);
			AtomicNumberToPeriodMap.Add(key, period);
		}
		#endregion
	}
}
