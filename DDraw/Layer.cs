using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DDraw
{
    class Layer : INotifyPropertyChanged
    {
        public Bitmap Bitmap { get; set; }
        public BitmapSource ImageBitmap
        {
            get
            {
                double aspectRatio = (double)Bitmap.Size.Width / (double)Bitmap.Size.Height;
                Bitmap resized;
                if (Bitmap.Size.Width > Bitmap.Size.Height)
                {
                    resized = new Bitmap(Bitmap, new Size((int)(100 * aspectRatio), 100));
                }
                else
                {
                    resized = new Bitmap(Bitmap, new Size(100, (int)(100 * aspectRatio)));
                }
                return Utils.loadBitmap(resized); 
            }
            set { }
        }
        public string Name { get; set; }
        private bool _locked;
        public bool Locked {
            get { return _locked; }
            set
            {
                _locked = value;
                OnPropertyChanged("Locked");
            }
        }
        private bool _visible;
        public bool Visible {
            get { return _visible; }
            set {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }

        public Layer(String name, int w, int h)
        {
            Locked = false;
            Visible = true;
            this.Name = name;
            Bitmap = new Bitmap(w, h);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var propChanged = PropertyChanged;
            if (propChanged != null)
            {
                propChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void BitmapChanged()
        {
            OnPropertyChanged("ImageBitmap");
        }

    }
}
