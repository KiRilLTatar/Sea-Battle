using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void MouseKeyDownEnemy(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Border border && border.Tag is CellVM cell)
            {
                int x = cell.X;
                int y = cell.Y;
                tVM.PlayerMove(new Point(x, y));
            }
        }

        private void MouseKeyDownPlayer(object sender, MouseButtonEventArgs e)
        {
            ;
        }
    }

    
}
