using NUnit.Framework.Internal;
using Sort;

namespace Tests
{
  class SortableStub<T> : ISortable<T>
  {
    public List<T> Data;
    public T this[uint idx]
    {
      get => Data[(int)idx];
      set => Data[(int)idx] = value;
    }
    public uint Length { get => (uint)Data.Count; }
    public SortableStub(int capacity)
    {
      Data = new List<T>(capacity);
    }
  }

  class SortTest
  {
    private uint delCallCount = 0;
    private ISort<int> sort = new QuickSort<int>();
    private Ordering greaterFirst(int a, int b)
    {
        delCallCount++;
        if (a > b) return Ordering.Less;
        if (a < b) return Ordering.Greater;
        return Ordering.Equal;
    }
    private Ordering lessFirst(int a, int b)
    {
        delCallCount++;
        if (a < b) return Ordering.Less;
        if (a > b) return Ordering.Greater;
        return Ordering.Equal;
    }

    [Test]
    public void QuickSort_Zero()
    {
      var stub = new SortableStub<int>(0);
      sort.Sort(stub, greaterFirst);
      Assert.AreEqual(0, stub.Length);
    }

    [Test]
    public void QuickSort_One()
    {
      const int el = 12;
      var stub = new SortableStub<int>(1);
      stub.Data.Add(el);

      sort.Sort(stub, greaterFirst);
      Assert.AreEqual(1, stub.Length);
      Assert.AreEqual(el, stub[0]);
    }

    [Test]
    public void QuickSort_Many()
    {
      const int len = 100;
      var stub = new SortableStub<int>(len);
      delCallCount = 0;
      for (int i = 0; i < len; ++i)
      {
        stub.Data.Add(i);
      }
      stub.Data.Add(len - 1);

      sort.Sort(stub, greaterFirst);
      Assert.That(delCallCount > 0);
      for (uint i = 0; i < stub.Length - 1; ++i)
      {
        Assert.GreaterOrEqual(stub[i], stub[i+1]);
      }
      
      delCallCount = 0;
      sort.Sort(stub, lessFirst);
      Assert.That(delCallCount > 0);
      for (uint i = 0; i < stub.Length - 1; ++i)
      {
        Assert.LessOrEqual(stub[i], stub[i+1]);
      }

      delCallCount = 0;
      sort.Sort(stub, lessFirst);
      Assert.That(delCallCount > 0);
      for (uint i = 0; i < stub.Length - 1; ++i)
      {
        Assert.LessOrEqual(stub[i], stub[i+1]);
      }
    }
  }
}