using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattle2._0
{
    // игровое поле
    internal class PlayerGrid
    {
        // размер игрового поля
        public int Size { get; }

        // само игровое поле
        public Cell[][] Cells { get; }

        // список кораблей
        public List<Ship> Ships { get; }

        public PlayerGrid(int size)
        {
            Size = size;
            Cells = new Cell[size][];
            Ships = new List<Ship>();

            for (int x = 0; x < size; x++)
            {
                Cells[x] = new Cell[size];
                for (int y = 0; y < size; y++)
                {
                    Cells[x][y] = new Cell(x, y);
                }
            }
        }

        /// <summary>
        /// Заполняет поле кораблями
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns>true или false</returns>
        public bool PlaceShip(List<(int X, int Y)> coordinates)
        {
            var shipCells = new List<Cell>();

            foreach (var (x, y) in coordinates)
            {
                if (!IsValidCoordinate(x, y))
                {
                    return false;
                }

                if (Cells[x][y].IsShip || HasShips(x, y))
                {
                    return false; 
                }

                shipCells.Add(Cells[x][y]);
            }

            var ship = new Ship(shipCells);
            Ships.Add(ship);
            return true;
        }


        /// <summary>
        /// есть ли в соседних координатах корабль
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool HasShips(int x, int y)
        {
            // все возможные смещения от корабля
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (IsValidCoordinate(nx, ny) && Cells[nx][ny].IsShip)
                {
                    return true; 
                }
            }

            return false; 
        }

        /// <summary>
        /// Атака по клетке
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool Attack(int x, int y)
        {
            
            if (!IsValidCoordinate(x, y))
            {
                throw new ArgumentException("Некорректные координаты."); // выбрасываем ошибку
            }

            var cell = Cells[x][y];

            if (cell.IsShot)
            {
                return false;
            }

            cell.Shot();

            return true; 
        }

        /// <summary>
        /// все корабли на поле потоплены
        /// </summary>
        /// <returns></returns>
        public bool AreAllShipsSunk()
        {
            return Ships.All(ship => ship.IsSunk);
        }
        
        /// <summary>
        /// валидность координат
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsValidCoordinate(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        // тестирование
        //public void PrintBoard()
        //{
        //    for (int y = 0; y < Size; y++)
        //    {
        //        for (int x = 0; x < Size; x++)
        //        {
        //            var cell = Cells[x, y];
        //            Console.Write(cell.IsDestroyed ? "X" :
        //                          cell.IsShot ? "." :
        //                          cell.IsShip ? "S" : "~");
        //        }
        //        Console.WriteLine();
        //    }
        //}
    }
}
