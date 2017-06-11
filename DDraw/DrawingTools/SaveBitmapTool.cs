using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDraw
{
    abstract class SaveBitmapTool: DrawTool
    {
        protected Bitmap initialBitmap;

        public SaveBitmapTool(string name) : base(name)
        {
        }

        public override void BeginDrawing(Bitmap bmp, Point point)
        {
            //initialBitmap = new Bitmap(bmp);
            base.BeginDrawing(bmp, point);
        }

        public override void EndDrawing(Point point)
        {
            //initialBitmap.Dispose();
            //initialBitmap = null;
            base.EndDrawing(point);
        }
    }
}
