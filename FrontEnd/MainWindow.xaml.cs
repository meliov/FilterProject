

using System.Windows;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListContext ListContext = new ListContext();
        public MainWindow()
        {
            InitializeComponent(); 
            DataContext = ListContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
    
}