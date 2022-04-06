using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Architecture
{
    public class PermutationList
    {
        int numberOfElements;
        List<int> indexes;

        public PermutationList(int numberOfElements)
        {
            Assert.IsTrue(numberOfElements > 0);
            this.numberOfElements = numberOfElements;
            Reset();
        }

        public void Reset()
        {
            indexes = new List<int>();
            for (int i = 0; i < numberOfElements; i++)
            {
                indexes.Add(i);
            }
        }

        public int GetNextIndex()
        {
            if (indexes.Count == 0)
            {
                Reset();
            }

            int thisIndex = Random.Range(0, indexes.Count);
            int result = indexes[thisIndex];
            indexes.RemoveAt(thisIndex);
            return result;
        }
    }

}