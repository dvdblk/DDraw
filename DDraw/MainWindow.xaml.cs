using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DDraw.DrawingTools;
using System.Collections.Specialized;

namespace DDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private ObservableCollection<Layer> layers = new ObservableCollection<Layer>();
        private List<DrawingTool> tools = new List<DrawingTool>();
        private Bitmap mainBitmap;
        private Bitmap temporaryBitmap;
        private Graphics mainBitmapGraphics;
        private bool Drawing;

        private double _mainImageZoomValue = 1.0;

        internal DrawTool SelectedTool
        {
            get { return ((DrawTool)toolsList.SelectedItem); }
        }

        internal Layer SelectedLayer
        {
            get { return ((Layer)layersList.SelectedItem); }
        }

        private bool _imageCreated;
        public Boolean ImageCreated
        {
            get { return _imageCreated; }
            set
            {
                _imageCreated = value;
                OnPropertyChanged("ImageCreated");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            addLayerBtn.DataContext = this;
            ImageCreated = false;
            layersList.ItemsSource = layers;
            layers.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    //layersList.items;
                }
            };
            toolsList.ItemsSource = tools;

            tools.Add(new PointerTool("/Resources/pointerToolImage.png"));
            tools.Add(new BrushTool("/Resources/brushToolImage.png"));
            tools.Add(new RectangleTool(""));
            tools.Add(new EllipseTool(""));
            tools.Add(new LineTool(""));
            tools.Add(new PaintBucketTool(""));
            toolsList.SelectedItem = tools[0];
        }

        private void BitmapFromLayers()
        {
            mainBitmapGraphics.Clear(System.Drawing.Color.White);
            for (int x = layers.Count - 1; x >= 0; x--)
            {
                if (layers[x].Visible)
                {
                    mainBitmapGraphics.DrawImage((layers[x].Bitmap), 0, 0);
                }
            }
        }

        private void Draw()
        {
            BitmapFromLayers();
            mainImage.Source = Utils.loadBitmap(mainBitmap);
        }


        private void CreateNewImage(int w, int h)
        {
            
            mainImage.Width = w;
            mainImage.Height = h;
            mainBitmap = new Bitmap(w, h);
            mainBitmapGraphics = Graphics.FromImage(mainBitmap);
            layers.RemoveAll(x => true);
            ImageCreated = true;
            addLayer();
            Draw();
        }


        private void addLayer(string name = null)
        {
            var actualName = "Layer " + layers.Count;
            if (name != null)
            {
                actualName = name;
            }
            //layersList.Items.Insert(0, new Layer(actualName, (int)mainImage.Width, (int)mainImage.Height));
            layers.Insert(0, new Layer(actualName, (int)mainImage.Width, (int)mainImage.Height));
            layersList.SelectedItem = layers[0];
        }

        private void addLayerBtn_Click(object sender, RoutedEventArgs e)
        {
            addLayer();
        }

        private void mainImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                _mainImageZoomValue += 0.5;
            }
            else
            {
                _mainImageZoomValue -= 0.5;
                if (_mainImageZoomValue <= 0) {
                    _mainImageZoomValue = 0.5;
                }
            }

            ScaleTransform scale = new ScaleTransform(_mainImageZoomValue, _mainImageZoomValue);
            mainImage.LayoutTransform = scale;
            e.Handled = true;
        }

        private void menuItemNew_Click(object sender, RoutedEventArgs e)
        {
            /*var newImageWindow = new NewImageWindow();
            newImageWindow.ShowDialog();
            if (newImageWindow.DialogResult.HasValue && newImageWindow.DialogResult.Value)
            {*/
                CreateNewImage(400, 200);
            //}

            
        }

        private void layerVisibleBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Layer layer = button.DataContext as Layer;
            layer.Visible = !layer.Visible;
            Draw();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // MOUSE

        private System.Drawing.Point GetPoint(MouseEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(mainImage);
            int x = (int)p.X;
            int y = (int)p.Y;
            return new System.Drawing.Point(x, y);
        }


        private void mainImage_MouseMove(object sender, MouseEventArgs e)
        {
            var point = GetPoint(e);
            mainImageCursorPosition.Text = "Pos: " + point.X.ToString() + ", " + point.Y + "px";

            if (Drawing)
            {
                if (SelectedTool is SaveBitmapTool)
                {
                    SelectedLayer.Bitmap.Dispose();
                    SelectedLayer.Bitmap = new Bitmap(temporaryBitmap);
                    SelectedTool.BeginDrawing(SelectedLayer.Bitmap, SelectedTool.initialPoint);
                }
                SelectedTool.DrawStep(point);
                Draw();
                SelectedLayer.BitmapChanged();
            }

        }

        private void mainImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Drawing = true;
            if (SelectedTool is SaveBitmapTool)
            {
                if (temporaryBitmap != null)
                {
                    temporaryBitmap.Dispose();
                    temporaryBitmap = null;
                }
                temporaryBitmap = new Bitmap(SelectedLayer.Bitmap);
            }
            SelectedTool.BeginDrawing(SelectedLayer.Bitmap, GetPoint(e));
        }

        private void mainImage_MouseUp(object sender, MouseButtonEventArgs e)
        {

            Drawing = false;
            SelectedTool.EndDrawing(GetPoint(e));
            Draw();
        }
    }
}
