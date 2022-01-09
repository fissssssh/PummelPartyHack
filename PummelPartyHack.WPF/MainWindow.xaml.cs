using System.Windows;
using System.Windows.Controls;

namespace PummelPartyHack.WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly MainWindowViewModel ViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
