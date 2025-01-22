using System.Windows;

namespace SeaBattle2._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameBattleShipVM tVM = new GameBattleShipVM();

        public MainWindow()
        {
            DataContext = tVM;
            InitializeComponent();
        }

        private void MouseKeyDownEnemy(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ;
        }

        private void MouseKeyDownPlayer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ;
        }
    }

    
}
