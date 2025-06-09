using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GeneratorToggle.Checked += (s, e) =>
            {
                GeneratorSubmenu.Visibility = Visibility.Visible;
                GeneratorToggle.Content = "Brute Force ▲";
            };

            GeneratorToggle.Unchecked += (s, e) =>
            {
                GeneratorSubmenu.Visibility = Visibility.Collapsed;
                GeneratorToggle.Content = "Brute Force ▼";
            };
        }

        private void ParserButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ParserView(); // ← вставляется прямо в MainWindow
        }


        private void BtcButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "Генерация BTC кошелька", FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
        }

        private void LtcButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "Генерация LTC кошелька", FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
        }

        private void TrxButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "Генерация TRX кошелька", FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
        }
    }
}