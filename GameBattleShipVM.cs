using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SeaBattle2._0
{
    class GameBattleShipVM : BaseViewModel
    { 
        private TimerModel timer;
        private string time;

        private string scorePlayer;
        private string enemyPlayer;
        public string ScorePlayer
        {
            get => scorePlayer;
            set
            {
                scorePlayer = value;
                OnPropertyChanged();
            }
        }

        public string ElementScore
        {
            get => enemyPlayer;
            set
            {
                enemyPlayer = value;
                OnPropertyChanged();
            }
        }

        private bool isPlayerTurn = true;
        private bool gameOver = false;

        public bool IsPlayerTurn
        {
            get => isPlayerTurn;
            set
            {
                isPlayerTurn = value;
                OnPropertyChanged();
            }
        }

        public bool GameOver
        {
            get => gameOver;
            set
            {
                gameOver = value;
                OnPropertyChanged();
            }
        }

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
            scorePlayer = $"Player: 0";
            enemyPlayer = $"Computer: 0";

            timer = new TimerModel();
            timer.TimeElapsed += (s, e) => Timer = timer.Timer;

            StartCommand = new RelayCommand(o => timer.Start());
            StopCommand = new RelayCommand(o => timer.Stop());

            PlayerGrid playerGrid = new PlayerGrid(10);
            PlayerGrid enemyGrid = new PlayerGrid(10);
            playerGrid.GenerationShip();
            enemyGrid.GenerationShip();

            PlayerCells = new PlayerGridVM(playerGrid, false);
            EnemyCells = new PlayerGridVM(enemyGrid, true);
        }

        public void PlayerMove(Point target)
        {
            if (!timer.IsRunningTimer())
            {
                return;
            }
            else
            {
                if (!IsPlayerTurn || GameOver)
                    return;

                (bool, bool) chun = EnemyCells.Attack((int)target.X, (int)target.Y);

                if (!chun.Item1)
                {
                    return;
                }
                if (chun.Item2) {
                    int count = EnemyCells.CntSunkShip();
                    ScorePlayer = $"Player: {(count).ToString()}";
                }



                if (CheckVictory(EnemyCells))
                {
                    GameOver = true;
                    timer.Stop();
                    MessageBox.Show("Вы выиграли!");

                }
                else
                {
                    IsPlayerTurn = false;
                    EnemyMove();
                }
            }
        }

        private void EnemyMove()
        {
            if (GameOver)
                return;

            Random rand = new Random();
            bool moveMade = false;

            while (!moveMade)
            {
                int x = rand.Next(10);
                int y = rand.Next(10);
                var cell = PlayerCells.Cells[x][y];

                if (!cell.IsShot)
                {
                    (bool, bool) chun = PlayerCells.Attack((int)x, (int)y);

                    if (chun.Item2)
                    {
                        int count = EnemyCells.CntSunkShip();
                        ElementScore = $"Computer: {(count).ToString()}";
                    }
                    if (cell.IsShip)
                    {
                        if (CheckVictory(PlayerCells))
                        {
                            GameOver = true;
                            MessageBox.Show("Противник выиграл!");
                            return;
                        }
                        // Противник ходит снова при попадании
                        EnemyMove();
                    }
                    else
                    {
                        IsPlayerTurn = true;
                    }
                    moveMade = true;
                }
            }
        }

        private bool CheckVictory(PlayerGridVM grid)
        {
            return grid.Cells.All(row => row.All(cell => !cell.IsShip || cell.IsShot));
        }

    }


}
