using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw.DrawingTools
{
    class PaintBucketTool: SaveBitmapTool
    {

        public PaintBucketTool(string name) : base(name)
        {
        }

        public override void DrawStep(Point point)
        {
            Utils.FloodFill(initialBitmap, point, Color.White, Color.Aqua);
        }

        public override bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
