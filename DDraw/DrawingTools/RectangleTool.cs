using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw
{
    class RectangleTool: ShapeTool
    {
        public RectangleTool(string name) : base(name)
        {
        }

        override public void DrawShape(int x, int y, int width, int height)
        {
            //currentGraphics.DrawRectangle(pen, new Rectangle(x, y, width, height));
            //currentGraphics.FillRectangle(brush, new Rectangle(x, y, width, height));
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            currentGraphics.DrawRoundedRectangle(pen, new Rectangle(x, y, width, height), 2);
        }

        public override bool RequiresAdditionalSettings()
        {
            return true;
        }
    }
}
