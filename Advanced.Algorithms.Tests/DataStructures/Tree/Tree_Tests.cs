﻿using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class Tree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void Tree_Test()
        {
            
            var tree = new Tree<int>();
            Assert.AreEqual(tree.GetHeight(), -1);

            tree.Insert(0, 0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(0, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(1, 2);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Delete(1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Delete(2);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Delete(0);
            Assert.AreEqual(tree.GetHeight(), -1);

            tree.Insert(0, 0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(0, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(1, 2);
            Assert.AreEqual(tree.GetHeight(), 2);

        }
    }
}
