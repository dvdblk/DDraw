using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw.DrawingTools
{
    class RectangleTool: ShapeTool
    {
        public RectangleTool(string name) : base(name)
        {
        }

        override public void DrawShape(int x, int y, int width, int height)
        {
            currentGraphics.DrawRectangle(new Pen(brush), new Rectangle(x, y, width, height));
        }

        public override bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
