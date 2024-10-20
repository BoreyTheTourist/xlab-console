using System.Numerics;
using System.Runtime.CompilerServices;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

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
            var vec = new Tuto.Vector<uint>();
            uint len = 0;
            Assert.AreEqual(vec.Length, len);

            len = 100;
            for (uint i = 0; i < len; ++i)
            {
                vec.Add(i);
                Assert.AreEqual(vec[i], i);
            }
            Assert.AreEqual(vec.Length, len);
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
            Assert.AreEqual(vec.Length, len);
            Assert.AreEqual(vec[0], el);

            vec.Insert(0, el1);
            len++;
            Assert.AreEqual(vec.Length, len);
            Assert.AreEqual(vec[0], el1);
            Assert.AreEqual(vec[1], el);

            vec.Insert(len++, el2);
            Assert.AreEqual(vec.Length, len);
            Assert.AreEqual(vec[0], el1);
            Assert.AreEqual(vec[1], el);
            Assert.AreEqual(vec[2], el2);
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
            const int el = 0;
            const int el1 = 1;
            const int el2 = 2;
            var vec = new Tuto.Vector<int>();
            uint len = 0;

            vec.Clear();
            Assert.AreEqual(vec.Length, len);

            len = 100;
            for (uint i = 0; i < len; ++i)
            {
                vec.Add((int)i);
            }
            Assert.AreEqual(vec.Length, len);

            vec.Clear();
            len = 0;
            Assert.AreEqual(vec.Length, len);
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
            Assert.AreEqual(vec.Length, len);
            Assert.AreEqual(vec[0], el1);
            Assert.AreEqual(vec[len - 1], el);

            vec.Remove(el3);
            Assert.AreEqual(vec.Length, len);
        }

        [Test]
        public void RemoveAt()
        {
            var vec = new Tuto.Vector<uint>();
            uint len = 100;

            for (uint i = 0; i < len; ++i)
            {
                vec.Add(i);
            }
            Assert.AreEqual(vec.Length, len);

            vec.RemoveAt(0);
            vec.RemoveAt(vec.Length-1);
            len -= 2;
            Assert.AreEqual(vec.Length, len);
            for (uint i = 0; i < len; ++i)
            {
                Assert.AreEqual(vec[i], i+1);
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
            Assert.AreEqual(vec.Length, len);
        }
    }
}