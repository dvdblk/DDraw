using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw
{
    class PaintBucketTool: SaveBitmapTool
    {

        public PaintBucketTool(string name) : base(name)
        {
        }

        public override void DrawStep(Bitmap bmp, Point point)
        {
            Utils.FloodFill(bmp, point, Color.White, fillColor);
        }

        public override bool RequiresAdditionalSettings()
        {
            return false;
        }
    }
}
