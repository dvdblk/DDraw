using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw.DrawingTools
{
    class EllipseTool: ShapeTool
    {
        public EllipseTool(string name) : base(name)
        {
        }

        public override void DrawShape(int x, int y, int width, int height)
        {
            currentGraphics.DrawEllipse(new Pen(brush), new Rectangle(x, y, width, height));
            currentGraphics.FillEllipse(brush, new Rectangle(x, y, width, height));
        }

        public override bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
