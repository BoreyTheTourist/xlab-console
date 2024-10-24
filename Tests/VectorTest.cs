using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Sort;

namespace Tests
{
    public class VectorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add()
        {
            uint len = 100;
            var vec = new Tuto.Vector<uint>(len);
            Assert.AreEqual(0, vec.Length);

            for (uint i = 0; i < len; ++i)
            {
                vec.Add(i);
                Assert.AreEqual(i, vec[i]);
            }
            Assert.AreEqual(len, vec.Length);
        }

        [Test]
        public void Insert()
        {
            const char el = '5';
            const char el1 = '3';
            const char el2 = '2';
            var vec = new Tuto.Vector<char>();
            uint len = 0;

            vec.Insert(0, el);
            len++;
            Assert.AreEqual(len, vec.Length);
            Assert.AreEqual(el, vec[0]);

            vec.Insert(0, el1);
            len++;
            Assert.AreEqual(len, vec.Length);
            Assert.AreEqual(el1, vec[0]);
            Assert.AreEqual(el, vec[1]);

            vec.Insert(len++, el2);
            Assert.AreEqual(len, vec.Length);
            Assert.AreEqual(el1, vec[0]);
            Assert.AreEqual(el, vec[1]);
            Assert.AreEqual(el2, vec[2]);
        }

        [Test]
        public void Insert_OutOfRange()
        {
            const double el = 1.0;
            var vec = new Tuto.Vector<double>();

            Assert.Throws<IndexOutOfRangeException>(() => { vec.Insert(vec.Length + 1, el); });
        }

        [Test]
        public void Clear()
        {
            uint len = 100;
            var vec = new Tuto.Vector<int>(len);

            vec.Clear();
            Assert.AreEqual(0, vec.Length);

            for (uint i = 0; i < len; ++i)
            {
                vec.Add((int)i);
            }
            Assert.AreEqual(len, vec.Length);

            vec.Clear();
            len = 0;
            Assert.AreEqual(len, vec.Length);
        }

        [Test]
        public void Remove()
        {
            const int el = 0;
            const int el1 = 2;
            const int el2 = 5;
            const int el3 = 7;
            var vec = new Tuto.Vector<int>();
            uint len = 0;

            vec.Add(el);
            vec.Add(el1);
            vec.Add(el2);
            vec.Add(el);
            len += 4;

            vec.Remove(el);
            len--;
            Assert.AreEqual(len, vec.Length);
            Assert.AreEqual(el1, vec[0]);
            Assert.AreEqual(el, vec[len - 1]);

            vec.Remove(el3);
            Assert.AreEqual(len, vec.Length);
        }

        [Test]
        public void RemoveAt()
        {
            uint len = 100;
            var vec = new Tuto.Vector<uint>(len);

            for (uint i = 0; i < len; ++i)
            {
                vec.Add(i);
            }
            Assert.AreEqual(len, vec.Length);

            vec.RemoveAt(0);
            vec.RemoveAt(vec.Length-1);
            len -= 2;
            Assert.AreEqual(len, vec.Length);
            for (uint i = 0; i < len; ++i)
            {
                Assert.AreEqual(i+1, vec[i]);
            }
        }

        [Test]
        public void RemoveAt_OutOfRange()
        {
            const int el = 10;
            var vec = new Tuto.Vector<int>();
            vec.Add(el);
            uint len = 1;

            Assert.Throws<IndexOutOfRangeException>(() => { vec.RemoveAt(vec.Length + 1); });
            Assert.AreEqual(len, vec.Length);
        }

        [Test]
        public void IndexOf()
        {
            const uint len = 100;
            var vec = new Tuto.Vector<uint>(len);
            for (uint i = 0; i < len; ++i)
            {
                vec.Add(i);
                uint idx;
                Assert.NotNull(vec.IndexOf(vec[i]).IsSome(out idx));
                Assert.AreEqual(i, idx);
            }
        }

        [Test]
        public void IndexOf_NotPresent()
        {
            const int el = 2;
            const int el1 = 3;
            var vec = new Tuto.Vector<int>();
            vec.Add(el);
            Assert.That(vec.IndexOf(el1).IsNone());
        }

        [Test]
        public void SortBy()
        {
            const uint len = 10;
            uint callCount = 0;
            var vec = new Tuto.Vector<uint>(len);
            for (uint i = 0; i < len; ++i)
            {
                vec.Add(i);
            }
            
            vec.Sorter = new QuickSort<uint>();
            vec.SortBy(Ordering (uint a, uint b) => {
                callCount++;
                if (a > b) return Ordering.Greater;
                if (a < b) return Ordering.Less;
                return Ordering.Equal;
            });
            Assert.That(callCount > 0);
        }

        [Test]
        public void ForEach()
        {
            uint len = 20;
            uint callCount = 0;
            var vec1 = new Tuto.Vector<uint>(len);
            var vec2 = new Tuto.Vector<uint>(len);

            vec1.ForEach(el => {
                callCount++;
                vec2.Add(el);
            });
            Assert.AreEqual(0, vec1.Length);
            Assert.AreEqual(0, vec1.Length);
            Assert.AreEqual(0, callCount);

            for (uint i = 0; i < len; ++i)
            {
                vec1.Add(i);
            }
            vec1.ForEach(el => {
                callCount++;
                vec2.Add(el);
            });
            Assert.AreEqual(len, vec1.Length);
            Assert.AreEqual(len, callCount);
            Assert.AreEqual(vec1.Length, vec2.Length);
            for (uint i = 0; i < vec1.Length; ++i)
            {
                Assert.AreEqual(vec1[i], vec2[i]);
            }
        }

        [Test]
        public void Find()
        {
            const string el = "some";
            const string elSub = "so";
            const string el1 = "kak";
            var vec = new Tuto.Vector<string>();
            vec.Add(el);
            vec.Add(el1);

            var ans = vec.Find(el => el.Contains(elSub));
            string val;
            Assert.That(ans.IsSome(out val));
            Assert.That(val == el);
        }

        [Test]
        public void Find_NotPresent()
        {
            var vec = new Tuto.Vector<int>();
            Assert.That(vec.Find(el => el > 0).IsNone());
        }
    }
}