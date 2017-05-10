﻿using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreeDictionaryNode<K, V> : IComparable
                                 where K : IComparable
    {
        public K Key { get; private set; }
        public V Value { get; set; }

        public AsTreeDictionaryNode(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            var itemToComare = obj as AsTreeDictionaryNode<K, V>;
            return Key.CompareTo(itemToComare.Key);
        }
    }
    /// <summary>
    /// A Dictionary implementation using balanced binary search tree (log(n) operations in worst case)
    /// This may be better than regular Dictionary implementation which can give o(K) in worst case (but O(1) when collisions K is avoided )
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsTreeDictionary<K, V> where K : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private AsRedBlackTree<AsTreeDictionaryNode<K, V>> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public AsTreeDictionary()
        {
            binarySearchTree = new AsRedBlackTree<AsTreeDictionaryNode<K, V>>();
        }

        //O(log(n) time complexity; 
        public bool ContainsKey(K key)
        {
            return binarySearchTree
                .HasItem(new AsTreeDictionaryNode<K, V>(key, default(V)));
        }

        //O(log(n) time complexity; 
        public V GetValue(K key)
        {
            return binarySearchTree
                .Find(new AsTreeDictionaryNode<K, V>(key, default(V)))
                .Value
                .Value;
        }

        //O(log(n) time complexity; 
        //add an item to this hash table
        public void Add(K key, V value)
        {
            binarySearchTree.Insert(new AsTreeDictionaryNode<K, V>(key, value));
        }

        //O(log(n) time complexity
        public void Remove(K key)
        {
            binarySearchTree.Delete(new AsTreeDictionaryNode<K, V>(key, default(V)));
        }

        //O(n) time complexity
        public AsArrayList<AsTreeDictionaryNode<K, V>> GetAll()
        {
            var nodes = binarySearchTree.GetAllNodes();

            var allNodeValues = new AsArrayList<AsTreeDictionaryNode<K, V>>();

            for (int i = 0; i < nodes.Count; i++)
            {
                allNodeValues.Add(nodes[i]);
            }

            nodes.Clear();

            return allNodeValues;
        }

        internal void Clear()
        {
            binarySearchTree.Clear();
        }
    }
}
