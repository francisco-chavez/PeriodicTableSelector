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
			/// Elements 11 - 20
			/// 
			AddMeta(11, 1, 3);
			AddMeta(12, 2, 3);
			AddMeta(13, 13, 3);
			AddMeta(14, 14, 3);
			AddMeta(15, 15, 3);

			AddMeta(16, 16, 3);
			AddMeta(17, 17, 3);
			AddMeta(18, 18, 3);
			AddMeta(19, 1, 4);
			AddMeta(20, 2, 4);
		
			///
			/// Elements 21 - 30
			/// 
			AddMeta(21, 3, 4);
			AddMeta(22, 4, 4);
			AddMeta(23, 5, 4);
			AddMeta(24, 6, 4);
			AddMeta(25, 7, 4);

			AddMeta(26, 8, 4);
			AddMeta(27, 9, 4);
			AddMeta(28, 10, 4);
			AddMeta(29, 11, 4);
			AddMeta(30, 12, 4);

			///
			/// Elements 31 - 40
			/// 
			AddMeta(31, 13, 4);
			AddMeta(32, 14, 4);
			AddMeta(33, 15, 4);
			AddMeta(34, 16, 4);
			AddMeta(35, 17, 4);
 
			AddMeta(36, 18, 4);
			AddMeta(37, 1, 5);
			AddMeta(38, 2, 5);
			AddMeta(39, 3, 5);
			AddMeta(40, 4, 5);

			///
			/// Elements 41 - 50
			/// 
			AddMeta(41, 5, 5);
			AddMeta(42, 6, 5);
			AddMeta(43, 7, 5);
			AddMeta(44, 8, 5);
			AddMeta(45, 9, 5);

 			AddMeta(46, 10, 5);
			AddMeta(47, 11, 5);
			AddMeta(48, 12, 5);
			AddMeta(49, 13, 5);
			AddMeta(50, 14, 5);

			///
			/// Elements 51 - 60
			/// 
			AddMeta(51, 15, 5);
			AddMeta(52, 16, 5);
			AddMeta(53, 17, 5);
			AddMeta(54, 18, 5);
			AddMeta(55, 1, 6);

			AddMeta(56, 2, 6);
			AddMeta(57, -1, 6);
			AddMeta(58, -1, 6);
			AddMeta(59, -1, 6);
			AddMeta(60, -1, 6);

			///
			/// Elements 61 - 70
			///
			AddMeta(61, -1, 6);
			AddMeta(62, -1, 6);
			AddMeta(63, -1, 6);
			AddMeta(64, -1, 6);
			AddMeta(65, -1, 6);

			AddMeta(66, -1, 6);
			AddMeta(67, -1, 6);
			AddMeta(68, -1, 6);
			AddMeta(69, -1, 6);
			AddMeta(70, -1, 6);

			///
			/// Elements 71 - 80
			/// 
			AddMeta(71, 3, 6);
			AddMeta(72, 4, 6);
			AddMeta(73, 5, 6);
			AddMeta(74, 6, 6);
			AddMeta(75, 7, 6);

			AddMeta(76, 8, 6);
			AddMeta(77, 9, 6);
			AddMeta(78, 10, 6);
			AddMeta(79, 11, 6);
			AddMeta(80, 12, 6);

			///
			/// Elements 81 - 90
			/// 
			AddMeta(81, 13, 6);
			AddMeta(82, 14, 6);
			AddMeta(83, 15, 6);
			AddMeta(84, 16, 6);
			AddMeta(85, 17, 6);

			AddMeta(86, 18, 6);
			AddMeta(87, 1, 7);
			AddMeta(88, 2, 7);
			AddMeta(89, -1, 7);
			AddMeta(90, -1, 7);

			///
			/// Elements 91 - 100
			/// 
			AddMeta(91, -1, 7);
			AddMeta(92, -1, 7);
			AddMeta(93, -1, 7);
			AddMeta(94, -1, 7);
			AddMeta(95, -1, 7);

			AddMeta(96, -1, 7);
			AddMeta(97, -1, 7);
			AddMeta(98, -1, 7);
			AddMeta(99, -1, 7);
			AddMeta(100, -1, 7);

			///
			/// Elements 101 - 110
			/// 
			AddMeta(101, -1, 7);
			AddMeta(102, -1, 7);
			AddMeta(103, 3, 7);
			AddMeta(104, 4, 7);
			AddMeta(105, 5, 7);

			AddMeta(106, 6, 7);
			AddMeta(107, 7, 7);
			AddMeta(108, 8, 7);
			AddMeta(109, 9, 7);
			AddMeta(110, 10, 7);
		}

		/// <summary>
		/// This method takes in an atomic number of a chemical element as a key,
		/// and places the group and period meta data for that element under said
		/// key.
		/// </summary>
		private void AddMeta(int key, int group, int period)
		{
			AtomicNumberToGroupMap.Add(key, group);
			AtomicNumberToPeriodMap.Add(key, period);
		}
		#endregion
	}
}
