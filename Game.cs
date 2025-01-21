using System;

namespace SeaBattle2._0
{
    // игровые поля 
    internal class Game
    {
        public PlayerGrid PlayerEnemy { get; }
        public PlayerGrid EnemyCells { get; }

        public int CurrentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }

        public int? Win {  get; private set; } // null определеляем на момент того пока идёт игра


        public Game(int size)
        {
            PlayerEnemy = new PlayerGrid(size);
            EnemyCells = new PlayerGrid(size);

            CurrentPlayer = 1;
            IsGameOver = false;
            Win = null;
        }

        /// <summary>
        /// проведение хода 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="Exception">выкидвает ошибку в случае окончания игры</exception>
        public bool MakeStep(int x, int y)
        {
            if (IsGameOver)
            {
                throw new Exception("Игры законченна");
            }

            var targetGrig = CurrentPlayer == 1 ? PlayerEnemy : EnemyCells;

            if (!targetGrig.Attack(x, y))
            {
                return false;
            }

            if (targetGrig.AreAllShipsSunk())
            {
                IsGameOver = true;
                Win = CurrentPlayer;
            }

            return true;
        }

        /// <summary>
        /// переход хода, используется в случае промаха
        /// </summary>
        private void SwitchStep()
        {
            CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
        }

        /// <summary>
        /// Проверка состояния игры, чей ход
        /// </summary>
        /// <returns></returns>
        public string GetGameState()
        {
            if (IsGameOver)
            {
                return $"Игра завершена! Победитель: Игрок {Win}";
            }

            return $"Ход игрока {CurrentPlayer}";
        }
    }
}
