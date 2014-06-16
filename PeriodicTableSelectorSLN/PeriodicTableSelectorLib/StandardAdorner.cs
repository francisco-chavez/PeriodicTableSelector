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
			}
		}
		private double m_elementWidth = 10;

		public double ElementHeight
		{
			get { return m_elementHeight; }
			set
			{
				m_elementHeight = value;
			}
		}
		private double m_elementHeight = 10;
		#endregion


		#region Constructors
		public StandardAdorner(UIElement adornedElement)
			: base(adornedElement)
		{
			m_insertText1 = new TextBlock()
			{
				Text = "*",
				FontWeight = FontWeights.Bold,
				FontSize = 14,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			m_insertText2 = new TextBlock()
			{
				Text = "**",
				FontWeight = FontWeights.Bold,
				FontSize = 14,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			m_headerText1 = new TextBlock()
			{
				Text = "*",
				FontWeight = FontWeights.Bold,
				FontSize = 14,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			m_headerText2 = new TextBlock()
			{
				Text = "**",
				FontWeight = FontWeights.Bold,
				FontSize = 14,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};


			m_drawArea = new Canvas();
			m_drawArea.Children.Add(m_headerText1);
			m_drawArea.Children.Add(m_headerText2);
			m_drawArea.Children.Add(m_insertText1);
			m_drawArea.Children.Add(m_insertText2);


			this.AddVisualChild(m_drawArea);
		}
		#endregion


		#region Methods
		private void SetHorizontalLocations()
		{
			Canvas.SetLeft(m_insertText1, 50);
			Canvas.SetLeft(m_insertText2, 50);

			Canvas.SetLeft(m_headerText1, 25);
			Canvas.SetLeft(m_headerText2, 25);
		}

		protected override Visual GetVisualChild(int index)
		{
			return m_drawArea;
		}

		protected override Size MeasureOverride(Size constraint)
		{
			m_drawArea.Measure(constraint);
			return m_drawArea.DesiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			m_drawArea.Arrange(new Rect(finalSize));
			return finalSize;
		}
		#endregion
	}
}
