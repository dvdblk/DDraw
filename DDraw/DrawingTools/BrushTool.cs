using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw.DrawingTools
{
    class BrushTool : DrawTool
    {

        private Point previousPoint;

        public BrushTool(string name) : base(name)
        {
        }

        override public void BeginDrawing(Bitmap bmp, Point point)
        {
            previousPoint = point;
            base.BeginDrawing(bmp, point);
        }

        override public void DrawStep(Point point)
        {
            currentGraphics.DrawLine(new Pen(brush), previousPoint, point);
            previousPoint = point;
        }

        override public void EndDrawing(Point point)
        {
            if (point == previousPoint)
            {
                currentGraphics.FillRectangle(brush, point.X, point.Y, 1, 1);
                //currentGraphics.DrawEllipse(new Pen(brush), previousPoint.X - brushWidth / 2, previousPoint.Y-brushWidth/2, brushWidth, brushWidth);
            }
            base.EndDrawing(point);
        }

        override public bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
