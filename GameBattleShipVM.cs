using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace SeaBattle2._0
{
    class GameBattleShipVM : BaseViewModel
    { 
        private TimerModel timer;
        private string time;
        public Point point;

        public string Timer
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }
        
        public PlayerGridVM PlayerCells { get; }
        public PlayerGridVM EnemyCells { get; }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        public GameBattleShipVM()
        {

            timer = new TimerModel();
            timer.TimeElapsed += (s, e) => Timer = timer.Timer;

            StartCommand = new RelayCommand(o => timer.Start());
            StopCommand = new RelayCommand(o => timer.Stop());

            PlayerGrid playerGrid = new PlayerGrid(10);
            PlayerGrid enemyGrid = new PlayerGrid(10);
            playerGrid.GenerationShip();
            enemyGrid.GenerationShip();

            PlayerCells = new PlayerGridVM(playerGrid);
            EnemyCells = new PlayerGridVM(enemyGrid);


        }
        
    }


}
