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
            Assert.AreEqual(len, vec.Length);

            len = 100;
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
            var vec = new Tuto.Vector<int>();
            uint len = 0;

            vec.Clear();
            Assert.AreEqual(len, vec.Length);

            len = 100;
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
            var vec = new Tuto.Vector<uint>();
            uint len = 100;

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
                uint? idx = vec.IndexOf(vec[i]);
                Assert.NotNull(idx);
                Assert.AreEqual(i, idx.Value);
            }
        }

        [Test]
        public void IndexOf_NotPresent()
        {
            const int el = 2;
            const int el1 = 3;
            var vec = new Tuto.Vector<int>();
            vec.Add(el);
            Assert.Null(vec.IndexOf(el1));
        }
    }
}