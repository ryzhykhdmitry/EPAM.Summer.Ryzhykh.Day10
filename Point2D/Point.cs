using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point2D
{
    /// <summary>
    /// Struct Point for binary tree testing
    /// </summary>
    public struct Point
    {
        private int x;
        private int y;
        public int X
        { get { return x; } }

        public int Y
        { get { return y; } }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return String.Format("Point : {0}-{1}", this.X, this.Y);
        }
    }
}
