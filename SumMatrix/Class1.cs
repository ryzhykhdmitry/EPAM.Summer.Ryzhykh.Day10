using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Matrix;

namespace SumMatrix
{
    public static class Class1
    {
        /// <summary>
        /// Create delegate.
        /// </summary>
        /// <typeparam name="T">Specified type.</typeparam>
        /// <returns>Delegate.</returns>
        private static Func<T, T, T> CreateAdd<T>()
        {
            ParameterExpression lhs = Expression.Parameter(typeof(T), "lhs");
            ParameterExpression rhs = Expression.Parameter(typeof(T), "rhs");

            Expression<Func<T, T, T>> addExpr = Expression<Func<T, T, T>>.
            Lambda<Func<T, T, T>>(Expression.Add(lhs, rhs), new ParameterExpression[] { lhs, rhs });

            Func<T, T, T> addMethod = addExpr.Compile();

            return addMethod;
        }

        /// <summary>
        /// Summary method for abstract matrix.
        /// </summary>
        /// <typeparam name="T">Specified type of a matrix.</typeparam>
        /// <param name="matrix">Abstract matrix.</param>
        /// <param name="otherMatrix">Abstract matrix.</param>
        /// <returns>Abstract matrix result.</returns>
        public static AbstractMatrix<T> SumMatrix<T>(this AbstractMatrix<T> matrix, AbstractMatrix<T> otherMatrix)
        {
            AbstractMatrix<T> newMatrix = new SquareMatrix<T>(matrix.Size);
            Func<T, T, T> addMethod = CreateAdd<T>();
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    newMatrix[i, j] = addMethod(matrix[i, j], otherMatrix[i, j]);
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// Summary method for diagonal matrix.
        /// </summary>
        /// <typeparam name="T">Specified type of a matrix.</typeparam>
        /// <param name="matrix">Diagonal matrix.</param>
        /// <param name="otherMatrix">Diagonal matrix.</param>
        /// <returns>Abstract matrix result.</returns>
        public static AbstractMatrix<T> SumMatrix<T>(this DiagonalMatrix<T> matrix, DiagonalMatrix<T> otherMatrix)
        {
            AbstractMatrix<T> newMatrix = new DiagonalMatrix<T>(matrix.Size);
            Func<T, T, T> addMethod = CreateAdd<T>();
            for (int i = 0; i < matrix.Size; i++)
            {
                newMatrix[i, i] = addMethod(matrix[i, i], otherMatrix[i, i]);
            }
            return newMatrix;
        }

        /// <summary>
        /// Summary method for triangular matrix.
        /// </summary>
        /// <typeparam name="T">Specified type of a matrix.</typeparam>
        /// <param name="matrix">Triangular matrix.</param>
        /// <param name="otherMatrix">Triangular matrix.</param>
        /// <returns>Abstract matrix result.</returns>
        public static AbstractMatrix<T> SumMatrix<T>(this TriangularMatrix<T> matrix, TriangularMatrix<T> otherMatrix)
        {
            AbstractMatrix<T> newMatrix = new TriangularMatrix<T>(matrix.Size);
            Func<T, T, T> addMethod = CreateAdd<T>();
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    newMatrix[i, i] = addMethod(matrix[i, i], otherMatrix[i, i]);
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// Summary method for triangular and diagonal matrix.
        /// </summary>
        /// <typeparam name="T">Specified type of a matrix.</typeparam>
        /// <param name="matrix">Triangular matrix.</param>
        /// <param name="otherMatrix">Diagonal matrix.</param>
        /// <returns>Abstract matrix result.</returns>
        public static AbstractMatrix<T> SumMatrix<T>(this TriangularMatrix<T> matrix, DiagonalMatrix<T> otherMatrix)
        {
            AbstractMatrix<T> newMatrix = new TriangularMatrix<T>(matrix.Size);
            Func<T, T, T> addMethod = CreateAdd<T>();
            for (int i = 0; i < matrix.Size; i++)
            {
                newMatrix[i, i] = addMethod(matrix[i, i], otherMatrix[i, i]);
            }
            return newMatrix;
        }
    }
}
