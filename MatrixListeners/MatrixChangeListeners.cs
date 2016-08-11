using Matrix;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace MatrixListeners
{
    #region Listeners
    /// <summary>
    /// Listeners for matrix.
    /// </summary>
    /// <typeparam name="T">Specified type of the listeners.</typeparam>
    public class ListenersChangeMatrix<T>
    {
        public ListenersChangeMatrix(AbstractMatrix<T> matrix)
        {
            matrix.Change += ListenerMsg;
        }
        private void ListenerMsg(Object sender, ChangeMatrixEventArgs eventArgs)
        {
            Console.WriteLine("Change occurred : [{0},{1}]", eventArgs.IndexRow, eventArgs.IndexColumn);
        }

        public void Unregister(AbstractMatrix<T> matrix)
        {
            matrix.Change -= ListenerMsg;
        }
    }
    #endregion
}
