using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTracking
{
    struct Vector2
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }


    struct ChessField
    {
        public int[,] matrix { get; set; }
        public int chessCount { get; set; }
    }

    class BackTrack
    {
        private int[,] matrix;
        private Vector2[] hitPos;
        private int colums;
        private int rows;
        private ChessField field;
        private int ier;
        public int[,] Result { get=>field.matrix; }

        public string IER
        {
            get
            {
                switch (ier)
                {
                    case 0:
                        return "Ошибок нет";
                    case 1:
                        return "Не удалось закрыть все позиции";
                    default:
                        return "Неизвестная ошибка";
                }
            }
        }

        public bool OnError { get => ier != 0; }

        public BackTrack(int[,] matrix)
        {
            InitHitPos();
            FillMatrix(matrix);
            if (ier != 0)
                return;
            field.matrix = CopyMatrix(this.matrix);
            var list = GetChess().ToList();
            field.chessCount = list.Count;
            Solve(list);
        }

        private void FillMatrix(int[,] matrix)
        {
            colums = matrix.GetLength(0);
            rows = matrix.Length / colums;
            this.matrix = new int[rows, colums];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < colums; col++)
                {
                    if (matrix[row, col]==1)
                    {
                        var hitPoses = GetHitPos(new Vector2(col, row));
                        bool existPos = false;
                        foreach (var hitPos in hitPoses)
                        {
                            if (matrix[hitPos.y, hitPos.x] !=1)
                            {
                                this.matrix[row, col]++;
                                this.matrix[hitPos.y, hitPos.x] = -1;
                                existPos = true;
                            }
                        }
                        if(!existPos)
                        {
                            ier = 1;
                            return;
                        }

                    }
                }
            }

        }

        private int[,] CopyMatrix(int[,] matrix)
        {
            var colums = matrix.GetLength(0);
            var rows = matrix.Length / colums;
            int[,] res = new int[rows, colums];
            for(int row=0; row<rows; row++)
            {
                for(int col=0; col<colums; col++)
                {
                    res[row, col] = matrix[row, col];
                }
            }
            return res;
        }

        private void Solve(List<Vector2> list)
        {
            int ind = 0;
            foreach (var i in list)
            {
                var hitPoses = GetHitPos(i);
                if (hitPoses.All(a => matrix[a.y, a.x] != 1))
                {
                    foreach (var pos in hitPoses)
                        if (matrix[pos.y, pos.x] > 1)
                        {
                            matrix[i.y, i.x] = 0;
                            matrix[pos.y, pos.x]--;
                        }
                    Solve(RemoveAt(list, ind));
                    foreach (var pos in hitPoses)
                        if (matrix[pos.y, pos.x] > 1)
                        {
                            matrix[pos.y, pos.x]++;
                            matrix[i.y, i.x] = -1;
                        }
                }
                else
                {
                    if(field.chessCount>list.Count)
                    {
                        field.chessCount = list.Count;
                        field.matrix = CopyMatrix(matrix);
                    }
                }
                ind++;
            }
        }

        private List<Vector2> RemoveAt(List<Vector2> list, int index)
        {
            var res = new List<Vector2>();
            foreach (var i in list) res.Add(i);
            res.RemoveAt(index);
            return res;
        }

        private IEnumerable<Vector2> GetChess()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < colums; col++)
                {
                    if (matrix[row, col] == -1)
                    {
                        yield return new Vector2(col, row);
                    }
                }
            }
        }

        private IEnumerable<Vector2> GetHitPos(Vector2 pos)
        {
            for (int i = 0; i < hitPos.Length; i++)
            {
                var hitPos = new Vector2(pos.x + this.hitPos[i].x, pos.y + this.hitPos[i].y);
                if (hitPos.y == Clamp(hitPos.y, 0, rows - 1) && hitPos.x == Clamp(hitPos.x, 0, colums - 1))
                {
                    yield return new Vector2(hitPos.x, hitPos.y);
                }
            }
        }

        private int Clamp(int value, int minValue, int maxValue)
        {
            return value > maxValue ? maxValue : value < minValue ? minValue : value;
        }

        private void InitHitPos()
        {
            hitPos = new Vector2[]{
            new Vector2(2, 1 ),
            new Vector2(2, -1 ),
            new Vector2(1, 2 ),
            new Vector2(1, -2 ),
            new Vector2(-2, 1 ),
            new Vector2(-2, -1 ),
            new Vector2(-1, 2 ),
            new Vector2(-1, -2 ) };
        }
    }
}
