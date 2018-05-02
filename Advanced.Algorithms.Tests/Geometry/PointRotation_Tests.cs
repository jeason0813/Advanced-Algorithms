﻿using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class PointRotation_Tests
    {
        [TestMethod]
        public void PointRotation_Smoke_Test()
        {
            var result = PointRotation.Rotate(
                new Point() { x = 0, y = 0 },
                new Point() { x = 5, y = 5 },
                -45);

            Assert.AreEqual(7, (int)result.x);
            Assert.AreEqual(0, (int)result.y);

            result = PointRotation.Rotate(
                new Point() { x = 0, y = 0 },
                new Point() { x = 5, y = 5 },
                -90);

            Assert.AreEqual(5, (int)result.x);
            Assert.AreEqual(-5, (int)result.y);
        }
    }
}
