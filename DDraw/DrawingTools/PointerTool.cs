using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw
{
    class PointerTool: DrawTool
    {
        public PointerTool(string name) : base(name)
        {
        }

        override public void DrawStep(Bitmap bmp, Point point)
        {

        }

        public override bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
