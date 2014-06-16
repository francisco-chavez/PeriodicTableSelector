using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;


namespace Unv.PeriodicTableSelectorLib
{
	public class StandardAdorner
		: Adorner
	{
		#region Attributes
		private Canvas m_drawArea;

		private TextBlock m_insertText1;
		private TextBlock m_insertText2;
		private TextBlock m_headerText1;
		private TextBlock m_headerText2;
		#endregion


		#region Properties
		protected override int VisualChildrenCount
		{
			get { return 1; }
		}

		public double ElementWidth
		{
			get { return m_elementWidth; }
			set
			{
				m_elementWidth = value;
				SetHorizontalLocations();
			}
		}
		private double m_elementWidth = 10;

		public double ElementHeight
		{
			get { return m_elementHeight; }
			set
			{
				m_elementHeight = value;
				SetVerticalLocations();
			}
		}
		private double m_elementHeight = 10;
		#endregion


		#region Constructors
		public StandardAdorner(UIElement adornedElement)
			: base(adornedElement)
		{
			double fontSize = 18;
			m_insertText1 = new TextBlock()
			{
				Text = "*",
				FontWeight = FontWeights.Bold,
				FontSize = fontSize,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			m_insertText2 = new TextBlock()
			{
				Text = "**",
				FontWeight = FontWeights.Bold,
				FontSize = fontSize,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			m_headerText1 = new TextBlock()
			{
				Text = "*",
				FontWeight = FontWeights.Bold,
				FontSize = fontSize,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			m_headerText2 = new TextBlock()
			{
				Text = "**",
				FontWeight = FontWeights.Bold,
				FontSize = fontSize,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};


			m_drawArea = new Canvas();
			m_drawArea.Children.Add(m_headerText1);
			m_drawArea.Children.Add(m_headerText2);
			m_drawArea.Children.Add(m_insertText1);
			m_drawArea.Children.Add(m_insertText2);

			this.AddLogicalChild(m_drawArea);
			this.AddVisualChild(m_drawArea);
			SetHorizontalLocations();
			SetVerticalLocations();
		}
		#endregion


		#region Methods
		private void SetHorizontalLocations()
		{
			double insertPosition = ElementWidth * 2.5;
			Canvas.SetLeft(m_insertText1, insertPosition);
			Canvas.SetLeft(m_insertText2, insertPosition);

			double headerPosition = (ElementWidth * (18 - 15) / 2) - (ElementHeight / 2);
			Canvas.SetLeft(m_headerText1, headerPosition);
			Canvas.SetLeft(m_headerText2, headerPosition);
		}

		private void SetVerticalLocations()
		{
			double insertPositions = ElementHeight * 5.5;
			Canvas.SetTop(m_insertText1, insertPositions);
			Canvas.SetTop(m_insertText2, insertPositions + ElementHeight);

			double headerPositions = ElementHeight * 8;
			Canvas.SetTop(m_headerText1, headerPositions);
			Canvas.SetTop(m_headerText2, headerPositions + ElementHeight);
		}

		protected override Visual GetVisualChild(int index)
		{
			return m_drawArea;
		}

		protected override Size MeasureOverride(Size constraint)
		{
			m_drawArea.Measure(constraint);
			return constraint;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			m_drawArea.Arrange(new Rect(finalSize));
			return finalSize;
		}
		#endregion
	}
}
