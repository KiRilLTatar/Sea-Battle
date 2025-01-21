using System.Collections.Generic;
using System.Linq;


namespace SeaBattle2._0
{
    internal class Ship
    {
        /// <summary>
        /// клетки корабля по координатам
        /// </summary>
        public List<Cell> Cells { get; }

        /// <summary>
        /// потоплен ли корабль
        /// </summary>
        public bool IsSunk => Cells.All(cell => cell.IsShot);

        public Ship(List<Cell> cells)
        {
            Cells = cells;

            foreach (var cell in Cells)
            {
                cell.IsShip = true;
            }
        }

        /// <summary>
        /// Данный метод производит выстрел по клетке и изменяет её состояние на пораженное
        /// </summary>
        /// <param name="repeat"></param>
        public void ShotCell(int x, int y) // выстрел по клетке 
        {
            var targetCell = Cells.FirstOrDefault(cell => cell.X == x && cell.Y == y);

            if (targetCell != null)
            {
                targetCell.Shot();
            }
        }


        /// <summary>
        /// Данный метод производит выстрел по клетке и изменяет её состояние на пораженное
        /// </summary>
        /// <param name="repeat">Сколько раз передать привет</param>
        /// <returns>Потоплен или повреждён</returns>
        public string GetStatus() // получение статуса корабля
        {
            if (IsSunk)
                return "Потоплен";
            return "Поврежден";
        }
    }
}
