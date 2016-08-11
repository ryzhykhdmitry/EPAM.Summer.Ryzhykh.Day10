using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using MatrixListeners;
using SumMatrix;

namespace MatrixTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractMatrix<int> matrix = new SquareMatrix<int>(3);
            ListenersChangeMatrix<int> listener = new ListenersChangeMatrix<int>(matrix);
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 4;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            matrix[2, 0] = 7;
            matrix[2, 1] = 8;
            matrix[2, 2] = 9;


            AbstractMatrix<int> matrix2 = new TriangularMatrix<int>(3);
            ListenersChangeMatrix<int> listener2 = new ListenersChangeMatrix<int>(matrix2);
            matrix2[0, 0] = 1;
            matrix2[1, 0] = 2;
            matrix2[1, 1] = 3;
            matrix2[2, 0] = 4;
            matrix2[2, 1] = 5;
            matrix2[2, 2] = 6;


            AbstractMatrix<int> matrix4 = new DiagonalMatrix<int>(3);
            ListenersChangeMatrix<int> listener3 = new ListenersChangeMatrix<int>(matrix4);
            matrix4[0, 0] = 1;
            matrix4[1, 1] = 2;
            matrix4[2, 2] = 3;


            AbstractMatrix<int> matrix3 = matrix4.SumMatrix(matrix4);
            for (int i = 0; i < matrix3.Size; i++)
            {
                for (int j = 0; j < matrix3.Size; j++)
                {
                    Console.Write(" " + matrix3[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
