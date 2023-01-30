using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Organizer;

namespace BornToMove.OrganizerTest
{
    public class RotateSortTest
    {
        [Test]
        public void TestSortEmpty()
        {
            // prepare
            List<int> input = new List<int>();
            var comparer = Comparer<int>.Default;
            var shs = new ShiftHighestSort<int>();

            // run
            var results = shs.Sort(input, comparer);

            // validate
            Assert.NotNull(results);
        }

        [Test]
        public void TestSortOneElement() 
        {
            // prepare
            List<int> input = new List<int>() { 1};
            var comparer = Comparer<int>.Default;
            var shs = new ShiftHighestSort<int>();

            // run
            var results = shs.Sort(input, comparer);

            // validate
            Assert.AreEqual(1, results[0]);
        }

        [Test]
        public void TestSortTwoElements()
        {
            // prepare
            List<int> input = new List<int>() { 4, 1 };
            var comparer = Comparer<int>.Default;
            var shs = new ShiftHighestSort<int>();

            // run
            var results = shs.Sort(input, comparer);

            // validate
            Assert.AreEqual(1, results[0]);
            Assert.AreEqual(4, results[1]);
        }

        [Test]
        public void TestSortThreeEqualElements()
        {
            // prepare
            List<int> input = new List<int>() { 4, 4, 4 };
            var comparer = Comparer<int>.Default;
            var shs = new ShiftHighestSort<int>();

            // run
            var results = shs.Sort(input, comparer);

            // validate
            Assert.AreEqual(4, results[0]);
            Assert.AreEqual(4, results[1]);
            Assert.AreEqual(4, results[2]);
        }

        [Test]
        public void TestSortThreeUnsortedArray()
        {
            // prepare
            List<int> input = new List<int>() { 4, 1, 7 };
            var comparer = Comparer<int>.Default;
            var shs = new ShiftHighestSort<int>();

            // run
            var results = shs.Sort(input, comparer);

            // validate
            Assert.AreEqual(1, results[0]);
            Assert.AreEqual(4, results[1]);
            Assert.AreEqual(7, results[2]);
        }

        [Test]
        public void TestSortSevenElementsThreeEqual()
        {
            // prepare
            List<int> input = new List<int>() { 5, 4, 4, 1, 4, 6, 2 };
            var comparer = Comparer<int>.Default;
            var shs = new ShiftHighestSort<int>();

            // run
            var results = shs.Sort(input, comparer);

            // validate
            Assert.AreEqual(1, results[0]);
            Assert.AreEqual(2, results[1]);
            Assert.AreEqual(4, results[2]);
            Assert.AreEqual(4, results[3]);
            Assert.AreEqual(4, results[4]);
            Assert.AreEqual(5, results[5]);
            Assert.AreEqual(6, results[6]);

        }

    }
}
