using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.ViewModels;

namespace SnakeGame.Models
{
    internal class Snake
    {
        public Queue<CellVM> SnakeCells { get; } = new Queue<CellVM>();
        private List<List<CellVM>> _allCells;
        private CellVM _start;
        private Action _generateFood;

        public Snake(List<List<CellVM>> allCells, CellVM start, Action generateFood)
        {
            this._start = start;
            this._allCells = allCells;
            _generateFood = generateFood;
            _start.CellType = CellType.Snake;
            SnakeCells.Enqueue(_start);
        }

        public void Restart()
        {
            foreach (var cells in SnakeCells)
            {
                cells.CellType = CellType.None;
            }
            SnakeCells.Clear();
            _start.CellType = CellType.Snake;
            SnakeCells.Enqueue(_start);
        }

        public void Move(MoveDirection direction)
        {
            var leaderCell = SnakeCells.Last();
            int nextRow = leaderCell.Row;
            int nextColumn = leaderCell.Column;

            switch (direction)
            {
                case MoveDirection.Left:
                    nextColumn -= 1;
                    break;
                case MoveDirection.Right:
                    nextColumn += 1;
                    break;
                case MoveDirection.Up:
                    nextRow -= 1;
                    break;
                case MoveDirection.Down:
                    nextRow += 1;
                    break;
                default:
                    break;
            }

            try
            {
                var nextCell = _allCells[nextRow][nextColumn];
                switch (nextCell?.CellType)
                {
                    case CellType.None:
                        nextCell.CellType = CellType.Snake;
                        SnakeCells.Dequeue().CellType = CellType.None;
                        SnakeCells.Enqueue(nextCell);
                        _generateFood?.Invoke();
                        break;
                    case CellType.Food:
                        nextCell.CellType = CellType.Snake;
                        SnakeCells.Enqueue(nextCell);
                        break;
                    default:
                        throw _gameOverEx;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                throw _gameOverEx;
            }
        }

        private Exception _gameOverEx => new Exception("Game over");
    }
}
