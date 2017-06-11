using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw
{
    class LineTool: ShapeTool
    {
        public LineTool(string name) : base(name)
        {
        }

        public override void DrawStep(Bitmap bmp, Point point)
        {
            currentGraphics.DrawLine(pen, initialPoint.X, initialPoint.Y, point.X, point.Y);
        }

        public override bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
