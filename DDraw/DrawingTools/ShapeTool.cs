using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw
{
    abstract class ShapeTool: SaveBitmapTool
    {

        public ShapeTool(string name) : base(name)
        {
        }

        public override void DrawStep(Bitmap bmp, Point point)
        {
            var w = Math.Abs(initialPoint.X - point.X);
            var h = Math.Abs(initialPoint.Y - point.Y);
            int y;
            int x;
            if (point.Y < initialPoint.Y)
            {
                var dY = initialPoint.Y - point.Y;
                y = initialPoint.Y - dY;
            }
            else
            {
                y = initialPoint.Y;
            }
            if (point.X < initialPoint.X)
            {
                x = point.X;
            }
            else
            {
                x = initialPoint.X;

            }
            DrawShape(x, y, w, h);
        }

        public virtual void DrawShape(int x, int y, int width, int height)
        {

        }

    }
}
