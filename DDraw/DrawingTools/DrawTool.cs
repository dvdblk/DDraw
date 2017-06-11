using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace DDraw.DrawingTools
{
    public interface DrawingTool
    {
        void BeginDrawing(Bitmap bmp, Point point);
        void DrawStep(Point point);
        void EndDrawing(Point point);
        bool RequiresAdditionalSettings();
    }

    abstract class DrawTool : DrawingTool
    {
        protected Graphics currentGraphics;
        protected Brush brush;
        public Point initialPoint;
        protected int brushWidth = 2;
        public string ToolImageName { get; set; }

        public DrawTool(string name)
        {
            this.ToolImageName = name;
            brush = new SolidBrush(Color.Black);
        }

        public virtual void BeginDrawing(Bitmap bmp, Point point)
        {
            currentGraphics = Graphics.FromImage(bmp);
            initialPoint = point;
            DrawStep(point);
        }

        public abstract void DrawStep(Point point);

        public virtual void EndDrawing(Point point)
        {
            if (currentGraphics != null)
            {
                currentGraphics.Dispose();
                currentGraphics = null;
            }
        }

        public abstract bool RequiresAdditionalSettings();
    }
}
