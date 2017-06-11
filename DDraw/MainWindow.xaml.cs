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
        private string imageName;
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
            toolsList.ItemsSource = tools;

            tools.Add(new PointerTool("/Resources/pointerToolImage.png"));
            tools.Add(new BrushTool("/Resources/brushToolImage.png"));
            tools.Add(new PaintBucketTool("/Resources/paintBucketToolImage.png"));
            tools.Add(new LineTool("/Resources/lineToolImage.png"));
            tools.Add(new RectangleTool("/Resources/rectangleToolImage.png"));
            tools.Add(new EllipseTool("/Resources/ellipseToolImage.png"));
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
            SelectedLayer.BitmapChanged();
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
            var newImageWindow = new NewImageWindow();
            newImageWindow.ShowDialog();
            if (newImageWindow.DialogResult.HasValue && newImageWindow.DialogResult.Value)
            {
                imageName = newImageWindow.ImageName;
                CreateNewImage(newImageWindow.ImageWidth, newImageWindow.ImageHeight);
            }

            
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

            if (SelectedLayer != null && Drawing && !SelectedLayer.Locked)
            {
                if (SelectedTool is SaveBitmapTool)
                {
                    SelectedLayer.Bitmap.Dispose();
                    SelectedLayer.Bitmap = new Bitmap(temporaryBitmap);
                    SelectedTool.BeginDrawing(SelectedLayer.Bitmap, SelectedTool.initialPoint);
                }
                SelectedTool.DrawStep(SelectedLayer.Bitmap, point);
                Draw();
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
            if (SelectedLayer != null)
            {
                SelectedTool.BeginDrawing(SelectedLayer.Bitmap, GetPoint(e));
            }
        }

        private void mainImage_MouseUp(object sender, MouseButtonEventArgs e)
        {

            Drawing = false;
            SelectedTool.EndDrawing(GetPoint(e));
            Draw();
        }

        private void menuItemSave_Click(object sender, RoutedEventArgs e)
        {
            if (ImageCreated)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = imageName ?? "image";
                dlg.DefaultExt = ".png";
                dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;
                    mainBitmap.Save(filename);
                }
            }
            
        }

        private void stokeColorBtn_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Drawing.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                SelectedTool.strokeColor = wpfColor;
            }
        }

        private void fillColorBtn_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Drawing.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                SelectedTool.fillColor = wpfColor;
            }
        }

        private void toolsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            additionalSettingsGrid.DataContext = SelectedTool;
            lineWidthRow.Height = new GridLength(40);
            strokeColorRow.Height = new GridLength(40);
            fillColorRow.Height = new GridLength(40);
            if (SelectedTool is BrushTool || SelectedTool is LineTool)
            {
                fillColorRow.Height = new GridLength(0);
            }
            else if (SelectedTool is EllipseTool)
            {
                roundedCornersRow.Height = new GridLength(0);
            }
            else if (SelectedTool is PaintBucketTool) 
            {
                lineWidthRow.Height = new GridLength(0);
                strokeColorRow.Height = new GridLength(0);
            }
            else if (!SelectedTool.RequiresAdditionalSettings())
            {
                lineWidthRow.Height = new GridLength(0);
                strokeColorRow.Height = new GridLength(0);
                fillColorRow.Height = new GridLength(0);
            }
        }

        private void mainImage_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Drawing)
            {
                Drawing = false;
                SelectedTool.EndDrawing(GetPoint(e));
                Draw();
            }
            this.Cursor = Cursors.Arrow;
        }

        private void layersListItemRemove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLayer != null && layers.Count != 1)
            {
                int nextSelected = layersList.SelectedIndex;
                if (nextSelected == layers.Count - 1)
                {
                    nextSelected--;
                }
                layers.Remove(SelectedLayer);
                layersList.SelectedItem = layers[nextSelected];
                Draw();
            }
        }

        private void layersListItemMoveUp_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLayer != null && layers.Count != 1 && layersList.SelectedIndex != 0)
            {
                int nextSelected = layersList.SelectedIndex;
                var tmp = layers[nextSelected];
                layers[nextSelected] = layers[nextSelected - 1];
                layers[nextSelected - 1] = tmp;
                layersList.SelectedItem = layers[nextSelected - 1];
                Draw();
            }
        }

        private void layersListItemMoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLayer != null && layers.Count != 1 && layersList.SelectedIndex != layers.Count-1)
            {
                int nextSelected = layersList.SelectedIndex;
                var tmp = layers[nextSelected];
                layers[nextSelected] = layers[nextSelected + 1];
                layers[nextSelected + 1] = tmp;
                layersList.SelectedItem = layers[nextSelected + 1];
                Draw();
            }
        }

        private void layersListItemMoveTop_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLayer != null && layers.Count != 1)
            {
                var tmp = SelectedLayer;
                layers.Remove(SelectedLayer);
                layers.Insert(0, tmp);
                layersList.SelectedItem = layers[0];
                Draw();
            }
        }

        private void layersListItemMoveBottom_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLayer != null && layers.Count != 1)
            {
                var tmp = SelectedLayer;
                layers.Remove(SelectedLayer);
                layers.Insert(layers.Count, tmp);
                layersList.SelectedItem = layers[layers.Count-1];
                Draw();
            }
        }

        private void layerLockedBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Layer layer = button.DataContext as Layer;
            layer.Locked = !layer.Locked;
        }

        private void menuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new System.Windows.Forms.OpenFileDialog();
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    CreateNewImage(500, 500);
                    Bitmap bitmap = new Bitmap(openDialog.FileName);
                    using (var g = Graphics.FromImage(SelectedLayer.Bitmap))
                    {
                        g.DrawImage(bitmap, 0, 0);
                    }
                    Draw();
                }
                catch (Exception)
                {
                    MessageBoxResult result = MessageBox.Show("Invalid file format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void Cleanup(object sender, CancelEventArgs e)
        {
            if (mainBitmap != null)
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    layers[i].Bitmap.Dispose();
                }
                temporaryBitmap?.Dispose();
                mainBitmap.Dispose();

                for (int i = 0; i < tools.Count; i++)
                {
                    tools[i].Cleanup();
                }
            }
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mainImage_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
        }
    }
}
