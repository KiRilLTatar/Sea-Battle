using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeaBattle2._0
{
    internal class PlayerGridVM : BaseViewModel
    {

        private PlayerGrid playerGrid;

        /// <summary>
        /// Коллекция ячеек для отображения
        /// </summary>
        public CellVM[][] Cells { get; }

        /// <summary>
        /// Размер игрового поля
        /// </summary>
        public int Size => playerGrid.Size;

        public List<Ship> Ships { get; }


        public PlayerGridVM(PlayerGrid playerGrid)
        {
            this.playerGrid = playerGrid;
            Cells = new CellVM[Size][];

            for (int i = 0; i < Size; i++)
            { 
                Cells[i] = new CellVM[Size];
                for (int j = 0; j < Size; j++)
                {
                    Cells[i][j] = new CellVM(playerGrid.CellsGrid[i][j]);              
                }
            }
        }

        /// <summary>
        /// Атаковать клетку на игровом поле.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Успешна ли атака.</returns>
        public bool Attack(int x, int y)
        {
            var success = playerGrid.Attack(x, y);

            if (success)
            {
                var cellVM = Cells[x][y];
                if (cellVM != null)
                {
                    cellVM.Shot();
                }
            }

            return success;
        }

        /// <summary>
        /// Проверка, потоплены ли все корабли.
        /// </summary>
        /// <returns>True, если все корабли потоплены.</returns>
        public bool AreAllShipsSunk()
        {
            return playerGrid.AreAllShipsSunk();
        }

        /// <summary>
        /// Получение ViewModel ячейки по координатам.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>ViewModel ячейки.</returns>
        //private CellVM GetCellViewModel(int x, int y)
        //{
        //    return Cells.FirstOrDefault(cell => cell.X == x && cell.Y == y);
        //}
    }
}
