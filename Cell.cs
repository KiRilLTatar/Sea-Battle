using System;

namespace SeaBattle2._0
{
    internal class Cell 
    {
        // есть ли корабль в этой клетке
        public bool IsShip { get; set; }
        // была ли атака
        public bool IsShot {  get; set; }
        // уничтожен ли корбаль или нет
        public bool IsDestroyed => IsShip && IsShot;

        // координаты ячейки на игровом поле
        public int X { get; }
        public int Y { get; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            IsShip = false;
            IsShot = false;
        }

        /// <summary>
        /// измениние состояния влетки на пораженное 
        /// </summary>
        public void Shot()
        {
            IsShot = true;
        }


        /// <summary>
        /// Возвращает тип клетки строкой
        /// </summary>
        /// <returns>X - Уничтоженн
        ///          . - Поражен
        ///          S - корабль
        ///          ~ - пусто
        /// </returns>
        public string GetCell()
        {
            if (IsDestroyed)
                return "X"; 
            if (IsShot)
                return "."; 
            if (IsShip)
                return "S"; 
            return "~"; 
        }
    }
}
