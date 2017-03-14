using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab1
{
    [Serializable]
    class NameComparer : IComparer<Child>
    {
        public int Compare(Child x, Child y)
        {
            if(x.Name.Length == y.Name.Length)
            {
                return x.Name.CompareTo(y.Name);
            }

            return x.Name.Length - y.Name.Length;
        }
    }
}