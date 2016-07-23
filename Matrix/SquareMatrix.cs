using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public abstract class AbstractMatrix<T> 
    {
        protected T[] matrix;

        public int Size { get; private set; }

        protected AbstractMatrix(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException();
            this.Size = size;
        }
        public abstract T this[int indexRow, int indexColumn] { get; set; }

        #region Event

        public event EventHandler<ChangeMatrixEventArgs> Change = delegate { };
        protected virtual void OnChanged(ChangeMatrixEventArgs e)
        {
            EventHandler<ChangeMatrixEventArgs> temp = Change;
            temp(this, e);
        }
        public void MakeChange(int indexRow, int indexColumn)
        {
            OnChanged(new ChangeMatrixEventArgs(indexRow, indexColumn));
        }
        #endregion
    }

    public class SquareMatrix<T> : AbstractMatrix<T>
    {
        public SquareMatrix(int size)
            : base(size)
        {
            this.matrix = new T[size * size];
        }

        public override T this[int indexRow, int indexColumn]
        {
            get
            {
                if (indexRow < 0 || indexColumn < 0 || indexRow >= Size || indexColumn >= Size) throw new ArgumentOutOfRangeException();
                return matrix[indexColumn + indexRow * Size];
            }
            set
            {
                if (indexRow < 0 || indexColumn < 0 || indexRow >= Size || indexColumn >= Size) throw new ArgumentOutOfRangeException();
                matrix[indexColumn + indexRow * Size] = value;
                this.MakeChange(indexRow, indexColumn);
            }
        }
    }

    public class DiagonalMatrix<T> : AbstractMatrix<T>
    {
        public DiagonalMatrix(int size)
            : base(size)
        {
            this.matrix = new T[size];
        }

        public override T this[int indexRow, int indexColumn]
        {
            get
            {
                if (indexColumn >= Size || indexRow >= Size || indexColumn < 0 || indexRow < 0) throw new ArgumentOutOfRangeException();
                if (indexRow != indexColumn) return default(T);
                else return matrix[indexRow];
            }
            set
            {
                if (indexRow != indexColumn) throw new ArgumentOutOfRangeException();
                try
                {
                    matrix[indexRow] = value;
                    this.MakeChange(indexColumn, indexRow);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class TriangularMatrix<T> : AbstractMatrix<T>
    {
        public TriangularMatrix(int size)
            : base(size)
        {
            this.matrix = new T[(Size * Size + Size) / 2];
        }

        public override T this[int indexRow, int indexColumn]
        {
            get
            {
                if (indexColumn >= Size || indexRow >= Size || indexColumn < 0 || indexRow < 0) throw new ArgumentOutOfRangeException();

                if (indexRow.CompareTo(indexColumn) >= 0) return matrix[indexRow * (indexRow + 1) / 2 + indexColumn];
                else return matrix[indexColumn * (indexColumn + 1) / 2 + indexRow];
            }
            set
            {
                if (indexColumn >= Size || indexRow >= Size || indexColumn < 0 || indexRow < 0) throw new ArgumentOutOfRangeException();

                if (indexRow.CompareTo(indexColumn) >= 0) matrix[indexRow * (indexRow + 1) / 2 + indexColumn] = value;
                else matrix[indexColumn * (indexColumn * (indexColumn + 1) / 2 + indexRow)] = value;

                this.MakeChange(indexColumn, indexRow);
                if (indexRow != indexColumn) this.MakeChange(indexRow, indexColumn);
            }
        }
    }

    public sealed class ChangeMatrixEventArgs : EventArgs
    {
        #region fields
        private readonly int indexRow;
        private readonly int indexColumn;
        #endregion

        #region ctor
        public ChangeMatrixEventArgs(int indexRow, int indexColumn)
        {
            this.indexRow = indexRow;
            this.indexColumn = indexColumn;
        }
        #endregion

        #region properties
        public int IndexRow { get { return indexRow; } }
        public int IndexColumn { get { return indexColumn; } }
        #endregion
    }
}
