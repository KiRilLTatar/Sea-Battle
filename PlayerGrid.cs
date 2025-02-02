using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;

namespace SeaBattle2._0
{
    // игровое поле
    internal class PlayerGrid
    {
        // размер игрового поля
        public int Size { get; }

        // само игровое поле
        public Cell[][] CellsGrid { get; }

        // список кораблей
        public List<Ship> Ships { get; }

        public PlayerGrid(int size)
        {
            Size = size;
            CellsGrid = new Cell[size][];
            Ships = new List<Ship>();

            for (int x = 0; x < size; x++)
            {
                CellsGrid[x] = new Cell[size];
                for (int y = 0; y < size; y++)
                {               
                    CellsGrid[x][y] = new Cell(x, y);
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

                if (CellsGrid[x][y].IsShip || HasShips(x, y))
                {
                    return false; 
                }

                shipCells.Add(CellsGrid[x][y]);
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

                if (IsValidCoordinate(nx, ny) && CellsGrid[nx][ny].IsShip)
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

            var cell = CellsGrid[x][y];

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
        public bool IsValidCoordinate(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        // тестирование
        public void PrintBoard()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    var cell = CellsGrid[x][y];
                    Console.Write(cell.IsDestroyed ? "X" :
                                  cell.IsShot ? "." :
                                  cell.IsShip ? "S" : "~");
                }
                Console.WriteLine();
            }
        }

        public void GenerationShip()
        {
            PlaceShipsGrid(4, 1);
            PlaceShipsGrid(3, 2);
            PlaceShipsGrid(2, 3);
            PlaceShipsGrid(1, 4);
        }

        private void PlaceShipsGrid(int count, int size)
        {
            Random rand = new Random();
            List<(int x, int y)> allShipsCoordinates = new List<(int x, int y)>();

            for (int i = 0; i < count; i++)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = rand.Next(10);
                    int col = rand.Next(10);
                    bool horizontal = rand.Next(2) == 0;

                    var shipCoordinates = GenerateShipCoordinates(row, col, size, horizontal);

                    if (CanPlaceShip(CellsGrid, shipCoordinates))
                    {
                        PlaceShip(shipCoordinates);
                        placed = true;
                    }
                }
            }
        }


        private List<(int x, int y)> GenerateShipCoordinates(int row, int col, int size, bool horizontal)
        {
            List<(int x, int y)> coordinates = new List<(int x, int y)>();
            for (int i = 0; i < size; i++)
            {
                int r = row + (horizontal ? 0 : i);
                int c = col + (horizontal ? i : 0);
                coordinates.Add((r, c));
            }
            return coordinates;
        }

        private bool CanPlaceShip(Cell[][] field, List<(int x, int y)> coordinates)
        {
            foreach (var (r, c) in coordinates)
            {
                if (r < 0 || r >= 10 || c < 0 || c >= 10 || field[r][c].IsShip)
                    return false;

                // Проверка на соседние клетки
                for (int dr = -1; dr <= 1; dr++)
                {
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        int nr = r + dr;
                        int nc = c + dc;
                        if (nr >= 0 && nr < 10 && nc >= 0 && nc < 10 && field[nr][nc].IsShip)
                            return false;
                    }
                }
            }
            return true;
        }


    }
}
