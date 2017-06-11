using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace DDraw
{
    public interface DrawingTool : INotifyPropertyChanged
    {
        void BeginDrawing(Bitmap bmp, Point point);
        void DrawStep(Bitmap bmp, Point point);
        void EndDrawing(Point point);
        bool RequiresAdditionalSettings();
        void Cleanup();
    }

   public  abstract class DrawTool : DrawingTool
    {
        protected Graphics currentGraphics;
        protected Pen pen;
        protected Brush brush;
        public Point initialPoint;
        protected int brushWidth;
        public int PenWidth {
            get { return brushWidth; }
            set
            {
                brushWidth = value;
                OnPropertyChanged("PenWidth");
                OnPropertyChanged("PenWidthBitmap");
            }
        }
        public string ToolImageName { get; set; }

        private Color _strokeColor;
        public Color strokeColor
        {
            get { return _strokeColor; }
            set
            {
                _strokeColor = value;
                OnPropertyChanged("StrokeColorBitmap");
            }
        }
        private Color _fillColor;
        public Color fillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                OnPropertyChanged("FillColorBitmap");
            }
        }

        private Bitmap _strokeColorBitmap;
        public BitmapSource StrokeColorBitmap
        {
            get
            {
                Graphics.FromImage(_strokeColorBitmap).Clear(strokeColor);
                return Utils.loadBitmap(_strokeColorBitmap);
            }
            set { }
        }

        private Bitmap _fillColorBitmap;
        public BitmapSource FillColorBitmap
        {
            get
            {
                Graphics.FromImage(_fillColorBitmap).Clear(_fillColor);
                return Utils.loadBitmap(_fillColorBitmap);
            }
            set { }
        }

        private Bitmap _penWidthBitmap;
        public BitmapSource PenWidthBitmap
        {
            get
            {
                using (var g = Graphics.FromImage(_penWidthBitmap))
                {
                    g.Clear(Color.Transparent);
                    g.DrawLine(new Pen(Color.Black, PenWidth), 0, 50, 100, 50);
                }
                return Utils.loadBitmap(_penWidthBitmap);
            }
            set { }
        }

        public DrawTool(string name)
        {
            this.ToolImageName = name;
            this.strokeColor = Color.Black;
            this.fillColor = Color.White;
            this._strokeColorBitmap = new Bitmap(100, 100);
            this._fillColorBitmap = new Bitmap(100, 100);
            this._penWidthBitmap = new Bitmap(100, 100);
            brush = new SolidBrush(Color.Black);
            PenWidth = 2;
        }

        public virtual void BeginDrawing(Bitmap bmp, Point point)
        {
            pen = new Pen(strokeColor, PenWidth);
            brush = new SolidBrush(fillColor);
            currentGraphics = Graphics.FromImage(bmp);
            initialPoint = point;
            DrawStep(bmp, point);
        }

        public abstract void DrawStep(Bitmap bmp, Point point);

        public virtual void EndDrawing(Point point)
        {
            if (currentGraphics != null)
            {
                currentGraphics.Dispose();
                currentGraphics = null;
            }
            if (pen != null)
            {
                pen.Dispose();
                pen = null;
            }
            if (brush != null)
            {
                brush.Dispose();
                brush = null;
            }
        }

        public void Cleanup()
        {
            pen?.Dispose();
            brush?.Dispose();
            currentGraphics?.Dispose();
            _penWidthBitmap?.Dispose();
            _fillColorBitmap?.Dispose();
        }

        public abstract bool RequiresAdditionalSettings();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var propChanged = PropertyChanged;
            if (propChanged != null)
            {
                propChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
