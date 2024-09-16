using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace ImageRecognitionSystem
{
    public partial class MainWindow : Window
    {
        [DllImport("CppImageProcessing.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProcessEdges(string inputPath, string outputPath);

        [DllImport("CppImageProcessing.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProcessObjectDetection(string inputPath, string outputPath);

        [DllImport("CppImageProcessing.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProcessEnhancement(string inputPath, string outputPath);

        private string inputImagePath;
        private string outputImagePath = "output.jpg";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

            if (dlg.ShowDialog() == true)
            {
                inputImagePath = dlg.FileName;
                imgOriginal.Source = new BitmapImage(new Uri(inputImagePath));
            }
        }

        private void btnProcessEdges_Click(object sender, RoutedEventArgs e)
        {
            ProcessEdges(inputImagePath, outputImagePath);
            DisplayProcessedImage();
        }

        private void btnProcessObjects_Click(object sender, RoutedEventArgs e)
        {
            ProcessObjectDetection(inputImagePath, outputImagePath);
            DisplayProcessedImage();
        }

        private void btnEnhance_Click(object sender, RoutedEventArgs e)
        {
            ProcessEnhancement(inputImagePath, outputImagePath);
            DisplayProcessedImage();
        }

        private void DisplayProcessedImage()
        {
            imgProcessed.Source = new BitmapImage(new Uri(Path.GetFullPath(outputImagePath)));
        }
    }
}
