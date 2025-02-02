using System.Collections.Generic;
using System.Linq;


namespace SeaBattle2._0
{
    internal class Ship
    {
        /// <summary>
        /// клетки корабля по координатам
        /// </summary>
        public List<Cell> CellsShip { get; }

        /// <summary>
        /// потоплен ли корабль
        /// </summary>
        public bool IsSunk => CellsShip.All(cell => cell.IsShot);

        public Ship(List<Cell> cells)
        {
            CellsShip = cells;

            foreach (var cell in CellsShip)
            {
                cell.IsShip = true;
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
