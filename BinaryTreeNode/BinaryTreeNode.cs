using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    #region BinaryTreeNode
    /// <summary>
    /// Node of the binary tree.
    /// </summary>
    /// <typeparam name="T">The type of the binary tree node.</typeparam>
    public class BinaryTreeNode<T>
    {
        public T Value { get; private set; }
        internal BinaryTreeNode<T> Right { get; set; }
        internal BinaryTreeNode<T> Left { get; set; }

        /// <summary>
        /// Initializes a new instance of the BinaryNodeTree<T> class that has the specified value.
        /// </summary>
        /// <param name="value">The value of the binary node tree.</param>
        public BinaryTreeNode(T value)
        {
            this.Value = value;
        }

    }
    #endregion

    #region BinaryTree
    /// <summary>
    /// Binary tree class.
    /// </summary>
    /// <typeparam name="T">The type of the binary tree.</typeparam>
    public class BinaryTree<T> : IEnumerable<T>
    {

        #region Fields
        private readonly IComparer<T> comparer;
        #endregion

        #region Properties
        public BinaryTreeNode<T> Head { get; private set; }

        public int Count { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the BinaryTree<T> class that is empty and has the default comparer.
        /// </summary>
        public BinaryTree()
        {
            if (typeof(T).GetInterface("IComparable`1") != null)
                comparer = Comparer<T>.Default;
            else
                throw new InvalidOperationException("Not implement the interface \"IComparable<>\"");
        }

        /// <summary>
        /// Initializes a new instance of the BinaryTree<T> class that is empty and has the specified comparer.
        /// </summary>
        /// <param name="comparer">Specified comparer.</param>
        public BinaryTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }
        #endregion

        #region Add
        /// <summary>
        /// Add an object to the binary tree.
        /// </summary>
        /// <param name="value">Specified object.</param>
        public void Add(T value)
        {
            if (ReferenceEquals(Head, null)) Head = new BinaryTreeNode<T>(value);
            else AddChild(Head, value);
            this.Count++;
        }

        /// <summary>
        /// Add an object to the binary tree as the child of the specified node.
        /// </summary>
        /// <param name="currentNode">Specified node.</param>
        /// <param name="value">Specified object.</param>
        private void AddChild(BinaryTreeNode<T> currentNode, T value)
        {
            if (comparer.Compare(value, currentNode.Value) < 0)
            {
                if (ReferenceEquals(currentNode.Left, null)) currentNode.Left = new BinaryTreeNode<T>(value);
                else AddChild(currentNode.Left, value);
            }
            else
            {
                if (ReferenceEquals(currentNode.Right, null)) currentNode.Right = new BinaryTreeNode<T>(value);
                else AddChild(currentNode.Right, value);
            }
        }
        #endregion

        #region Contains
        /// <summary>
        /// Determines whether an element is in the BinaryTree<T>.
        /// </summary>
        /// <param name="value">Element.</param>
        /// <returns>Bool value.</returns>
        public bool Contains(T value)
        {
            if (ReferenceEquals(Head, null)) throw new InvalidOperationException("Tree is empty");
            BinaryTreeNode<T> parent;
            return Contains(value, out parent) != null;
        }

        /// <summary>
        /// Determines whether an element is in the BinaryTree<T>.
        /// </summary>
        /// <param name="value">Element.</param>
        /// <param name="parent">Parent.</param>
        /// <returns>Bool value.</returns>
        private BinaryTreeNode<T> Contains(T value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = Head;
            parent = null;

            while (current != null)
            {
                int result = comparer.Compare(current.Value, value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes the first occurrence of a specific object from the BinaryTree<T>.
        /// </summary>
        /// <param name="value">Specified value.</param>
        /// <returns>Bool value.</returns>
        public virtual bool Remove(T value)
        {
            if (ReferenceEquals(Head, null))
                return false;

            BinaryTreeNode<T> current;
            BinaryTreeNode<T> parent;
            current = Contains(value, out parent);
            if (current == null)
            {
                return false;
            }
            Count--;


            if (current.Right == null)
            {
                if (parent == null)
                {
                    Head = current.Left;
                }

                else
                {
                    int result = comparer.Compare(parent.Value, current.Value);

                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }

                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }

            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    Head = current.Right;
                }
                else
                {
                    int result = comparer.Compare(parent.Value, current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
                leftmostParent.Left = leftmost.Right;

                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    Head = leftmost;
                }

                else
                {
                    int result = comparer.Compare(parent.Value, current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }

                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }

            return true;
        }
        #endregion

        #region Traversal      
        public IEnumerable<T> Inorder()
        {
            return Inorder(Head);
        }
        public IEnumerable<T> Preorder()
        {
            return Preorder(Head);
        }
        public IEnumerable<T> Postorder()
        {
            return Postorder(Head);
        }
        private IEnumerable<T> Inorder(BinaryTreeNode<T> node)
        {
            if (node.Left != null)
                foreach (var value in Inorder(node.Left))
                    yield return value;
            yield return node.Value;
            if (node.Right != null)
                foreach (var value in Inorder(node.Right))
                    yield return value;
        }

        private IEnumerable<T> Preorder(BinaryTreeNode<T> node)
        {
            yield return node.Value;
            if (node.Left != null)
                foreach (var value in Preorder(node.Left))
                    yield return value;
            if (node.Right != null)
                foreach (var value in Preorder(node.Right))
                    yield return value;
        }

        private IEnumerable<T> Postorder(BinaryTreeNode<T> node)
        {
            if (node.Left != null)
                foreach (var value in Postorder(node.Left))
                    yield return value;
            if (node.Right != null)
                foreach (var value in Postorder(node.Right))
                    yield return value;
            yield return node.Value;
        }
        #endregion

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    } 
    #endregion
}
