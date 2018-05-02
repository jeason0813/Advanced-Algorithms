﻿using System;

namespace Advanced.Algorithms.DataStructures
{
    public interface IBSTNode<T> where T : IComparable
    {
        IBSTNode<T> Left { get;  }
        IBSTNode<T> Right { get;  }

        T Value { get;  }
    }
}
