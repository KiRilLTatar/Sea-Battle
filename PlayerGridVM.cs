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

        private string scorePlayer;
        private string enemyPlayer;


        /// <summary>
        /// Коллекция ячеек для отображения
        /// </summary>
        public CellVM[][] Cells { get; }

        /// <summary>
        /// Размер игрового поля
        /// </summary>
        public int Size => playerGrid.Size;

        public PlayerGridVM(PlayerGrid playerGrid, bool LRgrid)
        {
            this.playerGrid = playerGrid;
            scorePlayer = "0";
            enemyPlayer = "0";
            Cells = new CellVM[Size][];

            for (int i = 0; i < Size; i++)
            { 
                Cells[i] = new CellVM[Size];
                for (int j = 0; j < Size; j++)
                {
                    if (LRgrid)
                        Cells[i][j] = new CellVM(playerGrid.CellsGrid[i][j], true); 
                    else
                        Cells[i][j] = new CellVM(playerGrid.CellsGrid[i][j], false);
                }
            }
        }

        /// <summary>
        /// Атаковать клетку на игровом поле.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Успешна ли атака.</returns>
        public (bool, bool) Attack(int x, int y)
        {
            var success = playerGrid.Attack(x, y);
            bool isSunki = false;

            if (success)
            { 
                var cellVM = Cells[x][y];
                if (cellVM.IsShip)
                {
                    foreach (var ship in playerGrid.Ships)
                    {
                        if (ship.GetStatus() == "Потоплен")
                        {
                            isSunki = true;
                            foreach (var coor in ship.CellsShip)
                            {
                                ArroundShip(coor.X, coor.Y);
                            }
                        }
                     
                    }
                }
                if (cellVM != null)
                {
                    cellVM.Shot();
                }
            }

            return (success, isSunki);
        }

        public int CntSunkShip()
        {
            return playerGrid.Ships.Count(ship => ship.IsSunk);
        }

        public void ArroundShip(int x, int y)
        {
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (playerGrid.IsValidCoordinate(nx, ny))
                {
                    Cells[nx][ny].Shot();
                }
            }
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
