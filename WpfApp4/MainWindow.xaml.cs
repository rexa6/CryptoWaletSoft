using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        private readonly TransferParser _parser = new();
        private int _totalAddresses = 0;
        private readonly HashSet<string> _uniqueAddresses = new();

        public MainWindow()
        {
            InitializeComponent();

            _parser.OnNewTransfers += Parser_OnNewTransfers;
            _parser.OnError += Parser_OnError;

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

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConsoleTextBox.Clear();
            WalletCountText.Text = "Всего адресов: 0";
            _totalAddresses = 0;
            _uniqueAddresses.Clear();

            _parser.StartParsing();
        }

        private void Parser_OnNewTransfers(List<Transfer> transfers)
        {
            Dispatcher.Invoke(() =>
            {
                foreach (var t in transfers)
                {
                    if (_uniqueAddresses.Add(t.transferFromAddress))
                    {
                        ConsoleTextBox.AppendText(t.transferFromAddress + Environment.NewLine);
                        _totalAddresses++;
                    }

                    if (_uniqueAddresses.Add(t.transferToAddress))
                    {
                        ConsoleTextBox.AppendText(t.transferToAddress + Environment.NewLine);
                        _totalAddresses++;
                    }
                }

                WalletCountText.Text = $"Всего уникальных адресов: {_totalAddresses}";
                ConsoleTextBox.ScrollToEnd();
            });
        }

        private void Parser_OnError(string error)
        {
            Dispatcher.Invoke(() =>
            {
                ConsoleTextBox.AppendText($"Ошибка: {error}{Environment.NewLine}");
            });
        }

        private void BtcButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BTC button clicked");
        }

        private void LtcButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("LTC button clicked");
        }

        private void TrxButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TRX button clicked");
        }
    }
}