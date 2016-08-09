using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTree;
using BookLogicLayer;
using Point2D;

namespace BinaryTreeTestConsoleApplication
{
    #region Comparators
    public class MyComparator : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x > y) return -1;
            else if (x < y) return 1;
            else return 0;
        }
    }

    public class ComporatorByAuthor : IComparer<Book>
    {
        public int Compare(Book first, Book second)
        {
            return first.Author.CompareTo(second.Author);
        }
    }

    public class PointComparator : IComparer<Point>
    {
        public int Compare(Point first, Point second)
        {
            return (first.X + second.X).CompareTo(second.X + second.Y);
        }
    } 
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            #region IntTest
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(6);
            tree.Add(2);
            tree.Add(8);
            tree.Add(1);
            tree.Add(4);
            tree.Add(7);
            tree.Add(9);
            tree.Add(3);
            tree.Add(5);
            tree.Remove(7);

            foreach (var x in tree.Inorder())
            {
                Console.Write(x);
            }

            Console.WriteLine();
            #endregion

            #region IntWithComparatorTest
            BinaryTree<int> tree2 = new BinaryTree<int>(new MyComparator());
            tree2.Add(6);
            tree2.Add(2);
            tree2.Add(8);
            tree2.Add(1);
            tree2.Add(4);
            tree2.Add(7);
            tree2.Add(9);
            tree2.Add(3);
            tree2.Add(5);

            foreach (var x in tree2.Inorder())
            {
                Console.Write(x);
            }
            Console.WriteLine();
            #endregion

            #region StringTest
            BinaryTree<string> tree3 = new BinaryTree<string>();
            tree3.Add("A");
            tree3.Add("M");
            tree3.Add("L");
            tree3.Add("R");
            tree3.Add("B");
            tree3.Add("O");
            tree3.Add("E");
            tree3.Add("K");
            tree3.Add("D");

            foreach (var x in tree3.Inorder())
            {
                Console.Write(x);
            }

            Console.WriteLine(); 
            #endregion

            #region BooksWithComparatorTest

            Book book1 = new Book("Jeffrey Richter", "CLR via C#", 2014, 890);
            Book book2 = new Book("Joseph Albahari", "C# 5.0 in a Nutshell", 2013, 1029);
            Book book3 = new Book("Andrew Troelsen", "Pro C# 5.0 and the .NET 4.5 Framework", 2014, 950);
            BinaryTree<Book> tree4 = new BinaryTree<Book>(new ComporatorByAuthor());
            tree4.Add(book1);
            tree4.Add(book2);
            tree4.Add(book3);

            foreach (var x in tree4.Inorder())
            {
                Console.Write(x);
            }
            Console.WriteLine();

            #endregion

            #region PointTest
            try
            {
                BinaryTree<Point> tree5 = new BinaryTree<Point>();

                tree5.Add(new Point(2, 6));
                tree5.Add(new Point(7, 2));
                tree5.Add(new Point(1, 3));
                tree5.Add(new Point(6, 4));
                tree5.Add(new Point(1, 1));
                tree5.Add(new Point(2, 8));


                foreach (var x in tree5.Inorder())
                {
                    Console.Write(x);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine(); 
            #endregion

        }
    }
}
