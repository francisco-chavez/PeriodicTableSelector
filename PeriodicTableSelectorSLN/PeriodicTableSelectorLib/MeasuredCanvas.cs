using System;
using System.Windows;
using System.Windows.Controls;


namespace Unv.PeriodicTableSelectorLib
{
	internal class MeasuredCanvas
		: Canvas
	{
		protected override Size MeasureOverride(Size constraint)
		{
			Size size = new Size();

			foreach (UIElement element in InternalChildren)
			{
				double left = Canvas.GetLeft(element);
				double top  = Canvas.GetTop(element);

				left = double.IsNaN(left) ? 0 : left;
				top  = double.IsNaN(top) ? 0 : top;

				element.Measure(constraint);

				Size desiredSize = element.DesiredSize;

				if (!double.IsNaN(desiredSize.Width))
					size.Width = Math.Max(size.Width, left + desiredSize.Width);
				if (!double.IsNaN(desiredSize.Height))
					size.Height = Math.Max(size.Height, left + desiredSize.Height);
			}

			Thickness margin = this.Margin;
			if (margin != null)
			{
				size.Width += margin.Left + margin.Right;
				size.Height += margin.Top + margin.Bottom;
			}

			if (!double.IsNaN(MinWidth) && size.Width < MinWidth)
				size.Width = MinWidth;
			if (!double.IsNaN(MinHeight) && size.Height < MinHeight)
				size.Height = MinHeight;

			return size;
		}
	}
}
