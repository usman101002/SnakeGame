using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using SnakeGame.Models;

namespace SnakeGame.ViewModels
{
    internal class CellVM : BindableBase
    {
        public int Row { get; }

        public int Column { get; }

        private CellType _cellType = CellType.None;

        public CellType CellType
        {
            get => _cellType;
            set
            {
                _cellType = value;
                RaisePropertyChanged(nameof(CellType));
            }
        }

        public CellVM(int row, int col)
        {
            Row = row;
            Column = col;
        }

    }
}
