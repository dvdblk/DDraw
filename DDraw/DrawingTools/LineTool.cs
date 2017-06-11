using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw.DrawingTools
{
    class LineTool: ShapeTool
    {
        public LineTool(string name) : base(name)
        {
        }

        public override void DrawShape(int x, int y, int width, int height)
        {
            currentGraphics.DrawLine(new System.Drawing.Pen(brush), x, y, width, height);
        }

        public override bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
