using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SeaBattle2._0
{
    internal class CellVM : BaseViewModel
    {
        private readonly Cell cell;

        private Visibility vis = Visibility.Hidden;

        private string color = "Red";
        public Visibility Visabile { 
            get => vis; 
            set
            {
                vis = value;
                OnPropertyChanged();
            } 
        }

        public string Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }

        public int X => cell.X;
        public int Y => cell.Y;

        public bool IsShip
        {
            get => cell.IsShip;
            set
            {
                if (cell.IsShip != value)
                {
                    cell.IsShip = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsShot
        {
            get => cell.IsShot;
            set
            {
                if (cell.IsShot != value)
                {
                    cell.IsShot = value;
                    OnPropertyChanged();
                }
            }
        }

        public CellVM(Cell cell)
        {
            this.cell = cell;
            if (cell.GetCell() == "~")
            {
                vis = Visibility.Hidden;
            }
            else if (cell.GetCell() == ".")
            {
                vis = Visibility.Visible;
                color = "White";
            }
            else
            {
                vis = Visibility.Visible;
                color = "Red";
            }
        }
    }
}
