using System;
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
using System.Windows.Shapes;

namespace DDraw
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewImageWindow : Window
    {

        public string ImageName { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public NewImageWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ImageName = "New Image";
            ImageWidth = 400;
            ImageHeight = 400;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var result = Validation.GetErrors(widthTextBox);
            if (Validation.GetErrors(widthTextBox).Count == 0 && Validation.GetErrors(heightTextBox).Count == 0 && Validation.GetErrors(nameTextBox).Count == 0) {
                ImageWidth = int.Parse(widthTextBox.Text);
                ImageHeight = int.Parse(heightTextBox.Text);
                ImageName = nameTextBox.Text;
                DialogResult = true;
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
