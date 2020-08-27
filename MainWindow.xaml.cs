using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QRCreator
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(txtToEncode.Text);

            var renderer = new GraphicsRenderer(new FixedModuleSize(40, QuietZoneModules.Two));
            var fileName = $"{Guid.NewGuid()}.png";
            using (var stream = new FileStream(fileName, FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);               

            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = File.OpenRead(fileName);
            imageSource.EndInit();            

            imageQR.Source = imageSource;

        }

        private void txtToEncode_GotFocus(object sender, RoutedEventArgs e)
        {
            txtToEncode.Text = string.Empty;
        }
    }
}
