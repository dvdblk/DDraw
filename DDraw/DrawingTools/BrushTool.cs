using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw
{
    public class BrushTool : DrawTool
    {

        private Point previousPoint;

        public BrushTool(string name) : base(name)
        {
        }

        override public void BeginDrawing(Bitmap bmp, Point point)
        {
            previousPoint = point;
            base.BeginDrawing(bmp, point);
            pen.EndCap = LineCap.Round;
        }

        override public void DrawStep(Bitmap bmp, Point point)
        {

            currentGraphics.DrawLine(pen, previousPoint, point);
            previousPoint = point;
        }

        override public void EndDrawing(Point point)
        {
            if (point == previousPoint)
            {
                currentGraphics.FillRectangle(brush, point.X, point.Y, 1, 1);
            }
            base.EndDrawing(point);
        }

        override public bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
