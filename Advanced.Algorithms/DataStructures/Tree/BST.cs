﻿using System;

namespace Advanced.Algorithms.DataStructures
{
    internal class BSTNode<T> : IBSTNode<T> where T : IComparable
    {
        internal T Value { get; set; }

        internal BSTNode<T> Parent { get; set; }

        internal BSTNode<T> Left { get; set; }
        internal BSTNode<T> Right { get; set; }

        internal bool IsLeaf => Left == null && Right == null;
        internal bool IsLeftChild => this.Parent.Left == this;
        internal bool IsRightChild => this.Parent.Right == this;

        IBSTNode<T> IBSTNode<T>.Left => Left;

        IBSTNode<T> IBSTNode<T>.Right => Right;

        T IBSTNode<T>.Value => Value;

        internal BSTNode(BSTNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }

    }

    //TODO support initial bulk loading
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    public class BST<T> where T : IComparable
    {
        internal BSTNode<T> Root { get; set; }
        public int Count { get; private set; }

        //worst O(n) for unbalanced tree
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return find(Root, value) != null;
        }

        //worst O(n) for unbalanced tree
        public int getHeight()
        {
            return getHeight(Root);
        }

        //worst O(n) for unbalanced tree
        private int getHeight(BSTNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(getHeight(node.Left), getHeight(node.Right)) + 1;
        }


        internal BSTNode<T> InsertAndReturnNewNode(T value)
        {
            if (Root == null)
            {
                Root = new BSTNode<T>(null, value);
                Count++;
                return Root;
            }

            var newNode = insert(Root, value);
            Count++;

            return newNode;
        }


        //worst O(n) for unbalanced tree
        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new BSTNode<T>(null, value);
                Count++;
                return;
            }

            insert(Root, value);
            Count++;
        }

        //worst O(n) for unbalanced tree
        private BSTNode<T> insert(BSTNode<T> currentNode, T newNodeValue)
        {
            while (true)
            {
                var compareResult = currentNode.Value.CompareTo(newNodeValue);

                //current node is less than new item
                if (compareResult < 0)
                {
                    //no right child
                    if (currentNode.Right != null)
                    {
                        currentNode = currentNode.Right;
                        continue;
                    }

                    //insert
                    currentNode.Right = new BSTNode<T>(currentNode, newNodeValue);
                    return currentNode.Right;
                }
                //current node is greater than new node

                if (compareResult > 0)
                {
                    if (currentNode.Left == null)
                    {
                        //insert
                        currentNode.Left = new BSTNode<T>(currentNode, newNodeValue);
                        return currentNode.Left;
                    }

                    currentNode = currentNode.Left;
                }
                else
                {
                    throw new Exception("Item exists");
                }
            }
        }

        //remove the node with the given identifier from the descendants 
        //worst O(n) for unbalanced tree
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty BST");
            }

            delete(Root, value);
            Count--;
        }

        internal BSTNode<T> DeleteAndReturnParent(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty BST");
            }

            var parentNode = delete(Root, value);

            Count--;

            return parentNode;
        }

        //worst O(n) for unbalanced tree
        private BSTNode<T> delete(BSTNode<T> node, T value)
        {
            while (true)
            {
                if (node != null)
                {
                    var compareResult = node.Value.CompareTo(value);

                    //node is less than the search value so move right to find the deletion node
                    if (compareResult < 0)
                    {
                        node = node.Right ?? throw new Exception("Item do not exist");
                        continue;
                    }
                    //node is less than the search value so move left to find the deletion node

                    if (compareResult > 0)
                    {
                        node = node.Left ?? throw new Exception("Item do not exist");
                        continue;
                    }
                }

                //node is a leaf node
                if (node != null && node.IsLeaf)
                {
                    deleteLeaf(node);
                    return node.Parent;
                }

                //case one - right tree is null (move sub tree up)
                if (node?.Left != null && node.Right == null)
                {
                    deleteLeftNode(node);
                    return node.Parent;
                }
                //case two - left tree is null  (move sub tree up)

                if (node?.Right != null && node.Left == null)
                {
                    deleteRightNode(node);
                    return node.Parent;
                }
                //case three - two child trees 
                //replace the node value with maximum element of left subtree (left max node)
                //and then delete the left max node

                if (node == null)
                {
                    continue;
                }

                var maxLeftNode = FindMax(node.Left);

                node.Value = maxLeftNode.Value;

                //delete left max node
                node = node.Left;
                value = maxLeftNode.Value;
            }
        }

        private void deleteLeaf(BSTNode<T> node)
        {
            //if node is root
            if (node.Parent == null)
            {
                Root = null;
            }
            //assign nodes parent.left/right to null
            else if (node.IsLeftChild)
            {
                node.Parent.Left = null;
            }
            else
            {
                node.Parent.Right = null;
            }
        }

        private void deleteRightNode(BSTNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Right.Parent = null;
                Root = Root.Right;
            }
            else
            {
                //node is left child of parent
                if (node.IsLeftChild)
                {
                    node.Parent.Left = node.Right;
                }
                //node is right child of parent
                else
                {
                    node.Parent.Right = node.Right;
                }

                node.Right.Parent = node.Parent;

            }
        }

        private void deleteLeftNode(BSTNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Left.Parent = null;
                Root = Root.Left;
            }
            else
            {
                //node is left child of parent
                if (node.IsLeftChild)
                {
                    node.Parent.Left = node.Left;
                }
                //node is right child of parent
                else
                {
                    node.Parent.Right = node.Left;
                }

                node.Left.Parent = node.Parent;

            }
        }

        public T FindMax()
        {
            return FindMax(Root).Value;
        }


        private BSTNode<T> FindMax(BSTNode<T> node)
        {
            while (true)
            {
                if (node.Right == null) return node;
                node = node.Right;
            }
        }

        public T FindMin()
        {
            return findMin(Root).Value;
        }

        private BSTNode<T> findMin(BSTNode<T> node)
        {
            while (true)
            {
                if (node.Left == null) return node;
                node = node.Left;
            }
        }

        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //worst O(n) for unbalanced tree
        private BSTNode<T> find(BSTNode<T> parent, T value)
        {
            while (true)
            {
                if (parent == null)
                {
                    return null;
                }

                if (parent.Value.CompareTo(value) == 0)
                {
                    return parent;
                }

                var left = find(parent.Left, value);

                if (left != null)
                {
                    return left;
                }

                parent = parent.Right;
            }
        }
    }
}
